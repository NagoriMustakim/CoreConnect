using LinkwayAPI.DTOs.EmploymentType;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IEmploymentTypeRepository
    {
        Task<bool> IsEmploymentTypeExistsAsync(string EmploymentTypeTitle, Guid? employmentTypeGuid);
        Task<ActionResult<EmploymentTypeDTO>> AddEmploymentTypeAsync(EmploymentTypeDTO dtoEmploymentType);
        Task<IEnumerable<EmploymentTypeViewDTO>> GetAllEmploymentTypeAsync();
        Task<ActionResult<EmploymentTypeViewDTO>> GetEmploymentTypeByIdAsync(Guid employmentTypeId);
        Task<bool> UpdateEmploymentTypeAsync(EmploymentTypeModifyDTO dtoEmploymentTypeModify);
        Task<bool> DeleteEmploymentTypeAsync(Guid employmentTypeId);
    }
}
