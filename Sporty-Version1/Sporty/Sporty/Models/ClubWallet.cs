using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sporty.Models
{
    public class ClubWallet
    {
        [Key, ForeignKey("Club")]
        public int ClubId { get; set; }

        public decimal TotalEarned { get; set; }
        public decimal TotalPaidOut { get; set; }
        public decimal CurrentBalance => TotalEarned - TotalPaidOut;
        public DateTime LastUpdated { get; set; }

        public Club Club { get; set; }
    }

}
