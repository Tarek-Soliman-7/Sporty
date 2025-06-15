using Sporty.Enums;
using Sporty.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.ViewModels
{
    public class BookingViewModel
    {
        public DateTime Date { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public BookingStatus status { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
    }
}
