namespace LinkwayAPI.DTOs.User.Education
{
    public class UsrEducationDTO
    {
        public string UserGuid { get; set; }

        public string InstituteName { get; set; } = null!;

        public string? Degree { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        public string? Grade { get; set; }

        public string? Description { get; set; }
    }
}
