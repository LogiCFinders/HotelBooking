using feedbackApi.Data.DBContext;
using feedbackApi.Data.DTOs;
using feedbackApi.Models;
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
    public class QuestionsController : ApiController
    {
        private readonly FeedbackDbContext _db;
        public QuestionsController() : this(new FeedbackDbContext()) { }
        public QuestionsController(FeedbackDbContext db) => _db = db;

        [HttpGet]
        [Route("api/company/{companyId:int}/questions")]
        public async Task<IHttpActionResult> GetForCompany(int companyId)
        {
            var questions = await _db.Questions
                .Where(q => q.CompanyID == companyId)
                .OrderBy(q => q.QSrNo)
                .ToListAsync();

            var result = questions.Select(q => new QuestionDto(
                q.QuestionID,
                q.QSrNo,
                q.QText,
                q.QType,
                new[] { q.AnsOp1, q.AnsOp2, q.AnsOp3, q.AnsOp4, q.AnsOp5, q.AnsOp6 }
                    .Where(o => !string.IsNullOrWhiteSpace(o))
                    .Select(o => o)   // no need for `o!` in .NET Framework
                    .ToList()
            ));

            return Ok(result);
        }
        [HttpPost]
        [Route("api/questions")]
        public async Task<IHttpActionResult> Create(Question question)
        {
            _db.Questions.Add(question);
            await _db.SaveChangesAsync();

            // Build URI for the newly created resource using DefaultApi route
            var location = new Uri(Url.Link("DefaultApi", new { controller = "Question", id = question.QuestionID }));

            return Created(location, new { question.QuestionID });
        }
    }
}
