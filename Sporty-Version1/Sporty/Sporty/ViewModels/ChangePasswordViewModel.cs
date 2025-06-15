using System.ComponentModel.DataAnnotations;

namespace Sporty.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password does not match.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confrim New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
