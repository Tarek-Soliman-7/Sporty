using Sporty.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sporty.Models
{
   
    public class MembershipRequests
    {
        
        public int Id { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public DateTime? DecisionDate { get; set; }
        public string? Notes { get; set; }
        public bool? InterviewRequired { get; set; }

        public DateTime? InterviewDate { get; set; }
        public PaymentOption? PaymentOption { get; set; }

        public MembershipRequestStatus Status { get; set; } = MembershipRequestStatus.Pending;

        [ForeignKey("RequestingUser")]
        public string UserId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        [ForeignKey("Club")]
        public int ClubId { get; set; }

        [ForeignKey("MembershipType")]
        public int MembeshipTypeId { get; set; }

        [ForeignKey("ReviewdByAdmin")]
        public string? ReviewedByAdminId { get; set; }
        public User RequestingUser { get; set; }
        public User ReviewdByAdmin { get;  set; }
        public Branch Branch { get; set; }
        public Club Club { get; set; }
        public MembershipTypes MembershipType { get; set; }
        public ICollection<Document> Documents { get; set; }

    }
}
