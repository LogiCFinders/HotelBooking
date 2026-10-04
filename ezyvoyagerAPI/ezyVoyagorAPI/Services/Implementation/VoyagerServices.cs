using ezyvoyagerAPI.Models.DTOs;
using ezyvoyagerAPI.Models.Domain;
using ezyvoyagerAPI.Repositories.Implementation;
using ezyvoyagerAPI.Repositories.Interfaces;
using ezyvoyagerAPI.Services.Interfaces;
using ezyvoyagerAPI.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Runtime.Remoting.Messaging;

namespace ezyvoyagerAPI.Services.Implementation
{
    public class VoyagerServices : IVoyagerServices
    {
        private readonly IVoyagerRepository _voyagerRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly ICompanyRepository _companyRepository;
        public VoyagerServices()
        {
            _voyagerRepository = new VoyagerRepository();
            _staffRepository = new StaffRepository();
            _loginRepository= new LoginRepository();
            _companyRepository = new CompanyRepository();
        }
        public VoyagerServices(IVoyagerRepository voyagerRepository, IStaffRepository staffRepository, ILoginRepository loginRepository)
        {
            _voyagerRepository = voyagerRepository;
            _staffRepository = staffRepository;
            _loginRepository = loginRepository;
        }

        public async Task<VoyagerItenaryAddREQ> AddVoyager(VoyagerItenaryAddREQ voyagerAddREQ)
        {
            return await _voyagerRepository.AddVoyagerItenary(voyagerAddREQ);
        }
        public async Task<VoyagerItenaryAddREQ> getVoyagerItenary(Int64 VoyagerItenaryId)
        {
            return await _voyagerRepository.getVoyagerItenary(VoyagerItenaryId);
        }
        public async Task<VoyagerRESP> GetVoyagerItenary()
        {
            //await Task.Delay(500); // Simulate network latency

            return await _voyagerRepository.GetVoyagerItenary();


        }

        public async Task<LoginRESP> Login(LoginREQ loginREQ)
        {
            return await _voyagerRepository.Login(loginREQ);
        }

        public async Task<VoyagerStaticDataRESP> GetStaticData(VoyagerStaticDataREQ REQ)
        {
            return await _voyagerRepository.GetStaticData(REQ);
        }
        ///Auth
        ///
        // ✅ Registration via repositories
        public async Task<ServiceResult> RegisterStaffAsync(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return ServiceResult.Failure("Passwords do not match");
            
           return await _staffRepository.RegisterStaffAsync(dto);
        }

        // ✅ Login via repositories
        public async Task<ServiceResult> LoginAsync(LoginDto dto)
        {
            var login = await _loginRepository.GetLoginAsync(dto.LoginName);
            if (login == null || !_loginRepository.VerifyPassword(dto.Password, login.Password))
                return ServiceResult.Failure("Invalid credentials");

            var staff = await _staffRepository.GetStaffByIdAsync(login.StaffId);
            if (staff == null)
                return ServiceResult.Failure("Staff record not found");

            // Fetch related entities
            var company = await _staffRepository.GetCompanyByIdAsync(staff.CompanyId);
            var role = await _staffRepository.GetRoleByIdAsync(staff.PrimaryRoleId);
            var branch = await _staffRepository.GetBranchByIdAsync(staff.BranchId);
            var department = await _staffRepository.GetDepartmentByIdAsync(staff.DepartmentId);

            var loginTime = DateTime.Now;

            return ServiceResult.Success(new
            {
                Id= staff.Id,
                StaffName = staff.StaffName,
                NickName = staff.NickName,
                Email = staff.Email,
                Phone1 = staff.Phone1,
                Phone2 = staff.Phone2,
                CompanyId = staff.CompanyId,
                CompanyName=company.Name,
                BranchId = branch.Id,
                BranchName=branch.BranchName,
                RegionId = staff.RegionId,
                DepartmentId = staff.DepartmentId,
                DepartMentName = department.DepartmentName,
                DesignationId = staff.DesignationId,
                PrimaryRoleId = staff.PrimaryRoleId,
                SecondryRoleId = staff.SecondryRoleId,
                rolesId=role.Id,
                roles= role.Roles,
                TeamId = staff.TeamId,
                DailyBookingLimit = staff.DailyBookingLimit,
                isAppliedDailyBookingLimit = staff.isAppliedDailyBookingLimit,
                IsActive = true,
                IsDeleted = false,
                LoginTime=loginTime 
            }, "Login successfully");
        }

        // ✅ Forget Password via repositories
        public async Task<ServiceResult> ForgetPasswordAsync(ForgetPasswordDto dto)
        {
            var staff = await _staffRepository.GetStaffByEmailAsync(dto.Email);
            if (staff == null) return ServiceResult.Failure("Email not registered");

            var tempPassword = _loginRepository.GenerateTempPassword();
            await _loginRepository.UpdatePasswordAsync(staff.Id, tempPassword);

            // TODO: Send tempPassword via email/SMS
            return ServiceResult.Success(message: $"Temporary password sent to {dto.Email}");
        }

        // 🔒 Helpers
        private string HashPassword(string password) =>
            Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));

        private bool VerifyPassword(string inputPassword, string storedHash) =>
            HashPassword(inputPassword) == storedHash;

        public async Task<CompanyDTO> getCompanyDetails(Int64 companyId)
        {
            return await _companyRepository.GetByIdAsync(companyId);
        }
        public async Task<CompanyDTO> UpdateCompanyDetails(CompanyDTO company)
        {
            return  await  _companyRepository.UpdateAsync(company);
        }

        public async Task<TemplateDTO> getCompanyTemplate(string xApi)
        {
            return await _companyRepository.getCompanyTemplate(xApi);
        }
    }
}