using Microsoft.AspNetCore.Identity;
using Sporty.Enums;
namespace Sporty.Models
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
        public string? NationalId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public int? ClubId { get; set; }
    }
}
