using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Comment
{
    public class CommentCreateDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string Comment { get; set; }
    }
}
