using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int MaxAttendees { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public Branch Branch { get; set; }
        public Club Club { get; set; }

        public String? ImageName { get; set; }
    }
}
