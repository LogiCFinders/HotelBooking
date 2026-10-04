using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Models.DTOs
{
    public class RegisterDto
    {
        public string StaffName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int RegionId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int PrimaryRoleId { get; set; }
        public int SecondryRoleId { get; set; }
        public int TeamId { get; set; }
        public int DailyBookingLimit { get; set; }
        public bool isAppliedDailyBookingLimit { get; set; }

        public string LoginName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class LoginDto
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }

    public class ForgetPasswordDto
    {
        public string Email { get; set; }
    }

}