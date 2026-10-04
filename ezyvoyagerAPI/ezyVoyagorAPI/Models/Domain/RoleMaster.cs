using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace ezyvoyagerAPI.Models.Domain
{
    [Table("EZV_ADMIN.RoleMaster")]
    public class RoleMaster
    {

        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }

        // If this is meant to be a collection of role names, 
        // consider changing to List<string> or a related entity.
        public string Roles { get; set; }

        public int RightId { get; set; }
        public int BranchId { get; set; }
    }
}
