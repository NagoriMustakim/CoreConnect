using LinkwayAPI.DTOs.Skill;
using LinkwayAPI.Models;

namespace LinkwayAPI.DTOs.User.Project
{
    public class UsrProjectDisplayDTO
    {
        public Guid UserProjectGuid { get; set; }

        public string ProjectTitle { get; set; }

        public string ProjectGuid { get; set; }

        public DateTime ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        public bool IsProjectActive { get; set; }

        public virtual ICollection<SkillListDTO> Skills { get; set; } 

        public string? ProjectDescription { get; set; }
    }
}
