using System.ComponentModel.DataAnnotations.Schema;

namespace ezyvoyagerAPI.Models.Domain
{
   
    public class LoginInfo
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string LoginName { get; set; }

        [Column("Password")]
        public string LoginPassword { get; set; }
        public bool isActive { get; set; }
       


    }
    public class LoginREQInfo
    {
        public string LoginName { get; set; } = string.Empty;
        public string LoginPassword { get; set; } = string.Empty;
        public string LoginPasswordEncrypted { get; set; } = string.Empty;


    }
}