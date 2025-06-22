using LinkwayAPI.Data;

namespace LinkwayAPI.DTOs.User
{
    public class UsrAwardDTO
    {
        public Guid AwardId { get; set; }

        public string Title { get; set; } = null!;

        public string? Issuer { get; set; }

        public DateTime? IssueDate { get; set; }

        public string? Description { get; set; }

        public string? PhotoPath { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

    }
}
