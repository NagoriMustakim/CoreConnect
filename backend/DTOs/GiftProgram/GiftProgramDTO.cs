using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.GiftProgram
{
    public class GiftProgramDTO
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string CurrentDesignationTitle { get; set; } = null!;
        [Required]
        public Guid DesiredDesignationGuid { get; set; }

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? GapsIdentified { get; set; }

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? PlanOfAction { get; set; }

        public DateTime? TargetAchievementDate { get; set; }

        [Required]
        public bool IsSupportRequired { get; set; }

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? EndResult { get; set; }

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? MajorAchievements { get; set; }

        [Required]
        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string LearningAndTransistionProcess { get; set; }

        [Required]
        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string CareerGrowthContribution { get; set; }

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? WorkRelatedTrainingAndCertifications { get; set; }

        public int GiftformManagerStatus { get; set; }

        public int GiftformAdminStatus { get; set; }
    }
}
