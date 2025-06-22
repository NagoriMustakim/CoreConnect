namespace LinkwayAPI.DTOs.Skill
{
    public class SkillListDTO
    {
        public Guid SkillGuid { get; set; }

        public string SkillTitle { get; set; } = null!;

        public string? SkillDescription { get; set; }
    }
}
