using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ezyvoyagerAPI.Models.Domain;
using ezyvoyagerAPI.Models.DTOs;

namespace ezyvoyagerAPI.Services.Interfaces
{
    public interface ILogService
    {
        Task<EZV_LOG> CreateLogAsync(LogDto dto);
    }
}
