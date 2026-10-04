using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Hall
{
    [Table("HallServiceCategory")]
    public class HallServiceCategory
    {
        [Key]
        public int ServiceCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int BranchId { get; set; }
    }
}