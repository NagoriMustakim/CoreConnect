using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Nomination
{
    public class NominationDTO
    {
        [Required]
        public string NomineeGuid { get; set; }

        [Required]
        public Guid InternalProgramGuid { get; set; }

        public Guid? InternalProgramCategoryGuid { get; set; }

        [Required]
        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string JustificationComment { get; set; }
    }
}
