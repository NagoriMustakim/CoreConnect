using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.LocationType
{
    public class LocationTypeModifyDTO
    {
        public Guid LocationTypeGuid { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(ValidationConstant.ALPHANUMERICS_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]

        public string LocationType { get; set; }

        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? LocationTypeDescription { get; set; }
    }
}
