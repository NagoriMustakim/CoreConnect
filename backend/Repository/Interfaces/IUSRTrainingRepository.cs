using LinkwayAPI.DTOs.User.Training;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUSRTrainingRepository
    {
        Task<UsrTrainingDisplayDTO> CreateTrainingAsync(UsrTrainingDTO trainings, string userId);
        Task<bool> DeleteTrainingAsync(Guid trainingGuid, string userId);
        Task<UsrTrainingDisplayDTO> EditTrainingAsync(Guid trainingGuid, UsrTrainingModifyDTO training, string userId);
        Task<IEnumerable<UsrTrainingDisplayDTO>> GetAllTrainingAsync(string userId);
        Task<UsrTrainingDisplayDTO> GetTrainingByIdAsync(Guid trainingGuid, string userId);
    }
}