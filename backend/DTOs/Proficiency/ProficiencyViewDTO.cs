namespace LinkwayAPI.DTOs.Proficiency
{
    public class ProficiencyViewDTO
    {
        public Guid ProficiencyGuid { get; set; }

        public string ProficiencyTitle { get; set; } = null!;

        public string? ProficiencyDescription { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
