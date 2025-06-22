using LinkwayAPI.DTOs.User.Experience;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUSRExperienceRepository
    {
        Task<UsrExperienceViewDTO> AddExperienceAsync(string userId, UsrExperienceDTO dtoUsrExperience);
        Task<UsrExperienceViewDTO> GetExperienceByIdAsync(string userId, Guid experienceId);
        Task<UsrExperienceViewDTO> UpdateExperienceAsync(string userId, Guid experienceGuid, UsrExperienceModifyDTO dtoUsrExperienceModify);
        Task<bool> DeleteExperienceAsync(string userId, Guid experienceId);
        Task<IEnumerable<UsrExperienceViewDTO>> GetAllExperienceDetailsAsync(string userId);
    }
}
