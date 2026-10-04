using ezyvoyagerAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Models.DTOs
{
    public class VoyagerItenaryAddREQ
    {
        public Errors Errors { get; set; }
        public VoyagerActionType Actions { get; set; }
        public VoyagerInfo[] VoyagerInfoArry { get; set; }

    }
}