using System.ComponentModel.DataAnnotations.Schema;

namespace Sporty.Models
{
    public class Document
    {
        public int Id { get; set; } 
        public string DocumentType { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public bool IsValidated { get; set; }

        [ForeignKey("Request")]
        public int RequestID { get; set; }
        public MembershipRequests Request { get; set; }
    }
}
