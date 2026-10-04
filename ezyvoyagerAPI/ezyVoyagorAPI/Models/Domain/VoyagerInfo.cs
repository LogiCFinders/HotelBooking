using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezyvoyagerAPI.Models.Domain
{

    [Table("EZV_ADMIN.TripMaster")]
    public class VoyagerInfo
    {

        // Identification
        [Key]
        public Int64 VoyagerID { get; set; }              // Unique identifier
        public string Title { get; set; }               // Name/title of the activity
        public string Description { get; set; }         // Detailed description
        public string GroupType { get; set; }           // Type of group (e.g., family, solo, corporate)

        // Duration & Frequency
        public Int16 Duration { get; set; }               // Numeric duration
        public string DurationUnit { get; set; }        // Unit (hours, days, weeks)
        public string Frequency { get; set; }           // How often it occurs (daily, weekly, seasonal)

        // Ratings & Policies
        public Int16 Rating { get; set; }              // Average rating (e.g., 4.5)
        public string CancellationPolicy { get; set; }  // Cancellation terms
        public string PaymentPolicy { get; set; }       // Payment terms

        // Location & Pricing
        public string CountryCode { get; set; }         // ISO country code
        public string PriceType { get; set; }           // Per person, per group, etc.
        public decimal BaseRate { get; set; }           // Base price
        public string Currency { get; set; }            // Currency code (USD, INR, etc.)
        public decimal Tax { get; set; }                // Tax amount
        public bool IsTaxFixed { get; set; }            // True if tax is fixed, false if percentage
        public decimal StrikethroughRate { get; set; }  // Discounted/old rate
        public string RateHeading { get; set; }         // Heading/label for rate
        public string PriceOffer { get; set; }          // Special offer details

        // Seasonal & Activity Info
        public string MonthsOfSeasons { get; set; }     // Months applicable (e.g., Nov–Feb)
        public string BestTime { get; set; }            // Best time to visit
        public string OtherTerms { get; set; }          // Additional terms
        public string ActivityType { get; set; }        // Type of activity (adventure, cultural, etc.)
        public string PlaceOfActivity { get; set; }     // Location/place
        public string Grade { get; set; }               // Difficulty grade
        public bool StopSell { get; set; }              // Flag to stop selling
        public string Theme { get; set; }               // Theme (nature, heritage, etc.)

        // Status & Metadata
        public bool IsActive { get; set; }              // Active/inactive flag
        public DateTime DateCreated { get; set; }       // Creation date
        public Int64 CreatedBy { get; set; }           // Creator
        public Int64 ModifiedBy { get; set; }          // Last modifier
        public DateTime? ModifiedDate { get; set; }     // Last modified date (nullable)
        public bool IsDeleted { get; set; }             // Soft delete flag

        // SEO & Social
        public string MetaTags { get; set; }            // Meta tags for SEO
        public string CanonicalTags { get; set; }       // Canonical tags
        public string Keywords { get; set; }            // Keywords
        public string SocialTags { get; set; }          // Social media tags




        [NotMapped]
        public Image[] Images { get; set; }

        [NotMapped]
        public InclusionsExclusion[] InclusionsExclusions { get; set; }
        [NotMapped]
        public TravelDate[] TravelDates { get; set; }
        [NotMapped]
        public Itinerary[] Itineraries { get; set; }
        [NotMapped]
        public VoyagerFAQ[] VoyagerFaqs { get; set; }
    }
}