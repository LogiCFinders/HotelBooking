using feedbackApi.Data.DBContext;
using feedbackApi.Data.DTOs;
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
    public class StatsController : ApiController
    {
        private readonly FeedbackDbContext _db;
        public StatsController() : this(new FeedbackDbContext()) { }
        public StatsController(FeedbackDbContext db) => _db = db;

        // GET api/stats/summary
        [HttpGet]
        [Route("api/stats/summary")]
        public async Task<IHttpActionResult> GetSummary(int? companyId = null)
        {
            var reviews = _db.Reviews.AsQueryable();
            if (companyId.HasValue && companyId > 0)
                reviews = reviews.Where(r => r.ReviewFor == companyId);

            var total = await reviews.CountAsync();
            var approved = await reviews.CountAsync(r => r.PublishedDate != null);
            var rejected = await reviews.CountAsync(r => r.ReasonNotApproved != null && r.PublishedDate == null);
            var pending = total - approved - rejected;

            var since = DateTime.Today.AddDays(-29);
            var rawDaily = await reviews
                     .Where(r => r.DateofReview >= since)
                     .GroupBy(r => DbFunctions.TruncateTime(r.DateofReview))
                     .Select(g => new { Day = g.Key.Value, Count = g.Count() }) // unwrap nullable
                     .ToListAsync();

            var daily = Enumerable.Range(0, 30)
                     .Select(i => since.AddDays(i))
                     .Select(d => new DailyCount(
                         d.ToString("dd MMM"),
                         rawDaily.FirstOrDefault(x => x.Day == d.Date)?.Count ?? 0))
                     .ToList();

            var rawBuckets = await reviews
                .GroupBy(r => r.Rating)
                .Select(g => new { Rating = g.Key, Count = g.Count() })
                .ToListAsync();

            var buckets = Enumerable.Range(1, 5)
                .Select(r => new RatingBucket(r, rawBuckets.FirstOrDefault(x => x.Rating == r)?.Count ?? 0))
                .ToList();

            return Ok(new Summary(total, approved, pending, rejected, daily, buckets));
        }
    }
}
