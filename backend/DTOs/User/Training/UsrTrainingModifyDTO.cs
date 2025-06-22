using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.User.Training
{
    public class UsrTrainingModifyDTO
    {
        [Required]
        public Guid TrainingTypeGuid { get; set; }

        [Required]
        public DateTime TrainingStartDate { get; set; }

        public DateTime? TrainingEndDate { get; set; }

        [Required]
        public bool IsTrainingActive { get; set; }       
        public Guid? TrainingNameGuid { get; set; }
        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string? UserTrainingDescription { get; set; }
    }
}
