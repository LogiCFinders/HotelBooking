using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ezyvoyagerAPI.Models.DTOs;
using ezyvoyagerAPI.Models.Domain;
using ezyvoyagerAPI.common;

namespace ezyvoyagerAPI.Repositories.Interfaces
{
    public interface IStaffRepository
    {
        Task<StaffMember> AddStaffAsync(RegisterDto dto);
        Task<StaffMember> GetStaffByIdAsync(int staffId);
        Task<StaffMember> GetStaffByEmailAsync(string email);

        Task<Company> GetCompanyByIdAsync(int companyId);
        Task<RoleMaster> GetRoleByIdAsync(int roleId);
        Task<Branch> GetBranchByIdAsync(int branchId);
        Task<Department> GetDepartmentByIdAsync(int departmentId);

        Task<ServiceResult> RegisterStaffAsync(RegisterDto dto);
    }
    public interface ILoginRepository
    {
        Task<StaffLogin> CreateLoginAsync(RegisterDto dto, int staffId);
        Task<StaffLogin> GetLoginAsync(string loginName);
        bool VerifyPassword(string inputPassword, string storedHash);
        string GenerateTempPassword();
        Task UpdatePasswordAsync(int staffId, string newPassword);
    }
}
