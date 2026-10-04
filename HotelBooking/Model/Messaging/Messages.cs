using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Messaging
{
    [Table("Messages")]
    public class Messages
    {
        [Key]
        public Int64 MsgID { get; set; }
        public Int64 HotelID { get; set; }
        public string SentTo { get; set; }
        public string SentFrom { get; set; }
        public DateTime MsgDate { get; set; }
        public string MsgText { get; set; }
        public string MsgProduct { get; set; }
        public string Bookingref { get; set; }


    }
}