using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezyvoyagerAPI.Models.Domain
{
    [Table("EZV_ADMIN.Currency")]
    public class Currency
    {
        [Key]
        public Int64 ID { get; set; }
        public Int64 CompanyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }


    [Table("EZV_ADMIN.Theme")]
    public class Theme
    {

        [Key]
        public Int64 ID { get; set; }
        public Int64 CompanyId { get; set; }
        public string value { get; set; }
     
    }

    [Table("EZV_ADMIN.Grade")]
    public class Grade
    {

        [Key]
        public Int64 ID { get; set; }
        public Int64 CompanyId { get; set; }
        public string value { get; set; }

    }

    [Table("EZV_ADMIN.ActivityType")]
    public class ActivityType
    {

        [Key]
        public Int64 ID { get; set; }
        public Int64 CompanyId { get; set; }
        public string value { get; set; }

    }


    [Table("EZV_ADMIN.PriceType")]
    public class PriceType
    {

        [Key]
        public Int64 ID { get; set; }
        public Int64 CompanyId { get; set; }
        public string value { get; set; }

    }


    [Table("EZV_ADMIN.CountryCode")]
    public class CountryCode
    {

        [Key]
        public Int64 ID { get; set; }
        public Int64 CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }

    [Table("EZV_ADMIN.Frequency")]
    public class Frequency
    {

        [Key]
        public Int64 ID { get; set; }
        public Int64 CompanyId { get; set; }
        public string value { get; set; }

    }

    [Table("EZV_ADMIN.DurationUnit")]
    public class DurationUnit
    {

        [Key]
        public Int64 ID { get; set; }
        public Int64 CompanyId { get; set; }
        public string value { get; set; }

    }

    [Table("EZV_ADMIN.GroupType")]
    public class GroupType
    {

        [Key]
        public Int64 ID { get; set; }
        public Int64 CompanyId { get; set; }
        public string value { get; set; }

    }
}