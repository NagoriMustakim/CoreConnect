namespace LinkwayAPI.DTOs.User.Skill
{
    public class UsrSkillDisplayDTO
    {
        public Guid UserSkillGuid { get; set; }

        public string SkillTitle { get; set; }

        public string SkillGuid { get; set; }

        public int? ProficiencyId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

    }
}
