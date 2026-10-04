using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Hall
{
    [Table("HallServices")]
    public class HallServices
    {
        [Key]
        public int ServiceId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal COST { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxAmount { get; set; }
        public string ContentType { get; set; }
        public byte[] StreamData { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int BranchId { get; set; }
        public string Category { get; set; }
        [NotMapped]
        public bool isSelected { get; set; }


    }
}