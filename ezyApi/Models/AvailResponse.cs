using System.ComponentModel.DataAnnotations;

namespace ezyApi.Models
{
   

        public class AvailResponse
        {
        [Key]
        public int ResponseKey { get; set; }
        public Errors Errors { get; set; }
            public Hotelinfoary[] HotelInfoAry { get; set; }
        }

        public class Errors
        {
        [Key]
            public string Code { get; set; }
            public string Description { get; set; }
        }

        public class Hotelinfoary
        {
            [Key]
            public int HotelID { get; set; }
            public string HotelName { get; set; }
            public string HotelDescription { get; set; }
            //public string ChainCode { get; set; }
            public string ChainName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string StateCode { get; set; }
            public string ZipCode { get; set; }
            //public string CountryCode { get; set; }
            public string Telephone { get; set; }
            public string? Fax { get; set; }
            public string? AirportCode { get; set; }
            public string HotelRating { get; set; }
            public decimal HotelLowRate { get; set; }
            public decimal HotelHighRate { get; set; }
            public byte[] HotelThumbURL { get; set; }
            public byte[] HotelImageURL { get; set; }
            public decimal HotelLatitude { get; set; }
            public decimal HotelLongitude { get; set; }
            public string HotelType { get; set; }
            public string HotelAmenities { get; set; }
            public string HotelCurrency { get; set; }
            public Byte StarRating { get; set; }
            public string TripAdvisorRating { get; set; }
            public string TripAdvisorRatingCount { get; set; }
            public string TripAdvisorRatingUrl { get; set; }
        }

    }

