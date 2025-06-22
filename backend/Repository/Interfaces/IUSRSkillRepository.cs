using LinkwayAPI.DTOs.User.Skill;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUSRSkillRepository
    {
        Task<UsrSkillDisplayDTO> CreateSkillAsync(UsrSkillDTO skill, string userId);
        Task<bool> DeleteSkillAsync(Guid skillId, string userId);
        Task<UsrSkillDisplayDTO> EditSkillAsync(Guid skillId, UsrSkillDTO skill, string userId);
        Task<IEnumerable<UsrSkillDisplayDTO>> GetAllSkillAsync(string userId);
        Task<UsrSkillDisplayDTO> GetSkillByIdAsync(Guid skillId, string userId);
        Task<bool> IsSkillExistsAsync(Guid? skillGuid, string userId);
    }
}