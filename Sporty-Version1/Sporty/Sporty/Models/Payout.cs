using Sporty.Enums;

namespace Sporty.Models
{
    public class Payout
    {
        public Guid PayoutId { get; set; } = Guid.NewGuid();

        public int ClubId { get; set; }
        public Club Club { get; set; }

        public decimal Amount { get; set; }

        
        public PayoutStatus Status { get; set; } = PayoutStatus.Pending;

      
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public DateTime? ProcessedAt { get; set; }

        public string Note { get; set; }
    }
}
