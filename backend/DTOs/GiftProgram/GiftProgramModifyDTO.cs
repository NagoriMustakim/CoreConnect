using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.GiftProgram
{
    public class GiftProgramModifyDTO
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string DesiredDesignation { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string CurrentDesignation { get; set; } = null!;

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? GapsIdentified { get; set; }

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? PlanOfAction { get; set; }

        public DateTime? TargetAchievementDate { get; set; }

        public bool? IsSupportRequired { get; set; }

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? EndResult { get; set; }

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? MajorAchievements { get; set; }

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? LearningAndTransistionProcess { get; set; } = null!;

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? CareerGrowthContribution { get; set; } = null!;

        [MaxLength(1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_ALPHABETS_SPACE_DOT_HYPHEN, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? WorkRelatedTrainingAndCertifications { get; set; }

        public int GiftformManagerStatus { get; set; }

        public int GiftformAdminStatus { get; set; }

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? GiftformRejectionReason { get; set; }
    }
}
