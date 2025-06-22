using LinkwayAPI.Constants.API;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.User;
using LinkwayAPI.Enums.Request;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Request
{
    public class RequestCreateDTO
    {
        [Required]
        [Range(0, 2)]
        public RequestType Type { get; set; }
        [Required]
        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string RequestDescription { get; set; }
        [Required]
        [Range(1, 1000)]
        [RegularExpression(ValidationConstant.REGEX_PATTERN_DIGITS, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public int NoOfMembers { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        public virtual ICollection<string> Users { get; set; } 

    }
}
