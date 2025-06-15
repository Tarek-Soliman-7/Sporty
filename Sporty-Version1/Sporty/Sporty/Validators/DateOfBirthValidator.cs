using System.ComponentModel.DataAnnotations;

namespace Sporty.Validators
{
    public  class DateOfBirthValidator:ValidationAttribute
    {
        protected override ValidationResult? IsValid (object? value, ValidationContext context)
        {
            var date = (DateTime)value;
            if (date > DateTime.Now.AddYears(-21)) { return new("You must be at least 21 years."); }
            return ValidationResult.Success;
        }
    }
}
