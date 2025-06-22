
using LinkwayAPI.DTOs.Designation;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IDesignationRepository
    {
        Task<(IEnumerable<DesignationViewDTO> result, int totalCount)> GetAllDesignationsAsync(int pageNumber, int pageSize);
        Task<bool> IsDesignationExists(Guid departmentGuid, string designation, Guid? designationGuid);
        Task<DesignationViewDTO> CreateDesignationAsync(DesignationAddDTO dtoDesignation);
        Task<DesignationViewDTO> UpdateDesignationAsync(DesignationEditDTO dtoDesignation);
        Task<bool> DeleteDesignationAsync(Guid designationGuid);
    }
}
