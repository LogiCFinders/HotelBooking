using System.ComponentModel.DataAnnotations;

namespace ezyvoyagerAPI.Models.Domain
{
    public class Voyager
    {
        [Key]
        public int id { get; set; } = 0;
        public int VoyagerID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GroupType { get; set; }
        public string Duration { get; set; }
        public string DurationUnit { get; set; }
        public string Rating { get; set; }
        public string CancellationPolicy { get; set; }
        public string PaymentPolicy { get; set; }
        public string Frequency { get; set; }
        public string CountryCode { get; set; }
        public string PriceType { get; set; }
        public string MonthsOfSeasons { get; set; }
        public string BestTime { get; set; }
        public string OtherTerms { get; set; }
        public string ActivityType { get; set; }
        public string PlaceOfActivity { get; set; }
        public string Grade { get; set; }
        public string StopSell { get; set; }
        public string Theme { get; set; }
    }
}