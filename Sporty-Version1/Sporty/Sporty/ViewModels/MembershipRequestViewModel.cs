using Sporty.Models;

namespace Sporty.ViewModels
{
    public class MembershipRequestViewModel
    {
       public int RequestID { get; set; }
       public  string? UserId { get; set; }
       public int BranchId { get; set; }
       public int MembershipTypeId { get; set; }
       public int ClubId { get; set; }

        public IEnumerable<Branch> Branches { get; set; } 
        public IEnumerable<MembershipTypes> MembershipTypes { get; set; }

        // For document uploads
        public IFormFileCollection Files { get; set; }

    }
}
