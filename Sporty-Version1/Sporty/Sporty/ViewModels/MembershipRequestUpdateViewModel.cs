using Sporty.Enums;
using System.ComponentModel.DataAnnotations;
using Sporty.Models;
namespace Sporty.ViewModels
{
    public class MembershipRequestUpdateViewModel
    {
      
            public int Id { get; set; }
            [Required]
            public string? Notes { get; set; }
            public bool InterviewRequired { get; set; }
            public DateTime? InterviewDate { get; set; }
            [Required]
            public int? Decision { get; set; }
    }
}
