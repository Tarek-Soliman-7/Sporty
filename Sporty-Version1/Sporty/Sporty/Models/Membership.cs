using Sporty.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sporty.Models
{

    public class Membership
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MembershipStatus status { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }

        public Branch Branch { get; set; }
        public Club Club { get; set; }

        public User User { get; set; }
    }
}
