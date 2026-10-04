using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace feedbackApi.Models
{

    [Table("Plans")]
    public class Plan
    {
        [Key] public int PlanId { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public string PlanTitle { get; set; }
        public int ThresholdLimit { get; set; }          // 0 = unlimited
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        [Column(TypeName = "decimal(10,2)")] public decimal MonthlyCost { get; set; }
        [Column(TypeName = "decimal(5,2)")] public decimal DiscountonYearly { get; set; }
    }

}