using LinkwayAPI.DTOs.Skill;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface ISkillRepository
    {
        Task<(IEnumerable<SkillListDTO> List, int Count)> GetAllSkillsAsync(int pageNumber, int pageSize);

        Task<SkillListDTO> CreateSkillAsync(SkillCreateUpdateDTO dtoSkillCreateEdit);

        Task<SkillListDTO> UpdateSkillAsync(SkillCreateUpdateDTO dtoSkillCreateUpdate);

        Task<bool> DeleteSkillAsync(Guid skillGuid);

        Task<bool> IsSkillExists(string skillTitle, Guid? skillGuid);
    }
}
