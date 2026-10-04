using ezyvoyagerAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Models.DTOs
{
    public class VoyagerRESP
    {

        public Errors Errors { get; set; }
        public VoyagerInfo[] VoyagerInfoArry { get; set; }
    }
}