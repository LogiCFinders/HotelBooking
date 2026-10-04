using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Models.DTOs
{
    public class VoyagerREQ
    {
        public DateTime TravelDate { get; set; }
        public string Destination { get; set; } = string.Empty;
    }
}