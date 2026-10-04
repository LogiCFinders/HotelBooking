using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezyvoyagerAPI.Models.Domain
{
    [Table("EZV_ADMIN.TripTraveDates")]
    public class TravelDate
    {
        [Key]
        public Int64 Id { get; set; } = 0;
        public Int64 VoyagerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int32 NoOfSeats { get; set; }
        public bool isSeason { get; set; }
        public Int32 Markup { get; set; }
        public bool StopSell { get; set; }
        public bool IsFixedMarkup { get; set; }
    }
}