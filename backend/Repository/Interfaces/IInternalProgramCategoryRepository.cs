using LinkwayAPI.DTOs.InternalProgramsCategories;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IInternalProgramCategoryRepository
    {
        Task<(IEnumerable<InternalProgramCategoryViewDTO> List, int Count)> GetAllCategoriesAsync(int pageNumber, int pageSize);

        Task<IEnumerable<InternalProgramCategoryViewDTO>> GetCategoriesByInternalProgramIdAsync(Guid internalProgramGuid);

        Task<InternalProgramCategoryViewDTO> CreateCategoryAsync(InternalProgramCategoryCreateDTO dtoInternalProgramCategoryCreate);

        Task<InternalProgramCategoryViewDTO> UpdateCategoryAsync(InternalProgramCategoryModifyDTO dtoInternalProgramCategoryModify);

        Task<bool> DeleteCategoryAsync(Guid internalProgramCategoryGuid);

        Task<bool> IsCategoryExists(Guid internalProgramGuid, string internalProgramCategory, Guid? internalProgramCategoryGuid);
    }
}
