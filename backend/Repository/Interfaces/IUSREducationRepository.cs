using LinkwayAPI.DTOs.User.Education;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUSREducationRepository
    {
        Task<bool> AddEducationAsync(string email, UsrEducationDTO dtoUsrEducation);
        Task<UsrEducationViewDTO> GetEducationByIdAsync(string userId, Guid educationId);
        Task<bool> UpdateEducationAsync(string userId, Guid educationId, UsrEducationModifyDTO dtoUsrEducationModify);
        Task<bool> DeleteEducationAsync(string userId, Guid educationId);
        Task<IEnumerable<UsrEducationViewDTO>> GetAllEducationDetailsAsync(string userId);
    }
}
