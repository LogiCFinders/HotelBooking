using feedbackApi.Data.DBContext;
using feedbackApi.Data.DTOs;
using feedbackApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace feedbackApi.Controllers
{
    public class CompaniesController : ApiController
    {
        private readonly FeedbackDbContext _db;
        public CompaniesController() : this(new FeedbackDbContext()) { }
        public CompaniesController(FeedbackDbContext db) => _db = db;

        [HttpGet]
        [Route("api/companies")]
        public async Task<IHttpActionResult> GetAll()
        {
            var companies = await _db.Companies
                .Where(c => c.IsActive)
                .OrderBy(c => c.CompanyName)
                .Select(c => new
                {
                    c.CompanyId,
                    c.CompanyName,
                    c.BusinessCategory,
                    c.BusinessSubCategory,
                    c.Country
                })
                .ToListAsync();

            return Ok(companies);
        }
        [HttpGet]
        [Route("api/companies/{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var c = await _db.Companies.FindAsync(id);
            if (c == null)
                return NotFound();

            return Ok(new
            {
                c.CompanyId,
                c.CompanyName,
                c.Address1,
                c.Address2,
                c.Country,
                c.Email,
                c.Phone,
                c.BusinessCategory,
                c.BusinessSubCategory
            });
        }
        // GET api/companies/5/detail
        [HttpGet]
        [Route("api/companies/{id:int}/detail")]
        public async Task<IHttpActionResult> GetDetail(int id)
        {
            var c = await _db.Companies.FindAsync(id);
            if (c == null)
                return NotFound();

            return Ok(new
            {
                c.CompanyId,
                c.CompanyName,
                c.Address1,
                c.Address2,
                c.PostalCode,
                c.Country,
                c.Phone,
                c.Email,
                c.ContactName,
                c.ContactNumber,
                c.BusinessCategory,
                c.BusinessSubCategory,
                c.Remarks,
                c.BillingType,
                c.BillingRate,
                c.BillingDate,
                c.DateCreated,
                c.DateUpdated,
                c.IsActive
            });
        }
        [HttpPut]
        [Route("api/companies/{id:int}")]
        public async Task<IHttpActionResult> Update(int id, CompanyUpdateDto dto)
        {
            var c = await _db.Companies.FindAsync(id);
            if (c == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(dto.CompanyName))
            {
                return ResponseMessage(Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new { message = "Company name is required." }));
            }

            c.CompanyName = dto.CompanyName.Trim();
            c.Address1 = dto.Address1;
            c.Address2 = dto.Address2;
            c.PostalCode = dto.PostalCode;
            c.Country = dto.Country;
            c.Phone = dto.Phone;
            c.Email = dto.Email;
            c.ContactName = dto.ContactName;
            c.ContactNumber = dto.ContactNumber;
            c.BusinessCategory = dto.BusinessCategory;
            c.BusinessSubCategory = dto.BusinessSubCategory;
            c.Remarks = dto.Remarks;
            c.DateUpdated = DateTime.Now;

            await _db.SaveChangesAsync();

            return Ok(new { message = "Company profile updated." });
        }
        [HttpPost]
        [Route("api/companies")]
        public async Task<IHttpActionResult> Create(Company company)
        {
            company.DateCreated = DateTime.Now;
            _db.Companies.Add(company);
            await _db.SaveChangesAsync();

            // Web API 2 doesn't have CreatedAtAction, so use Created with URI
            var location = new Uri(Url.Link("DefaultApi", new { controller = "Company", id = company.CompanyId }));

            return Created(location, new { company.CompanyId });
        }
    }
}
