using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Comment
{
    public class CommentEditDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public Guid CommentGuid { get; set; }

        [Required]
        [StringLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]

        public string Comment { get; set; } = null!;
    }
}
