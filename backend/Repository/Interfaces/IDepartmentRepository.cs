using LinkwayAPI.DTOs.Department;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<(IEnumerable<DepartmentViewDTO> result, int totalCount)> GetAllDepartmentAsync(int pageNumber, int pageSize);
        Task<DepartmentViewDTO> AddDeparmentAsync(DepartmentCreateDTO dtoDepartmentCreate);
        //Task<DepartmentViewDTO> GetDepartmentByIdAsync(Guid departmentGuid);
        Task<bool> UpdateDepartmentAsync(Guid departmentGuid, DepartmentModifyDTO dtodepartmentModify);
        Task<bool> DeleteDepartmentAsync(Guid departmentGuid);
        Task<bool> IsDepartmentExistsAsync(string DepartmentName, Guid? departmentGuid);

    }
}
