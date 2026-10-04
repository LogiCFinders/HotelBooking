using ezyvoyagerAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Models.DTOs
{
    public class VoyagerItenaryAddRESP
    {
        public Errors Errors { get; set; }
        public VoyagerActionType Actions { get; set; }
        public bool IsSuccess { get; set; }

        /// <summary>Optional message for UI or logs</summary>
        public string Message { get; set; }

        /// <summary>ID of the newly created itinerary (if successful)</summary>
        public int ItenaryId { get; set; }

    }
}