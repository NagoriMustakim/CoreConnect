namespace LinkwayAPI.DTOs.User.Education
{
    public class UsrEducationViewDTO
    {
        public Guid EducationGuid { get; set; }

        public string EducationInstituteName { get; set; } = null!;

        public string? EducationDegree { get; set; }

        public DateTime EducationStartDate { get; set; }

        public DateTime? EducationEndDate { get; set; }

        public bool IsEducationActive { get; set; }

        public string? EducationGrade { get; set; }

        public string? EducationDescription { get; set; }
    }
}
