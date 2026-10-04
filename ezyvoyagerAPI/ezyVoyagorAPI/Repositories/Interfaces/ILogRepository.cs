using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ezyvoyagerAPI.Models.Domain;

namespace ezyvoyagerAPI.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<EZV_LOG> AddLogAsync(EZV_LOG log);
    }
}
