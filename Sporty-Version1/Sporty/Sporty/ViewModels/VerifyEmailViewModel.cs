using System.ComponentModel.DataAnnotations;

namespace Sporty.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage ="Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
