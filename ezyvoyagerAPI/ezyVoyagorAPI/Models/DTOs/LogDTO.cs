using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Models.DTOs
{
    public class LogDto
    {
        public string Source { get; set; }
        public string Event { get; set; }
        public Int64 LoggedInUserId { get; set; }
        public string LogDescription { get; set; }
    }
}