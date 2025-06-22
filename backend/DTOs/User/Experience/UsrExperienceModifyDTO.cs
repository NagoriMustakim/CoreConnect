using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.User.Experience
{
    public class UsrExperienceModifyDTO
    {
        public Guid ExperienceGuid { get; set; }

        public Guid? EmploymentTypeGuid { get; set; }

        public Guid? LocationTypeGuid { get; set; }

        public Guid? DesignationGuid { get; set; }
        public Guid? CompanyGuid { get; set; }

        public string? Country { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ExperienceStartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ExperienceEndDate { get; set; }

        public bool IsExperienceActive { get; set; }
       
        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? ExperienceDescription { get; set; }
    }
}
