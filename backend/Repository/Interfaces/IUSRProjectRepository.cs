using LinkwayAPI.DTOs.User.Project;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUSRProjectRepository
    {
        Task<UsrProjectDisplayDTO> CreateProjectAsync(UsrProjectDTO project, string userId);
        Task<bool> DeleteProjectAsync(Guid projectGuid, string userId);
        Task<UsrProjectDisplayDTO> EditProjectAsync(Guid projectGuid, UsrProjectModifyDTO project, string userId);
        Task<IEnumerable<UsrProjectDisplayDTO>> GetAllProjectAsync(string userId);
        Task<UsrProjectDisplayDTO> GetProjectByIdAsync(Guid projectGuid, string userId);
    }
}