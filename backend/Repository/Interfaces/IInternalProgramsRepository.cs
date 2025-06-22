using LinkwayAPI.DTOs.InternalPrograms;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IInternalProgramsRepository
    {
        Task<IEnumerable<InternalProgramViewDTO>> GetAllInternalProgramsAsync();
        Task<bool> AddInternalProgramAsync(InternalProgramCreateDTO dtoInternalProgramCreate);
        Task<InternalProgramViewDTO> GetInternalProgramByIdAsync(Guid internalProgramId);
        Task<bool> EditInternalProgramAsync(Guid internalProgramGuid, InternalProgramModifyDTO dtoInternalProgramModify);
        Task<bool> DeleteInrternalProgramAsync(Guid internalProgramId);
        Task<bool> IsInternalProgamExistsAsync(string InternalProgramName, Guid? internalProgramId);
    }
}
