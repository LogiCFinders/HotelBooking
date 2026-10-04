using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Models.Domain
{
    [Table("EZV_ADMIN.StaffLogins")]
    public class StaffLogin
    {
        [Key]
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("StaffMember")]   // <-- explicitly link to StaffMember
        public int StaffId { get; set; }
        // Navigation property
        public virtual StaffMember StaffMember { get; set; }
    }
}
