using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Sporty.Validators
{
    public class NationalIdValidator:ValidationAttribute
    {
        public NationalIdValidator() { ErrorMessage = "Then NationalID is invalid."; }
        public override bool IsValid(object? value)
        {
                var nationalId = value as string;

                if (string.IsNullOrWhiteSpace(nationalId)|| !Regex.IsMatch(nationalId, @"^\d{14}$"))
                    return false;


                // Parse birth date from ID
                string centuryDigit = nationalId.Substring(0, 1);
                string year = nationalId.Substring(1, 2);
                string month = nationalId.Substring(3, 2);
                string day = nationalId.Substring(5, 2);

                int century = centuryDigit == "2" ? 1900 : (centuryDigit == "3" ? 2000 : 0);
                if (century == 0) return false;

                string fullDate = $"{century + int.Parse(year)}-{month}-{day}";
                if (!DateTime.TryParse(fullDate, out DateTime birthDate))
                    return false;

                // Optional: Validate governorate code (next 2 digits)
                // Optional: Validate the check digit using checksum

                return true;
            

        }

    }
}

