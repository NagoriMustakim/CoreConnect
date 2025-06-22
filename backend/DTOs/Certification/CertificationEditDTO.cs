using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Certification
{
    public class CertificationEditDTO
    {
        public Guid? CertificationGuid { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression(ValidationConstant.ALPHABETS_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public string CertificationTitle { get; set; } = null!;

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? CertificationDescription { get; set; }
    }
}
