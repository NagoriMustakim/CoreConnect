using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.InternalPrograms
{
    public class InternalProgramModifyDTO
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(ValidationConstant.ALPHANUMERICS_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public string InternalProgramName { get; set; }

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? InternalProgramDescription { get; set; }

        [Required]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_DIGITS, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public int InternalProgramReviewCycle { get; set; }

        [Required]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_DIGITS, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public int InternalProgramActiveDays { get; set; }

        [Required]
        public bool IsInternalProgramCategoryExists { get; set; }
    }
}
