using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace feedbackApi.Data.DTOs
{

    public class ReplyCreateDto
    {
        [Required]
        [MaxLength(2000)]
        public string ReplyText { get; set; }

        public ReplyCreateDto(string replyText)
        {
            ReplyText = replyText;
        }
    }
    public class ApproveDto
    {
        [Required]
        public string AdminName { get; set; }

        public ApproveDto(string adminName)
        {
            AdminName = adminName;
        }
    }
    public class RejectDto
    {
        [Required]
        [MaxLength(500)]
        public string Reason { get; set; }

        [Required]
        public string AdminName { get; set; }

        public RejectDto(string reason, string adminName)
        {
            Reason = reason;
            AdminName = adminName;
        }
    }
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewText { get; set; }
        public DateTime? DateofService { get; set; }
        public DateTime? DateofReview { get; set; }
        public DateTime?  PublishedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ReasonNotApproved { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ReviewByName { get; set; }
        public string ReviewByEmail { get; set; }
        public string ServiceId { get; set; }
        public string ReviewJson { get; set; }
        public string Status { get; set; }
        public List<ReplyDto> Replies { get; set; }
        public List<MediaDto> Media { get; set; }

        // Constructor for initialization
        public ReviewDto(int reviewId, int rating, string reviewTitle, string reviewText,
                         DateTime? dateofService, DateTime? dateofReview, DateTime? publishedDate,
                         string approvedBy, string reasonNotApproved,
                         int companyId, string companyName,
                         string reviewByName, string reviewByEmail, string serviceId, string reviewJson,
                         string status,
                         List<ReplyDto> replies, List<MediaDto> media)
        {
            ReviewId = reviewId;
            Rating = rating;
            ReviewTitle = reviewTitle;
            ReviewText = reviewText;
            DateofService = dateofService;
            DateofReview = dateofReview;
            PublishedDate = publishedDate;
            ApprovedBy = approvedBy;
            ReasonNotApproved = reasonNotApproved;
            CompanyId = companyId;
            CompanyName = companyName;
            ReviewByName = reviewByName;
            ReviewByEmail = reviewByEmail;
            ServiceId = serviceId;
            ReviewJson = reviewJson;
            Status = status;
            Replies = replies;
            Media = media;
        }
    }
    public class ReviewSubmitDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        [Range(1, 5)]       
        public int Rating { get; set; }

        [MaxLength(150)]
        public string ReviewTitle { get; set; }

        [MaxLength(2000)]
        public string ReviewText { get; set; }

        public DateTime? DateofService { get; set; }

        [Required]
        [EmailAddress]
        public string ReviewByEmail { get; set; }

        [Required]
        [MaxLength(100)]
        public string ReviewByName { get; set; }

        public string ServiceId { get; set; }
        public string ReviewJson { get; set; }

        // Constructor for initialization
        public ReviewSubmitDto(int companyId, int rating, string reviewTitle, string reviewText,
                               DateTime? dateofService, string reviewByEmail, string reviewByName,
                               string serviceId, string reviewJson)
        {
            CompanyId = companyId;
            Rating = rating;
            ReviewTitle = reviewTitle;
            ReviewText = reviewText;
            DateofService = dateofService;
            ReviewByEmail = reviewByEmail;
            ReviewByName = reviewByName;
            ServiceId = serviceId;
            ReviewJson = reviewJson;
        }
    }
    public class ReplyDto
    {
        public int ReplyId { get; set; }
        public string ReplyText { get; set; }
        public DateTime? ReplyDate { get; set; }

        public ReplyDto(int replyId, string replyText, DateTime? replyDate)
        {
            ReplyId = replyId;
            ReplyText = replyText;
            ReplyDate = replyDate;
        }
    }
    public class MediaDto
    {
        public int MediaID { get; set; }
        public string MediaType { get; set; }
        public string Url { get; set; }
        public bool IsPublished { get; set; }

        public MediaDto(int mediaID, string mediaType, string url, bool isPublished)
        {
            MediaID = mediaID;
            MediaType = mediaType;
            Url = url;
            IsPublished = isPublished;
        }
    }


    public class RegisterDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        // Constructor for initialization (optional)
        public RegisterDto(int companyId, string username, string email, string password, string fullName)
        {
            CompanyId = companyId;
            Username = username;
            Email = email;
            Password = password;
            FullName = fullName;
        }

        // Parameterless constructor (needed for model binding in Web API)
        public RegisterDto() { }
    }
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        // Parameterless constructor (needed for Web API model binding)
        public LoginDto() { }

        // Optional convenience constructor
        public LoginDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }


public class CompanyUpdateDto
    {
        [Required]
        public string CompanyName { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string BusinessCategory { get; set; }
        public string BusinessSubCategory { get; set; }
        public string Remarks { get; set; }

        // Parameterless constructor (needed for Web API model binding)
        public CompanyUpdateDto() { }

        // Optional convenience constructor
        public CompanyUpdateDto(
            string companyName, string address1, string address2, string postalCode,
            string country, string phone, string email, string contactName,
            string contactNumber, string businessCategory, string businessSubCategory,
            string remarks)
        {
            CompanyName = companyName;
            Address1 = address1;
            Address2 = address2;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;
            Email = email;
            ContactName = contactName;
            ContactNumber = contactNumber;
            BusinessCategory = businessCategory;
            BusinessSubCategory = businessSubCategory;
            Remarks = remarks;
        }

    }
    public class QuestionDto
    {
        public int QuestionID { get; set; }
        public int QSrNo { get; set; }
        public string QText { get; set; }
        public string QType { get; set; }
        public List<string> Options { get; set; }

        // Parameterless constructor (needed for Web API model binding)
        public QuestionDto() { }

        // Convenience constructor
        public QuestionDto(int questionID, int qSrNo, string qText, string qType, List<string> options)
        {
            QuestionID = questionID;
            QSrNo = qSrNo;
            QText = qText;
            QType = qType;
            Options = options;
        }
    }
    public class ReviewRequestCreateDto
    {
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Phone]
        public string CustomerPhone { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [MaxLength(50)]
        public string ServiceID { get; set; }

        public DateTime? ServiceDate { get; set; }

        [Required]
        public int CompanyId { get; set; }

        // Parameterless constructor (needed for Web API model binding)
        public ReviewRequestCreateDto() { }

        // Optional convenience constructor
        public ReviewRequestCreateDto(
            string customerEmail,
            string customerPhone,
            string customerName,
            string serviceID,
            DateTime? serviceDate,
            int companyId)
        {
            CustomerEmail = customerEmail;
            CustomerPhone = customerPhone;
            CustomerName = customerName;
            ServiceID = serviceID;
            ServiceDate = serviceDate;
            CompanyId = companyId;
        }


    }

    public class DailyCount
    {
        public string Date { get; set; }
        public int Count { get; set; }

        public DailyCount(string date, int count)
        {
            Date = date;
            Count = count;
        }
    }

    public class RatingBucket
    {
        public int Rating { get; }
        public int Count { get; }

        public RatingBucket(int rating, int count)
        {
            Rating = rating;
            Count = count;
        }
    }

    public class Summary
    {
        public int TotalReviews { get; set; }
        public int ApprovedReviews { get; set; }
        public int PendingReviews { get; set; }
        public int RejectedReviews { get; set; }
        public List<DailyCount> ReviewsLast30Days { get; set; }
        public List<RatingBucket> RatingCounts { get; set; }

        public Summary(
            int totalReviews,
            int approvedReviews,
            int pendingReviews,
            int rejectedReviews,
            List<DailyCount> reviewsLast30Days,
            List<RatingBucket> ratingCounts)
        {
            TotalReviews = totalReviews;
            ApprovedReviews = approvedReviews;
            PendingReviews = pendingReviews;
            RejectedReviews = rejectedReviews;
            ReviewsLast30Days = reviewsLast30Days;
            RatingCounts = ratingCounts;
        }
    }


}