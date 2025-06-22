using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Proficiency
{
    public class ProficiencyCreateDTO
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(ValidationConstant.PROFICIENCY_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public string ProficiencyTitle { get; set; }

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? ProficiencyDescription { get; set; }
    }
}
