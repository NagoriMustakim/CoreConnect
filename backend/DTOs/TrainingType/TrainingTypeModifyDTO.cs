using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.TrainingType
{
    public class TrainingTypeModifyDTO
    {
        public Guid TrainingTypeGuid { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression(ValidationConstant.ALPHANUMERICS_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public string TrainingType { get; set; } = null!;

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? TrainingTypeDescription { get; set; }
    }
}
