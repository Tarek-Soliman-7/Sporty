using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
