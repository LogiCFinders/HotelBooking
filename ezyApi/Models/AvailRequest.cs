namespace ezyApi.Models
{
    

        public class AvailRequest
        {
            public float Longitude { get; set; }
            public float Latitude { get; set; }
            public string CountryCode { get; set; }
            public string HotelName { get; set; }
            public string HotelID { get; set; }
            public DateTime CheckOutDate { get; set; }
            public DateTime CheckInDate { get; set; }
            public Authentication Authentication { get; set; }
            public Roomdetailary RoomDetailAry { get; set; }
        }

        public class Authentication
        {
            public string AuthKey { get; set; }
            public string AuthIP { get; set; }
        }

        public class Roomdetailary
        {
            public int NumberOfAdults { get; set; }
            public int NumberOfChildren { get; set; }
            public string ChildrenAges { get; set; }
        }

    }

