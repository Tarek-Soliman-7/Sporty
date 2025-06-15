using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sporty.ViewModels
{
    public class MembershipTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Membership Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number")]
        [Display(Name = "Price")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 10, ErrorMessage = "Duration must be between 1 and 10 years")]
        [Display(Name = "Duration (Years)")]
        public int DurationYears { get; set; }

        [Required(ErrorMessage = "Max number of family members is required")]
        [Range(0, 10, ErrorMessage = "Value must be between 0 and 10")]
        [Display(Name = "Max Family Members")]
        public int MaxFamilyMembers { get; set; }

        [Display(Name = "Available")]
        public bool Availability { get; set; }

        [Required(ErrorMessage = "Branch selection is required")]
        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        // For displaying dropdown list of branches
        public List<SelectListItem>? Branches { get; set; }
    }
}
