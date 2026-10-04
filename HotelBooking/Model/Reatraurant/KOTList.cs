using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Reatraurant
{
    public class KOTList
    {
        
        public int RestaurantId { get; set; }
        public int BranchId { get; set; }
        public int BillingId { get; set; }
        public string TableNumber { get; set; }
        public int OrderedById { get; set; }
        public string orderedByName { get; set; }
        public IEnumerable<KOTListDetails> Items { get; set; }
    }
    public class KOTListDetails
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string OrderStatus { get; set; }
    }
}