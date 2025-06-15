using Sporty.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sporty.Models
{

    
    public class Booking
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;

        [ForeignKey("Event")]
        public int EventId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public BookingStatus status { get; set; }
        
        public User User { get; set; }
        public Event Event { get; set; }

    }
}
