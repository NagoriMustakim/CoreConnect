using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Nomination
{
    public class NominationModifyDTO
    {
        [Required]
        public int NomineeId { get; set; }

        [Required]
        public int InternalProgramId { get; set; }

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string NominationRejectionReason { get; set; } = null!;
    }
}
