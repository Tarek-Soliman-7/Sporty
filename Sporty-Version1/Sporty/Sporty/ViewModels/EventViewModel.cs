using System.ComponentModel.DataAnnotations;

namespace Sporty.ViewModels
{
    public class EventViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "invalid_inpute")]
        public string Title { get; set; }
        [Required(ErrorMessage = "invalid_inpute")]
        public string Description { get; set; }
        [Required(ErrorMessage = "invalid_inpute")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "invalid_inpute")]
        public int MaxAttendees { get; set; }
        public int? CurrentAttendess { get; set; } = 0;
        [Required(ErrorMessage = "invalid_inpute")]
        public int BranchId { get; set; }
        [Required(ErrorMessage = "invalid_inpute")]
        public int ClubId { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}
