using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Proficiency
{
    public class ProficiencyEditDTO
    {
        [Required]
        public Guid ProficiencyGuid { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProficiencyTitle { get; set; }
        [MaxLength(500)]
        public string? ProficiencyDescription { get; set; }
    }
}
