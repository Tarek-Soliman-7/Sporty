using Sporty.Enums;
using System.ComponentModel.DataAnnotations;
using Sporty.Validators;
namespace Sporty.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Phone Number is Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="insert a valid Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [DateOfBirthValidator]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "NationalID is required")]
        [NationalIdValidator]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(40,MinimumLength =8,ErrorMessage ="The {0} must be at {2} and at max {1} characters long.")]
        [Compare("ConfirmPassword",ErrorMessage ="Password does not match.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm is required")]
        [DataType(DataType.Password)]
        [Display(Name="Confrim Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Gender is Required")]
        public Gender Gender { get; set; }

    }
}
