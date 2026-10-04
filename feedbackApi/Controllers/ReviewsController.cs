using feedbackApi.Data.DBContext;
using feedbackApi.Data.DTOs;
using feedbackApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace feedbackApi.Controllers
{
    public class ReviewsController : ApiController
    {
        private readonly FeedbackDbContext _db;
        public ReviewsController() : this(new FeedbackDbContext()) { }
        //private readonly IWebHostEnvironment _env;
        private static string StatusOf(Review r) =>  r.PublishedDate != null ? "Approved" :  r.ReasonNotApproved != null ? "Rejected" : "Pending";
        public ReviewsController(FeedbackDbContext db)
        {
            _db = db;
            _db.Configuration.LazyLoadingEnabled = true;

        }

        private ReviewDto ToDto(Review r)
        {
            return new ReviewDto(
                r.ReviewId,
                r.Rating,
                r.ReviewTitle,
                r.ReviewText,
                r.DateofService,
                r.DateofReview,
                r.PublishedDate,
                r.ApprovedBy,
                r.ReasonNotApproved,
                r.ReviewFor,
                r.Company != null ? r.Company.CompanyName : string.Empty,
                r.ReviewByName,
                r.ReviewByEmail,
                r.ServiceId,
                r.ReviewJson,
                StatusOf(r),
                r.Replies
                    .OrderBy(x => x.ReplyDate)
                    .Select(x => new ReplyDto(x.ReplyId, x.ReplyText, x.ReplyDate))
                    .ToList(),
                r.MediaItems
                    .Where(m => m.IsPublished || StatusOf(r) != "Approved")
                    .Select(m => new MediaDto(
                        m.MediaID,
                        m.MediaType,
                        // Web API 2 doesn’t have Request.Scheme/Host, so build manually
                        $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}/{m.MediaPath}",
                        m.IsPublished))
                    .ToList()
            );
        }


        // POST api/reviews - public submission from the review form
        [HttpPost]
        [Route("api/reviews")]
        public async Task<IHttpActionResult> Submit(ReviewSubmitDto dto)
        {
            var companyExists = await _db.Companies
                .AnyAsync(c => c.CompanyId == dto.CompanyId && c.IsActive);

            if (!companyExists)
            {
                return ResponseMessage(Request.CreateResponse(
                    System.Net.HttpStatusCode.NotFound,
                    new { message = "Company not found." }));
            }

            var review = new Review
            {
                ReviewFor = dto.CompanyId,
                Rating = dto.Rating,
                ReviewTitle = dto.ReviewTitle,
                ReviewText = dto.ReviewText,
                DateofService = (DateTime)dto.DateofService,
                DateofReview = DateTime.Now,
                ReviewByEmail = dto.ReviewByEmail,
                ReviewByName = dto.ReviewByName,
                ServiceId = dto.ServiceId,
                ReviewJson = dto.ReviewJson
            };

            _db.Reviews.Add(review);

            // If this review answers a pending ReviewRequest, stamp its ReviewDate
            if (!string.IsNullOrWhiteSpace(dto.ServiceId))
            {
                var request = await _db.ReviewRequests.FirstOrDefaultAsync(x =>
                    x.CompanyId == dto.CompanyId &&
                    x.ServiceID == dto.ServiceId &&
                    x.ReviewDate == null);

                if (request != null)
                    request.ReviewDate = DateTime.Now;
            }

            await _db.SaveChangesAsync();

            return Ok(new { review.ReviewId, message = "Review submitted and awaiting approval." });
        }
        // GET api/reviews/company/1?status=Approved
        [HttpGet]
        [Route("api/reviews/company/{companyId:int}")]
     
        
        public async Task<IHttpActionResult>  GetForCompany(int companyId, string status = "Approved")
        {
            var query = _db.Reviews
                .Include("Company")
                .Include("Replies")
                .Include("MediaItems")
                .Where(r => r.ReviewFor == companyId);

            if (!string.IsNullOrEmpty(status))
            {
                var s = status.ToLower();
                if (s == "approved")
                    query = query.Where(r => r.PublishedDate != null);
                else if (s == "pending")
                    query = query.Where(r => r.PublishedDate == null && r.ReasonNotApproved == null);
                else if (s == "rejected")
                    query = query.Where(r => r.ReasonNotApproved != null);
            }

            // If EF6.1+ async is available:
            // var list = await query.OrderByDescending(r => r.DateofReview).ToListAsync();

            // Otherwise, fallback to sync:
            var list = await query.OrderByDescending(r => r.DateofReview).ToListAsync();

            return Ok(list.Select(ToDto));
        }



        // GET api/reviews?status=All|Pending|Approved|Rejected
        [HttpGet]
        [Route("api/reviews")]
        public async Task<IHttpActionResult> GetAll(string status = "All")
        {
            var query = _db.Reviews
                .Include(r => r.Company)
                .Include(r => r.Replies)
                .Include(r => r.MediaItems)
                .AsQueryable();

            // Replace switch expression with if/else
            if (!string.IsNullOrEmpty(status))
            {
                var s = status.ToLower();
                if (s == "approved")
                    query = query.Where(r => r.PublishedDate != null);
                else if (s == "pending")
                    query = query.Where(r => r.PublishedDate == null && r.ReasonNotApproved == null);
                else if (s == "rejected")
                    query = query.Where(r => r.ReasonNotApproved != null);
                // "all" just leaves query unchanged
            }

            var list = await query
                .OrderByDescending(r => r.DateofReview)
                .ToListAsync();

            return Ok(list.Select(ToDto));
        }

        // GET api/reviews/pending - admin moderation queue (all companies)
        [HttpGet]
        [Route("api/reviews/pending")]
        public async Task<IHttpActionResult> GetPending()
        {
            var list = await _db.Reviews
                .Include(r => r.Company)
                .Include(r => r.Replies)
                .Include(r => r.MediaItems)
                .Where(r => r.PublishedDate == null && r.ReasonNotApproved == null)
                .OrderBy(r => r.DateofReview)
                .ToListAsync();

            return Ok(list.Select(ToDto));
        }
        // PUT api/reviews/5/reject
        [HttpPut]
        [Route("api/reviews/{id:int}/reject")]
        public async Task<IHttpActionResult> Reject(int id, RejectDto dto)
        {
            var review = await _db.Reviews.FindAsync(id);

            if (review == null)
                return NotFound();

            review.PublishedDate = DateTime.MinValue;
            review.ApprovedBy = dto.AdminName;
            review.ReasonNotApproved = dto.Reason;

            await _db.SaveChangesAsync();

            return Ok(new { message = "Review rejected." });
        }
        // POST api/reviews/5/reply
        [HttpPost]
        [Route("api/reviews/{id:int}/reply")]
        public async Task<IHttpActionResult> AddReply(int id, ReplyCreateDto dto)
        {
            var exists = await _db.Reviews.AnyAsync(r => r.ReviewId == id);
            if (!exists)
                return NotFound();

            var reply = new Reply
            {
                ReviewID = id,
                ReplyText = dto.ReplyText,
                ReplyDate = DateTime.Now
            };

            _db.Replies.Add(reply);
            await _db.SaveChangesAsync();

            return Ok(new { reply.ReplyId });
        }
        // POST api/reviews/5/media - multipart upload (photo/video)
        [HttpPost]
        [Route("api/reviews/{id:int}/media")]
        public async Task<IHttpActionResult> UploadMedia(int id)
        {
            var httpRequest = HttpContext.Current.Request;
            if (!await _db.Reviews.AnyAsync(r => r.ReviewId == id))
                return NotFound();

            if (httpRequest.Files.Count == 0)
            {
                return ResponseMessage(Request.CreateResponse(
                    System.Net.HttpStatusCode.BadRequest,
                    new { message = "No file received." }));
            }

            var file = httpRequest.Files[0];
            if (file == null || file.ContentLength == 0)
                return ResponseMessage(Request.CreateResponse(
                    System.Net.HttpStatusCode.BadRequest,
                    new { message = "No file received." }));

            var allowedImage = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var allowedVideo = new[] { ".mp4", ".webm", ".mov" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            string mediaType;
            if (allowedImage.Contains(ext)) mediaType = "Photo";
            else if (allowedVideo.Contains(ext)) mediaType = "Video";
            else
                return ResponseMessage(Request.CreateResponse(
                   System.Net.HttpStatusCode.BadRequest,
                   new { message = "Only images(jpg / png / gif / webp) or videos(mp4 / webm / mov) are allowed."}));
           

            var uploads = HttpContext.Current.Server.MapPath("~/uploads");
            Directory.CreateDirectory(uploads);

            var fileName = $"{Guid.NewGuid():N}{ext}";
            var filePath = Path.Combine(uploads, fileName);
            file.SaveAs(filePath);

            var media = new Media
            {
                ReviewID = id,
                MediaType = mediaType,
                MediaPath = $"uploads/{fileName}"
            };

            _db.MediaItems.Add(media);
            await _db.SaveChangesAsync();

            var url = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Authority}/uploads/{fileName}";
            return Ok(new { media.MediaID, url });
        }

        // PUT api/reviews/5/approve
        [HttpPut]
        [Route("api/reviews/{id:int}/approve")]
        public async Task<IHttpActionResult> Approve(int id, ApproveDto dto)
        {
            var review = await _db.Reviews
                .Include(r => r.MediaItems)
                .SingleOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
                return NotFound();

            review.PublishedDate = DateTime.Now;
            review.ApprovedBy = dto.AdminName;
            review.ReasonNotApproved = null;

            foreach (var m in review.MediaItems)
            {
                m.IsPublished = true;
                m.PublishedDate = DateTime.Now;
            }

            await _db.SaveChangesAsync();

            return Ok(new { message = "Review approved and published." });
        }


        [HttpGet]
        [Route("api/RNScore/{companyGuid}")]
        public async Task<IHttpActionResult> GetRNScore(Guid companyGuid)
        {
            var company = await _db.Companies
                .FirstOrDefaultAsync(c => c.CompanyGuid == companyGuid);

            if (company == null) { 
            return ResponseMessage(Request.CreateResponse(
                 System.Net.HttpStatusCode.NotFound,
                 new { message = "Company not found." }));
            }

            var reviews = await _db.Reviews
                .Where(r => r.ReviewFor == company.CompanyId && r.PublishedDate != null)
                .ToListAsync();

            var totalCount = reviews.Count;
            var rnScore = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

            var scores = Enumerable.Range(1, 5)
                .Select(score => new {
                    Score = score.ToString(),
                    Count = reviews.Count(r => r.Rating == score).ToString()
                }).ToList();

            var response = new
            {
                CompanyName = company.CompanyName,
                RNScore = rnScore.ToString("0.0"),
                TotalCount = totalCount,
                Scores = scores
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("api/GetReviews/{companyGuid}")]
        public async Task<IHttpActionResult> GetCompanyReviews(Guid companyGuid, int pageCount = 1, int records = 10)
        {
            var company = await _db.Companies
                .FirstOrDefaultAsync(c => c.CompanyGuid == companyGuid);

            if (company == null)
            {
                return ResponseMessage(Request.CreateResponse(
                     System.Net.HttpStatusCode.NotFound,
                     new { message = "Company not found." }));
            }


            var reviewsQuery = _db.Reviews
                .Include(r => r.MediaItems) // assuming navigation property
                .Where(r => r.ReviewFor == company.CompanyId && r.PublishedDate != null)
                .OrderByDescending(r => r.DateofReview);

            var totalCount = await reviewsQuery.CountAsync();
            var rnScore = totalCount > 0 ? await reviewsQuery.AverageAsync(r => r.Rating) : 0;

            // Pagination
            var reviews = await reviewsQuery
                .Skip((pageCount - 1) * records)
                .Take(records)
                .ToListAsync();

            var reviewList = reviews.Select(r => new {
                Review = r.ReviewId.ToString(),
                Description = r.ReviewText,
                Score = r.Rating.ToString(),
                ReviewDate = r.DateofReview,
                ReviewBy = r.ReviewByName,
                Media = r.MediaItems.Select(m => new {
                    URL = m.MediaPath,
                    Type = m.MediaType
                    
                }).ToList()
            }).ToList();

            var response = new
            {
                CompanyName = company.CompanyName,
                RNScore = rnScore.ToString("0.0"),
                TotalCount = totalCount,
                PageCount = pageCount,
                Records = records,
                Reviews = reviewList
            };

            return Ok(response);
        }

    }
}
