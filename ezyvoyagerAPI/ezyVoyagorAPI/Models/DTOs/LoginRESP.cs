using ezyvoyagerAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Models.DTOs
{
    public class LoginRESP
    {
        public Errors Errors { get; set; }
        public VoyagerActionType Actions { get; set; }
        //public LoginInfo LoginDetails { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Designation { get; set; }

        public string vToken { get; set; } = string.Empty;
    }
}