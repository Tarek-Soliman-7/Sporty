using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.Models
{
    public class MembershipTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
        public int DurationYears { get; set; }
        public int MaxFamilyMembers { get; set; }
        public bool availability { get; set; }

        [ForeignKey("Branch")]
        public int BranchId;
        public Branch Branch { get; set; }


    }
}
