using LinkwayAPI.DTOs.User.UsrLanguage;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUSRLanguageRepository
    {
        Task<UsrLanguageDisplayDTO> CreateLanguageAsync(UsrLanguageDTO language, string userId);
        Task<bool> DeleteLanguageAsync(Guid languageId, string userId);
        Task<UsrLanguageDisplayDTO> EditLanguageAsync(Guid languageId, UsrLanguageModifiedDTO language, string userId);
        Task<IEnumerable<UsrLanguageDisplayDTO>> GetAllLanguageAsync(string userId);
        Task<UsrLanguageDisplayDTO> GetLanguageByIdAsync(Guid languageId, string userId);
        Task<bool> IsLanguageExistsAsync(string languageName, string userId, Guid? languageGuid);
    }
}