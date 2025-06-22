using LinkwayAPI.DTOs.Proficiency;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IProficiencyRepository
    {

        Task<IEnumerable<ProficiencyViewDTO>> GetAllProficiencyAsync();

        Task<bool> CreateProficiencyAsync(ProficiencyCreateDTO dtoProficiencyCreate);
        Task<bool> UpdateProficiencyAsync(ProficiencyEditDTO dtoProficiencyEdit);
        Task<bool> DeleteProficiencyAsync(Guid proficiencyId);
        Task<bool> CheckForExisitingDataAsync(string proficiencyTitle, Guid? proficiencyGuid);
    }
}
