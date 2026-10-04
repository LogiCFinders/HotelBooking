using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezyvoyagerAPI.Models.Domain
{

    [Table("EZV_ADMIN.TripItinerary")]
    public class Itinerary
    {
        [Key]
        public Int64 ID { get; set; } = 0;
        public Int64 VoyagerID { get; set; }

        public Int32 DayNo { get; set; }
        public string ItineraryDesc { get; set; }
    }
}