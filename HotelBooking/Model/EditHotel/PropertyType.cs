using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.EditHotel
{
    [Table("dbo.PropertyType")]
    public class PropertyType
    {
        [Key]
        public int PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; }
        public string PropertyTypeCode { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class Countries
    {
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateCreated { get; set; }
    }
    [Table("dbo.Currency")]
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string Currencysymbol { get; set; }
      
    }


}