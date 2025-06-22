using LinkwayAPI.DTOs.LocationType;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface ILocationTypeRepository
    {
        Task<bool> IsLocationTypeExistsAsync(string name, Guid? locationGuid);
        Task<ActionResult<LocationTypeDTO>> AddLocationTypeAsync(LocationTypeDTO dtoLocationType);
        Task<IEnumerable<LocationTypeViewDTO>> GetAllLocationTypeAsync();
        Task<ActionResult<LocationTypeViewDTO>> GetLocationTypeByIdAsync(Guid locationTypeId);
        Task<bool> UpdateLocationTypeAsync(LocationTypeModifyDTO dtoLocationType);
        Task<bool> DeleteLocationTypeAsync(Guid locationTypeId);
    }
}
