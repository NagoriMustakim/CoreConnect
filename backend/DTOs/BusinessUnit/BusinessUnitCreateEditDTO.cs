using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.BusinessUnit
{
    public class BusinessUnitCreateEditDTO
    {
        public Guid? BusinessUnitGuid { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(ValidationConstant.BUSINESS_NAME_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public string BusinessUnitName { get; set; }

        [StringLength(100)]
        public string? BusinessUnitLogoName { get; set; }

        [StringLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage =ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? BusinessUnitDescription { get; set; }
    }
}
