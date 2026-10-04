using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("HODB_Admin.MessageAPIConfig")]
    public class MessageAPIConfig
    {
        [Key]
        public Int64 ID { get; set; }
        public Int64 HotelId { get; set; }
        public string APiUrl { get; set; }
        public string APIKey { get; set; }
        public string APIPWD { get; set; }
        public string MSGPRODUCT { get; set; }
        public string MsgType { get; set; }
        public string MSGSender { get; set; }
        public bool Status { get; set; }
    }
}