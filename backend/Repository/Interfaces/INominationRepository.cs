using LinkwayAPI.DTOs.Nomination;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface INominationRepository
    {
        Task<bool> CreateNominationAsync(NominationDTO nomination, string userId,int iteration, IFormCollection? files);
        Task<bool> DeleteNominationAsync(Guid id);
        Task<(IEnumerable<NominationDisplayDTO> List, int Count)> GetAllNominationByNominationIdAsync(Guid internalProgramGuid, List<int> status, int pageNumber, int pageSize);
        Task<NominationDisplayDTO> GetNominationByIdAsync(Guid id);
        Task<(bool, string)> IsNominationExistsAsync(string nomineeId, string nominatorid, Guid internalProgramGuid);
        Task<(IEnumerable<NominationDisplayDTO> List, int Count)> GetAllNominationAsync(string userId, List<int> status, int pageNumber, int pageSize);
        Task<NominationDisplayDTO> ApproveNominationAsync(Guid nominationId);
        Task<NominationDisplayDTO> RejectNominationAsync(Guid nominationId, NominationModifyDTO dtoNominationModify);
        Task<(bool, int)> IsNominationVaildAsync(Guid internalProgramGuid);
    }
}