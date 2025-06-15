using System.ComponentModel.DataAnnotations;

namespace Sporty.ViewModels
{
    public class BranchViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Branch name is required")]
        [Display(Name = "Branch Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        
        public int ClubId { get; set; }
    }
}
