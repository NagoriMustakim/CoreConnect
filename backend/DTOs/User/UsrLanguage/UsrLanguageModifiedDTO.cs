using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.User.UsrLanguage
{
    public class UsrLanguageModifiedDTO
    {
        public Guid? ProficiencyGuid { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LanguageName { get; set; } = null!;

    }
}
