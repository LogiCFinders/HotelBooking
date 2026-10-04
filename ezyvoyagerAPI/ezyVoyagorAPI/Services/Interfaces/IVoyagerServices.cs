using ezyvoyagerAPI.Models.DTOs;
using ezyvoyagerAPI.common;
using System;
using System.Threading.Tasks;
using ezyvoyagerAPI.Models.Domain;

namespace ezyvoyagerAPI.Services.Interfaces
{
    public interface IVoyagerServices
    {

        Task<VoyagerRESP> GetVoyagerItenary();
        Task<VoyagerItenaryAddREQ> AddVoyager(VoyagerItenaryAddREQ voyagerAddREQ);
        Task<VoyagerItenaryAddREQ> getVoyagerItenary(Int64 VoyagerItenaryId);
        Task<LoginRESP> Login(LoginREQ loginREQ);

        Task<VoyagerStaticDataRESP> GetStaticData(VoyagerStaticDataREQ REQ);

        //Auth
        Task<ServiceResult> RegisterStaffAsync(RegisterDto dto);
        Task<ServiceResult> LoginAsync(LoginDto dto);
        Task<ServiceResult> ForgetPasswordAsync(ForgetPasswordDto dto);

        //Conpany
        Task<CompanyDTO> getCompanyDetails(Int64 companyId);
        Task<CompanyDTO> UpdateCompanyDetails(CompanyDTO company);

        Task<TemplateDTO> getCompanyTemplate(string xapiKey);
    }
}
