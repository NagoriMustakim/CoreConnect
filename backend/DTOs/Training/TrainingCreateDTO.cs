using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Training
{
    public class TrainingCreateDTO
    {
        [Required]
        [RegularExpression(ValidationConstant.ALPHANUMERICS_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string TrainingTitle { get; set; } = null!;
        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? TrainingDescription { get; set; }
    }
}
