using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.User.Project
{
    public class UsrProjectModifyDTO
    {
        public Guid UserProjectGuid { get; set; }
        
        [Required]
        public Guid? ProjectGuid { get; set; }
        
        public ICollection<Guid>? SkillsGuids { get; set; }

        public DateTime ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        public bool IsProjectActive { get; set; }
        [MaxLength(500)]
        [RegularExpression(ValidationConstant.DESCRIPTION_REGEX, ErrorMessage = ValidationConstant.DESCRIPTION_REGEX_MESSAGE)]
        public string? ProjectDescription { get; set; }

    }
}
