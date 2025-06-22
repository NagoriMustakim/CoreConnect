using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.User.BasicDetail
{
    public class UsrBasicDetailModifyDTO
    {
        public Guid? BusinessUnitGuid { get; set; }

        public Guid? PronounGuid { get; set; }

        public Guid? DesignationGuid { get; set; }

        [Required]
        [RegularExpression(ValidationConstant.ALPHABETS_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        [MaxLength(50)]
        [MinLength(2)]
        public string FirstName { get; set; } = null!;
        [Required]
        [RegularExpression(ValidationConstant.ALPHABETS_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        [MaxLength(50)]
        [MinLength(2)]
        public string LastName { get; set; } = null!;
        [RegularExpression(ValidationConstant.REGEX_PATTERN_DIGITS, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        [MaxLength(10)]

        public string? PhoneNumber { get; set; } = null!;
        [RegularExpression(ValidationConstant.REGEX_PATTERN_DIGITS, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        [MaxLength(10)]

        public string? EmergencyContactNo { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public DateTime? BirthDate { get; set; }

        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        [MaxLength(1000)]
        public string? About { get; set; }

        public DateTime? JoiningDate { get; set; }

        public string? SkypeId { get; set; }

        public string? LinkedInUrl { get; set; }

    }
}
