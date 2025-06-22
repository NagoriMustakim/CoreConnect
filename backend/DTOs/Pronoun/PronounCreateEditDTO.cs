using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Pronoun
{
    public class PronounCreateEditDTO
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(ValidationConstant.PRONOUN_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]

        public string Pronoun { get; set; } = null!;

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? PronounDescription { get; set; }

    }
}
