using LinkwayAPI.DTOs.Resume;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<object> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<bool> DeleteUserAsync(string userId);
        Task<ResumeExportDTO> FetchResumeDataAsync(string userId, Guid businessUnitId);
        Task<object> GetProfileStrengthAsync(string userId);
    }
}
