using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezyvoyagerAPI.Models.Domain
{
    [Table("EZV_ADMIN.TripFaqs")]
    public class VoyagerFAQ
    {
        [Key]
        public Int64 id { get; set; } = 0;
        public Int64 VoyagerID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}