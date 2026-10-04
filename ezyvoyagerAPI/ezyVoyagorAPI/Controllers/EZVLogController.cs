using System.Threading.Tasks;
using System.Web.Http;
using ezyvoyagerAPI.Services.Interfaces;
using ezyvoyagerAPI.Services;
using ezyvoyagerAPI.Services.Implementation;
using System;
using ezyvoyagerAPI.Models.DTOs;

namespace ezyvoyagerAPI.Controllers
{
    [RoutePrefix("api/logs")]
    public class EZVLogController : ApiController
    {
        private readonly ILogService _logService;

        public EZVLogController()
        {
            _logService = new LogService();
        }
        public EZVLogController(ILogService logService)
        {
            _logService = logService;
        }

        // POST api/logs/create
        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateLog(LogDto dto)
        {
            var log = await _logService.CreateLogAsync(dto);
            return Ok(log);
        }
    }

    
}

