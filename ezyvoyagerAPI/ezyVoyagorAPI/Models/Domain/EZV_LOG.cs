using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezyvoyagerAPI.Models.Domain
{
    [Table("EZV_LOG", Schema = "EZV_ADMIN")]
    public class EZV_LOG
    {
        [Key]
        public long ID { get; set; }   // BIGINT in SQL

        [Required]
        [StringLength(50)]
        public string Source { get; set; }   // nvarchar(50)

        [StringLength(15)]
        public string Event { get; set; }    // nvarchar(15)
        public Int64 LoggedInUserId { get; set; }

        public string LogDescription { get; set; }   // nvarchar(max)

        [Required]
        public DateTime LogDate { get; set; }   // datetime
    }
}
