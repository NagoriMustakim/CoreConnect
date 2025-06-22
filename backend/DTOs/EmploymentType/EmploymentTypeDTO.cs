using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.EmploymentType
{
    public class EmploymentTypeDTO
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(ValidationConstant.ALPHANUMERICS_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public string EmploymentTypeTitle { get; set; }

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? EmploymentTypeDescription { get; set; }
    }
}
