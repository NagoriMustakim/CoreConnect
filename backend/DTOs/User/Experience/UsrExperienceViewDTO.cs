namespace LinkwayAPI.DTOs.User.Experience
{
    public class UsrExperienceViewDTO
    {
        public Guid ExperienceGuid { get; set; }

        public Guid? EmploymentTypeGuid { get; set; }

        public string? EmploymentTypeName { get; set; }

        public Guid? LocationTypeGuid { get; set; }

        public string? LocationTypeName { get; set; }

        public string DesignationGuid { get; set; }
        public string Designation { get; set; } 
        public string CompanyGuid { get; set; }
        public string CompanyName { get; set; } 

        public string? Country { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public DateTime ExperienceStartDate { get; set; }

        public DateTime? ExperienceEndDate { get; set; }

        public bool IsExperienceActive { get; set; }

        public string ExperienceIndustry { get; set; } = null!;

        public string? ExperienceDescription { get; set; }
    }
}
