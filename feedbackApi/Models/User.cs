using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace feedbackApi.Models
{

    [Table("Users")]
    public class User
    {
        [Key] public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; }
        public string Role { get; set; } = "CompanyUser";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastLogin { get; set; }
        public bool IsEmailVerified { get; set; }
        public string EmailVerificationToken { get; set; }

        [ForeignKey(nameof(CompanyId))] public Company Company { get; set; }
    }
}