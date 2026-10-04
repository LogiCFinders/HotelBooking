using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace ezyvoyagerAPI.Models.Domain
{
    [Table("EZV_ADMIN.Branch")]
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchDescription { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string EmailID { get; set; }
        public string IATANo { get; set; }

        public string Bank1Name { get; set; }
        public string Bank1AcNumber { get; set; }
        public string Bank2Name { get; set; }
        public string Bank2AcNumber { get; set; }
        public string AltBankName { get; set; }
        public string AltBankAcNumber { get; set; }

        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public string HotelID { get; set; }
        public string HotelKey { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }

        public string PropInformation { get; set; }
        public string PropDescription { get; set; }
        public string DrivingDirections { get; set; }
        public string Transportation { get; set; }
        public bool FireSafetyCompliant { get; set; }

        public string CheckInInstructions { get; set; }
        public string SpecialCheckInInstructions { get; set; }
        public string HotelShortName { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ChainName { get; set; }
        public string TimeZone { get; set; }

        public int BuildYear { get; set; }
        public int StarRating { get; set; }
        public string Amenities { get; set; }
        public string NearestAirport { get; set; }
        public string WebsiteURL { get; set; }

        public bool PetsAllowed { get; set; }
        public string CheckinPolicy { get; set; }
        public bool CoupleFriendly { get; set; }
        public bool CheckinWithLocalIds { get; set; }

        public string WebsiteHeaderFile { get; set; }
        public string WebsiteFooterFile { get; set; }

        public string DateCreated { get; set; }
        public string DateModified { get; set; }

        public int NoOfFloors { get; set; }
        public string LogoImage { get; set; }
        public int DecimalPlaces { get; set; }

        public string PanCard { get; set; }
        public string TaxNo1 { get; set; }
        public string TaxNo2 { get; set; }

        public string HotelType { get; set; }
        public string HotelCurrency { get; set; }
    }
}
