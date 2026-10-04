using System;
using System.Data.Entity;
using System.Threading.Tasks;
using ezyvoyagerAPI.Models;
using ezyvoyagerAPI.Data;
using ezyvoyagerAPI.Models.Domain;
using ezyvoyagerAPI.Models.DTOs;
using ezyvoyagerAPI.Repositories.Interfaces;

namespace ezyvoyagerAPI.Repositories.Implementation
{
    public class LogRepository : ILogRepository
    {
        private VoyagerDbContext _context;
        public LogRepository()
        {
            _context = new VoyagerDbContext();
        }
        public LogRepository(VoyagerDbContext context)
        {
            _context = context;
        }

        public async Task<EZV_LOG> AddLogAsync(EZV_LOG log)
        {
            _context.EZV_LOG.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }
    }

   
}
