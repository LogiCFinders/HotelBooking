using ezyvoyagerAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ezyvoyagerAPI.Models.Domain;
using ezyvoyagerAPI.Models.DTOs;

namespace ezyvoyagerAPI.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<CompanyDTO>> GetAllAsync();

        Task<CompanyDTO> GetByIdAsync(long id);

        Task<CompanyDTO> AddAsync(CompanyDTO company);

        Task<CompanyDTO> UpdateAsync(CompanyDTO company);

        Task<bool> DeleteAsync(long id);
        Task<TemplateDTO> getCompanyTemplate(string xApi);

    }
}
