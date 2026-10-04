using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace feedbackApi.Models
{
    [Table("ReviewRequests")]
    public class ReviewRequest
    {
        [Key] public int RequestID { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ServiceID { get; set; }
        public DateTime? ServiceDate { get; set; }
        public DateTime? RequestedDate { get; set; } = DateTime.Now;
        public DateTime? ReviewDate { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))] public Company Company { get; set; }
    }
}