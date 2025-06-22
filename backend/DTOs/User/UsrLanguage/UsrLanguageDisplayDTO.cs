namespace LinkwayAPI.DTOs.User.UsrLanguage
{
    public class UsrLanguageDisplayDTO
    {
        public Guid LanguageGuid { get; set; }
        public string LanguageName { get; set; } = null!;
        public string ProficiencyGuid { get; set; }
        public string? ProficiencyTitle { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
