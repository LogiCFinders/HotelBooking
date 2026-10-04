using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Messaging
{
    
    public class MessageRequest
    {
        public int HotelId { get; set; }
        public string BookingREF { get; set; }
        public string LeadPaxName { get; set; }
        public string RoomTypeName { get; set; }
        public string CheckinDate { get; set; }
        public int TotalPax { get; set; }
        public string  MessageProduct { get; set; }//W=WhatsApp S= SMS, E=email
        public string MessageType { get; set; }
        public string PhoneNumber { get; set; }
        



    }
}