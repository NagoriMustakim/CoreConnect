using LinkwayAPI.DTOs.BusinessUnit;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IBusinessUnitRepository
    {
        Task<bool> CreateBusinessUnitAsync(BusinessUnitCreateEditDTO dtoBusinessUbitCreate);
        Task<bool> DeleteBusinessUnitAsync(Guid businessUnitId);
        Task<bool> EditImageAsync(Guid businessUnitId, IFormFile imageFile);
        Task<bool> EditBusinessUnitAsync(Guid businessUnitId, BusinessUnitCreateEditDTO dtoBusinessUbitCreate);
        Task<IEnumerable<BusinessUnitListDTO>> GetAllBusinessUnitsListAsync();
        Task<BusinessUnitListDTO> GetBusinessUnitByIdAsync(Guid businessUnitId);
        Task<bool> isBusinessUnitExits(string bussinessUnitName, Guid? businessUnitGuid);
    }
}
