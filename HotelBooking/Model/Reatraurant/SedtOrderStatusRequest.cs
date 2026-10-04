using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Reatraurant
{
    public class SedtOrderStatusRequest
    {
        public int RestaurantId { get; set; }
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public int ItemId { get; set; }
        public string status { get; set; }
    }
}