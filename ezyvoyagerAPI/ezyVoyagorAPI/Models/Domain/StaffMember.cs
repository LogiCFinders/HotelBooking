using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace ezyvoyagerAPI.Models.Domain
{
    [Table("StaffMember", Schema = "EZV_ADMIN")]
    public class StaffMember
    {
        [Key]
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int RegionId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int PrimaryRoleId { get; set; }
        public int SecondryRoleId { get; set; }
        public int TeamId { get; set; }
        public string StaffName { get; set; }
        public string NickName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public int DailyBookingLimit { get; set; }
        public bool isAppliedDailyBookingLimit { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
