
using LinkwayAPI.DTOs.Project;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IProjectRepository
    {
        Task<(IEnumerable<ProjectViewDTO> result, int totalCount)> GetAllProjectsAsync(int pageNumber, int pageSize);
        Task<bool> DoesProjectExists(string projectTitle, Guid? projectGuid);
        Task<ProjectViewDTO> CreateProjectAsync(ProjectAddDTO dtoProjectAdd);
        Task<ProjectViewDTO> UpdateProjectAsync(ProjectEditDTO dtoProjectEdit);
        Task<bool> DeleteProjectAsync(Guid projectGuid);
    }
}
