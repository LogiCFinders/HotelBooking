using System.Data.Entity;
using System.Threading.Tasks;
using ezyvoyagerAPI.Models;
using ezyvoyagerAPI.Models.Domain;
using ezyvoyagerAPI.Models.DTOs;
using ezyvoyagerAPI.Data;
using ezyvoyagerAPI.common;
using ezyvoyagerAPI.Repositories.Interfaces;
using ezyvoyagerAPI.Helper;

namespace ezyvoyagerAPI.Repositories.Implementation
{
    public class StaffRepository : IStaffRepository
    {
        private VoyagerDbContext _context;
        public StaffRepository()
        {
            _context = new VoyagerDbContext(); ;
        }
        public StaffRepository(VoyagerDbContext context)
        {
            _context = context;
        }

        public async Task<StaffMember> AddStaffAsync(RegisterDto dto)
        {
            var staff = new StaffMember
            {
                StaffName = dto.StaffName,
                Email = dto.Email,
                CompanyId = dto.CompanyId,
                IsActive = true,
                IsDeleted = false
            };

            _context.StaffMembers.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<StaffMember> GetStaffByIdAsync(int staffId)
        {
            return await _context.StaffMembers.FirstOrDefaultAsync(s => s.Id == staffId);
        }

        public async Task<StaffMember> GetStaffByEmailAsync(string email)
        {
            return await _context.StaffMembers.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<Company> GetCompanyByIdAsync(int companyId)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Id == companyId );
        }

        public async Task<RoleMaster> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<Branch> GetBranchByIdAsync(int branchId)
        {
            return await _context.Branches.FirstOrDefaultAsync(b => b.Id == branchId);
        }

        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<ServiceResult> RegisterStaffAsync(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return ServiceResult.Failure("Passwords do not match");
            // ✅ Check if email already exists
            bool emailExists = await _context.StaffMembers
                .AnyAsync(s => s.Email == dto.Email && !s.IsDeleted);

            if (emailExists)
                return ServiceResult.Failure("An account with this email already exists");

            // ✅ Check if username already exists (optional)
            bool usernameExists = await _context.StaffLogins
                .AnyAsync(l => l.LoginName == dto.LoginName && l.IsActive);

            if (usernameExists)
                return ServiceResult.Failure("This username is already taken");
            var staff = new StaffMember
            {
                StaffName = dto.StaffName,
                NickName = dto.NickName,
                Email = dto.Email,
                Phone1 = dto.Phone1,
                Phone2 = dto.Phone2,
                CompanyId = dto.CompanyId,
                BranchId = dto.BranchId,
                RegionId = dto.RegionId,
                DepartmentId = dto.DepartmentId,
                DesignationId = dto.DesignationId,
                PrimaryRoleId = dto.PrimaryRoleId,
                SecondryRoleId = dto.SecondryRoleId,
                TeamId = dto.TeamId,
                DailyBookingLimit = dto.DailyBookingLimit,
                isAppliedDailyBookingLimit = dto.isAppliedDailyBookingLimit,
                IsActive = true,
                IsDeleted = false
            };

            _context.StaffMembers.Add(staff);
            await _context.SaveChangesAsync();

            var login = new StaffLogin
            {
                LoginName = dto.LoginName,
                Password = PasswordHelper.HashPassword(dto.Password),
                IsActive = true,
                StaffId = staff.Id
            };

            _context.StaffLogins.Add(login);
            await _context.SaveChangesAsync();

            return ServiceResult.Success(new
            {
                staff.Id,
                staff.StaffName,
                login.LoginName
            }, "Registered successfully");
        }

    }
}
