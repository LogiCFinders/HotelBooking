using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezyvoyagerAPI.Models.Domain
{
    [Table("EZV_ADMIN.TripImages")]
    public class Image
    {
        [Key]
        public Int64 ImageID { get; set; }                // Unique identifier for the image
        public Int64 VoyagerID { get; set; }              // Foreign key reference to Voyager

        // Media Details
        public byte[] Media { get; set; }               // Path/URL to the media file
        public string ImageDescription { get; set; }    // Description of the image
        public string ImageName { get; set; }           // File name or label

        // Banner Settings
        public bool IsBannerImage { get; set; }         // Flag if image is used as banner
        public Int64? BannerImageSeq { get; set; }        // Sequence/order for banner images

        // Status
        public bool IsActive { get; set; }              // Active/inactive flag
        public bool IsDeleted { get; set; }             // Soft delete flag

        // Audit Information
        public DateTime DateCreated { get; set; }       // Creation date
        public Int64 CreatedBy { get; set; }           // Creator
        public Int64 ModifiedBy { get; set; }          // Last modifier
        public DateTime? ModifiedDate { get; set; }     // Last modified date (nullable)



    }
}