using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezyvoyagerAPI.Models.Domain
{
    [Table("EZV_ADMIN.TripInc_Exc")]
    public class InclusionsExclusion
    {
        [Key]
        public Int64 ID { get; set; } = 0;
        public Int64 VoyagerID { get; set; }
        public string Description { get; set; }

        public Boolean IsInclusions { get; set; }
        public Boolean IsActive { get; set; }
    }
}