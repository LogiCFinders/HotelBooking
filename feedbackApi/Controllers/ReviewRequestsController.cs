using feedbackApi.Data.DBContext;
using feedbackApi.Data.DTOs;
using feedbackApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;


namespace feedbackApi.Controllers
{
    public class ReviewRequestsController : ApiController
    {
        private readonly FeedbackDbContext _db;
        private readonly IConfiguration _config;
        private readonly feedbackApi.Services.EmailService _email;
        public ReviewRequestsController()
        {
            _db = new FeedbackDbContext();
            _config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory()) // needed for JSON file lookup
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //.AddEnvironmentVariables()
            .Build();
            _email = new feedbackApi.Services.EmailService(_config);
        }

        

        public ReviewRequestsController(FeedbackDbContext db, feedbackApi.Services.EmailService email)
        {
            _db = db;
            //_config = config;
            _email = email;
        }
        [HttpPost]
        [Route("api/reviewrequests")]
        public async Task<IHttpActionResult> Create(ReviewRequestCreateDto dto)
        {
            var company = await _db.Companies.Include(c => c.Plan)
                .SingleOrDefaultAsync(c => c.CompanyId == dto.CompanyId && c.IsActive);
            if (company == null)
            {
                return ResponseMessage(Request.CreateResponse(
                    HttpStatusCode.NotFound,
                    new { message = "Company not found or inactive." }));
            }

            // ---- Plan threshold check: requests created this calendar month ----
            var plan = company.Plan;
            if (plan != null && plan.ThresholdLimit > 0)
            {
                var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var usedThisMonth = await _db.ReviewRequests.CountAsync(r =>
                    r.CompanyId == dto.CompanyId && r.RequestedDate >= monthStart);

                if (usedThisMonth >= plan.ThresholdLimit)
                {
                    return ResponseMessage(Request.CreateResponse(
                        (HttpStatusCode)429,   // Too Many Requests
                        new
                        {
                            message = $"Monthly limit reached: your '{plan.PlanName}' plan allows "
                                    + $"{plan.ThresholdLimit} review requests per month and you have used {usedThisMonth}. "
                                    + "Please upgrade your plan to send more requests.",
                            used = usedThisMonth,
                            limit = plan.ThresholdLimit,
                            plan = plan.PlanName
                        }));
                }
            }

            var request = new ReviewRequest
            {
                CustomerEmail = dto.CustomerEmail,
                CustomerPhone = dto.CustomerPhone,
                CustomerName = dto.CustomerName,
                ServiceID = dto.ServiceID,
                ServiceDate = (DateTime)(dto.ServiceDate.HasValue? dto.ServiceDate:null),
                RequestedDate = DateTime.Now,
                CompanyId = dto.CompanyId
            };

            _db.ReviewRequests.Add(request);
            await _db.SaveChangesAsync();

            // Pre-filled link for SubmitReview.aspx that can be emailed/SMSed to the customer
            var baseUrl = System.Configuration.ConfigurationManager.AppSettings["ReviewFormBaseUrl"]
                    ?? "https://review.logicfinders.com/SubmitReview.aspx";
           
            var link = $"{baseUrl}?companyId={request.CompanyId}"
                     + $"&name={Uri.EscapeDataString(request.CustomerName)}"
                     + $"&email={Uri.EscapeDataString(request.CustomerEmail)}"
                     + (string.IsNullOrEmpty(request.ServiceID) ? "" : $"&serviceId={Uri.EscapeDataString(request.ServiceID)}")
                     + (request.ServiceDate!=null ? $"&serviceDate={request.ServiceDate:yyyy-MM-dd}" : "");

            // Web API 2 doesn't have CreatedAtAction, so use Created with Url.Link
            var location = new Uri(Url.Link("DefaultApi", new { controller = "ReviewRequest", id = request.RequestID }));

            return Created(location, new { request.RequestID, request.RequestedDate, reviewLink = link });
        }
        // GET api/reviewrequests?page=1&pageSize=10&companyId=2&status=pending
        [HttpGet]
        [Route("api/reviewrequests")]
        public async Task<IHttpActionResult> GetPaged(
            int page = 1,
            int pageSize = 10,
            int? companyId = null,
            string status = "all")
        {
            // Ensure valid bounds
            page = Math.Max(page, 1);
            pageSize = Math.Min(Math.Max(pageSize, 1), 100); // clamp manually

            var query = _db.ReviewRequests.Include(r => r.Company).AsQueryable();

            if (companyId.HasValue && companyId > 0)
                query = query.Where(r => r.CompanyId == companyId);

            // Replace C# 8 switch expression with if/else
            if (!string.IsNullOrEmpty(status))
            {
                var s = status.ToLower();
                if (s == "pending")
                    query = query.Where(r => r.ReviewDate == null);
                else if (s == "received")
                    query = query.Where(r => r.ReviewDate != null);
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.RequestedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new
                {
                    r.RequestID,
                    r.CustomerEmail,
                    r.CustomerPhone,
                    r.CustomerName,
                    r.ServiceID,
                    r.ServiceDate,
                    r.RequestedDate,
                    r.ReviewDate,
                    r.CompanyId,
                    CompanyName = r.Company.CompanyName,
                    Status = r.ReviewDate == null ? "Awaiting review" : "Review received"
                })
                .ToListAsync();

            return Ok(new
            {
                total,
                page,
                pageSize,
                totalPages = (int)Math.Ceiling(total / (double)pageSize),
                items
            });
        }
        [HttpPost]
        [Route("api/reviewrequests/{id:int}/sendmail")]
        public async Task<IHttpActionResult> SendMail(int id)
        {
            var request = await _db.ReviewRequests.Include(r => r.Company)
                .SingleOrDefaultAsync(r => r.RequestID == id);

            if (request == null)
            {
                return ResponseMessage(Request.CreateResponse(
                    HttpStatusCode.NotFound,
                    new { message = "Review request not found." }));
            }

            if (request.ReviewDate != null)
            {
                return ResponseMessage(Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new { message = "A review has already been submitted for this request." }));
            }
            var baseUrl = System.Configuration.ConfigurationManager.AppSettings["ReviewFormBaseUrl"]
                     ?? "https://review.logicfinders.com/SubmitReview.aspx";
           //
            var link = $"{baseUrl}?requestId={request.RequestID}"; 
            var companyName = request.Company?.CompanyName ?? "us";

            var subject = $"We'd love your feedback - {companyName}";
            var body = $"Dear {request.CustomerName},\n\n"
                     + $"Please share your feedback by clicking the link below:\n{link}\n\n"
                     + $"Thank you,\n{companyName}";

            // Example SMTP send (configure properly in Web.config)
            using (var smtp = new SmtpClient())
            {
                var mail = new MailMessage("info@logicfinders.com", request.CustomerEmail, subject, body);
                await smtp.SendMailAsync(mail);
            }

            return Ok(new { message = "Review request email sent successfully." });
        }

        // GET api/reviewrequests/5
        [HttpGet]
        [Route("api/reviewrequests/{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var r = await _db.ReviewRequests.Include(x => x.Company)
                .SingleOrDefaultAsync(x => x.RequestID == id);

            if (r == null)
                return NotFound();

            return Ok(new
            {
                r.RequestID,
                r.CustomerEmail,
                r.CustomerPhone,
                r.CustomerName,
                r.ServiceID,
                r.ServiceDate,
                r.RequestedDate,
                r.ReviewDate,
                r.CompanyId,
                CompanyName = r.Company != null ? r.Company.CompanyName : null,
                Status = r.ReviewDate == null ? "Awaiting review" : "Review received"
            });
        }

        // GET api/reviewrequests/company/2?pendingOnly=true
        [HttpGet]
        [Route("api/reviewrequests/company/{companyId:int}")]
        public async Task<IHttpActionResult> GetForCompany(int companyId, bool pendingOnly = false)
        {
            var query = _db.ReviewRequests.Where(r => r.CompanyId == companyId);

            if (pendingOnly)
                query = query.Where(r => r.ReviewDate == null);

            var items = await query
                .OrderByDescending(r => r.RequestedDate)
                .Select(r => new
                {
                    r.RequestID,
                    r.CustomerEmail,
                    r.CustomerPhone,
                    r.CustomerName,
                    r.ServiceID,
                    r.ServiceDate,
                    r.RequestedDate,
                    r.ReviewDate,
                    Status = r.ReviewDate == null ? "Awaiting review" : "Review received"
                })
                .ToListAsync();

            return Ok(items);
        }
    }
}
