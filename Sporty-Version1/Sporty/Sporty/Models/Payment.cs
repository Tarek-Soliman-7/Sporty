using Sporty.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.Models
{
    public class Payment
    {
        public Guid Id {  get; set; }= Guid.NewGuid();

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public int RelatedId { get; set; }
        public PaymentRelatedTo RelatedTo { get; set; }
        public decimal Amount { get; set; }
        public decimal NetAmountToClub { get; set; }
        public int PlatformFee { get; set; } = 5;
        public PaymentMethod PaymentMethod { get; set; }
        public string Reference { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public User User { get; set; }
        public Club Club { get; set; }
        
    }
}
