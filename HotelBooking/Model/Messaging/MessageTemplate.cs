using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Messaging
{
    [Table("TemplateMaster")]
    public class MessageTemplate
    {
        [Key]
        public int TempID { get; set; }
        public string TempMSG { get; set; }
        public string Status { get; set; }
        public Int64 Hotelid { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string MsgProduct { get; set; }
        public string MsgType { get; set; }
        
    }

}