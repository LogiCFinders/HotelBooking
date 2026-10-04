using System;
using System.Threading.Tasks;
using ezyvoyagerAPI.Repositories;
using ezyvoyagerAPI.Models;
using ezyvoyagerAPI.Models.Domain;
using ezyvoyagerAPI.Models.DTOs;
using ezyvoyagerAPI.Services.Interfaces;
using ezyvoyagerAPI.Repositories.Interfaces;
using ezyvoyagerAPI.Repositories.Implementation;


namespace ezyvoyagerAPI.Services.Implementation
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        public LogService()
        {
            _logRepository = new LogRepository();
        }
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<EZV_LOG> CreateLogAsync(LogDto dto)
        {
            var log = new EZV_LOG
            {
                Source = dto.Source,
                Event= dto.Event,
                LoggedInUserId= dto.LoggedInUserId,
                LogDescription = dto.LogDescription,
                LogDate = DateTime.Now
            };

            return await _logRepository.AddLogAsync(log);
        }
    }

   
}
