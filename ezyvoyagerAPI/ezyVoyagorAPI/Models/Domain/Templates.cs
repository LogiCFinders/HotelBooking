using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ezyVoyagorAPI.Models.Domain
{
    [Table("EZV_ADMIN.Templates")]
    public class Templates
    {
        [Key]
        public Int64 TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateType { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}