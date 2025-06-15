using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.Models
{
    public class Notification
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; } // Navigation property

        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsRead { get; set; } = false;

        // Optional: for linking to specific entities
        public string? RelatedTo { get; set; } // e.g., "Booking", "Membership"
        public int? RelatedId { get; set; }
    }

}
