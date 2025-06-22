using LinkwayAPI.DTOs.TrainingType;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface ITrainingTypeRepository
    {
        Task<bool> IsTrainingTypeExistsAsync(string title, Guid? trainingTypeGuid);
        Task<ActionResult<TrainingTypeDTO>> AddTrainingTypeAsync(TrainingTypeDTO dtoTrainingType);
        Task<IEnumerable<TrainingTypeViewDTO>> GetAllTrainingTypeAsync();
        Task<ActionResult<TrainingTypeViewDTO>> GetTrainingTypeByIdAsync(Guid trainingTypeId);
        Task<bool> UpdateTrainingTypeAsync(Guid trainingTypeId, TrainingTypeModifyDTO dtoTrainingType);
        Task<bool> DeleteTrainingTypeAsync(Guid trainingTypeId);


    }
}
