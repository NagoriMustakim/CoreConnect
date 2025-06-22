using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.User.Certificate
{
    public class UsrCertificateAddDTO
    {
        public Guid? SkillGuid { get; set; }
        [Required]

        public Guid CertificationGuid { get; set; }

        [Required]
        public Guid CompanyGuid { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CertificationIssueDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CertificationExpirationDate { get; set; }

        [MaxLength(250)]
        [RegularExpression(ValidationConstant.ALPHANUMERICS_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public string? CertificationCredentialId { get; set; }

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_NO_BRACKETS, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public string? CertificationCredentialUrl { get; set; }

        [MaxLength(500)]
        public string? CertificationPhotoName { get; set; }

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? CertificationDescription { get; set; }
    }
}
