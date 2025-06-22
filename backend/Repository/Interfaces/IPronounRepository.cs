using LinkwayAPI.DTOs.Pronoun;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IPronounRepository
    {
        Task<IEnumerable<PronounDisplayDTO>> GetPronounsList();
        Task<bool> CreatePronounAsync(PronounCreateEditDTO dtoPronounCreate);
        Task<bool> EditPronounAsync(Guid PronounId, PronounCreateEditDTO dtoPronounCreate);
        Task<bool> DeletePronounAsync(Guid pronounId);
        Task<PronounDisplayDTO> GetPronounByIdAsync(Guid? pronounId);
        Task<bool> IsPronounExitsAsync(string pronoun, Guid? pronounGuId);
    }
}
