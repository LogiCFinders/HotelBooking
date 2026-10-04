using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace feedbackApi.Models
{

    [Table("CompanyMaster")]
    public class Company
    {
        private string address2;

        [Key] public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Address1 { get; set; }
        public string Address2 { get => address2; set => address2 = value; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string BusinessCategory { get; set; }
        public string BusinessSubCategory { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.MinValue;
        public bool IsActive { get; set; } = true;
        public DateTime? DateUpdated { get; set; } = DateTime.MinValue;
        public string BillingType { get; set; }
        public decimal?  BillingRate { get; set; } 
        public DateTime? BillingDate { get; set; } = DateTime.MinValue;
        public string Remarks { get; set; }
        public string MasterLogin { get; set; }
        public string Password { get; set; }
        public bool is2FA { get; set; }
        public DateTime? LastLogin { get; set; }
        public int? PlanId { get; set; }
        public Guid CompanyGuid { get; set; }

        [ForeignKey(nameof(PlanId))] public Plan Plan { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }

    [Table("ReviewMaster")]
    public class Review
    {
        [Key] public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public string ReviewTitle { get; set; }
        public DateTime DateofService { get; set; } = DateTime.Now;
        public DateTime? DateofReview { get; set; } = DateTime.MinValue;
        public DateTime? PublishedDate { get; set; } = DateTime.MinValue;
        public string ApprovedBy { get; set; }
        public string ReasonNotApproved { get; set; }
        [Column("CompanyId")]
        public int ReviewFor { get; set; }
        public string ReviewByEmail { get; set; } = string.Empty;
        public string ReviewByName { get; set; } = string.Empty;
        public string ServiceId { get; set; }
        public string ReviewJson { get; set; }

       [ForeignKey(nameof(ReviewFor))] public Company Company { get; set; }
        public ICollection<Media> MediaItems { get; set; } = new List<Media>();
        public ICollection<Reply> Replies { get; set; } = new List<Reply>();
    }

    [Table("ReviewQuestionSet")]
    public class Question
    {
        [Key] public int QuestionID { get; set; }
        public int CompanyID { get; set; }
        public int QSrNo { get; set; }
        public string QText { get; set; } = string.Empty;
        public string QType { get; set; } = "TextBox";
        public string AnsOp1 { get; set; }
        public string AnsOp2 { get; set; }
        public string AnsOp3 { get; set; }
        public string AnsOp4 { get; set; }
        public string AnsOp5 { get; set; }
        public string AnsOp6 { get; set; }

        [ForeignKey(nameof(CompanyID))] public Company Company { get; set; }
    }

    [Table("ReviewMedia")]
    public class Media
    {
        [Key] public int MediaID { get; set; }
        public int ReviewID { get; set; }
        public string MediaType { get; set; } = "Photo";
        [Column("Media")] public string MediaPath { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ReviewID))] public Review Review { get; set; }
    }

    [Table("ReviewReply")]
    public class Reply
    {
        [Key] public int ReplyId { get; set; }
        public int ReviewID { get; set; }
        public string ReplyText { get; set; } = string.Empty;
        public DateTime? ReplyDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ReviewID))] public Review Review { get; set; }
    }

}