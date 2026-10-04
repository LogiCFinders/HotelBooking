using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Models.DTOs
{
    public class CompanyDTO
    {
             public int Id { get; set; }
            public string Name { get; set; }

            public string Address { get; set; }
            public string City { get; set; }
            public string Pincode { get; set; }
            public string Country { get; set; }

            public string Phone1 { get; set; }
            public string Phone2 { get; set; }
            public string FaxNumber { get; set; }
            public string WebSite { get; set; }
            public string Email { get; set; }

            public string ServiceTaxNumber { get; set; }
            public string ATOLNumber { get; set; }
            public string PANNumber { get; set; }
            public string TANNumber { get; set; }
            public string ABTANumber { get; set; }

            public string CompanyLogo { get; set; }
            public string XHostLogo { get; set; }

            public bool IsAddressAndLogoUsedForDocs { get; set; }
            public bool IsBranchAddressAndLogoUsedForDocs { get; set; }
            public bool IsSAddressAndLogoUsedForB2bDocs { get; set; }

            public int CRS_Id { get; set; }
            public int CTnC_Id { get; set; }

            public bool IsActive { get; set; }
            public bool IsDeleted { get; set; }
           public Int64 TemplateID { get; set; }
            public string xAPIKey { get; set; }

    }

    public class TemplateDTO
    {
        public Int64 TemplateID { get; set; }
        public Int64 CompanyID { get; set; }
        public string TemplateType { get; set; }

        public string TemplateName { get; set; }
    }
}