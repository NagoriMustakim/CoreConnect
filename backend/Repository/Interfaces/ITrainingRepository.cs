using LinkwayAPI.DTOs.Training;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface ITrainingRepository
    {
        Task<TrainingListDTO> CreateTrainingAsync(TrainingCreateDTO dtoTrainingCreate);
        Task<bool> DeleteTrainingAsync(Guid TrainingGuid);
        Task<(IEnumerable<TrainingListDTO> list, int count)> getAllTrainingAsync(int pageNumber, int pageSize);
        Task<bool> IsTrainingExists(string trainingTitle, Guid? trainingGuid);
        Task<TrainingListDTO> UpdateTrainingAsync(TrainingModifyDTO dtoTrainingModify);
    }
}