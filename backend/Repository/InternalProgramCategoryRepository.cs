using AutoMapper;
using LinkwayAPI.DTOs.InternalProgramsCategories;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class InternalProgramCategoryRepository : IInternalProgramCategoryRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public InternalProgramCategoryRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<InternalProgramCategoryViewDTO> List, int Count)> GetAllCategoriesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalCategories = await _dbContextLinkway.MstInternalProgramCategories.CountAsync();

                if (pageNumber > 0 && pageSize > 0)
                {
                    var categories = await _dbContextLinkway.MstInternalProgramCategories.Include(ip => ip.InternalProgram).OrderByDescending(category => category.CreationDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                    return (_mapper.Map<IEnumerable<InternalProgramCategoryViewDTO>>(categories), totalCategories);
                }
                else
                {
                    var categories = await _dbContextLinkway.MstInternalProgramCategories.OrderByDescending(category => category.InternalProgramCategory).ToListAsync();
                    return (_mapper.Map<IEnumerable<InternalProgramCategoryViewDTO>>(categories), totalCategories);
                }

            }
            catch
            {
                return (null, 0);
            }
        }

        public async Task<IEnumerable<InternalProgramCategoryViewDTO>> GetCategoriesByInternalProgramIdAsync(Guid internalProgramGuid)
        {
            try
            {
                var categories = await _dbContextLinkway.MstInternalProgramCategories.Include(category => category.InternalProgram).Where(category => category.InternalProgram.InternalProgramGuid == internalProgramGuid).OrderByDescending(category => category.InternalProgramCategory).ToListAsync();
                return _mapper.Map<IEnumerable<InternalProgramCategoryViewDTO>>(categories);
            }
            catch
            {
                return null;
            }
        }

        public async Task<InternalProgramCategoryViewDTO> CreateCategoryAsync(InternalProgramCategoryCreateDTO dtoInternalProgramCategoryCreate)
        {
            try
            {

                var internalProgram = await _dbContextLinkway.MstInternalPrograms.SingleOrDefaultAsync(ip => ip.InternalProgramGuid == dtoInternalProgramCategoryCreate.InternalProgramGuid);

                var category = _mapper.Map<MstInternalProgramCategory>(dtoInternalProgramCategoryCreate);
                category.InternalProgramId = internalProgram.InternalProgramId;

                _dbContextLinkway.MstInternalProgramCategories.Add(category);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<InternalProgramCategoryViewDTO>(category);

                return null;
            }
            catch
            {
                return null;
            }
        }


        public async Task<InternalProgramCategoryViewDTO> UpdateCategoryAsync(InternalProgramCategoryModifyDTO dtoInternalProgramCategoryModify)
        {
            try
            {
                var category = await _dbContextLinkway.MstInternalProgramCategories.SingleOrDefaultAsync(c => c.InternalProgramCategoryGuid == dtoInternalProgramCategoryModify.InternalProgramCategoryGuid);
                var internalProgram = await _dbContextLinkway.MstInternalPrograms.SingleOrDefaultAsync(ip => ip.InternalProgramGuid == dtoInternalProgramCategoryModify.InternalProgramGuid);

                _mapper.Map(dtoInternalProgramCategoryModify, category);

                category.InternalProgramId = internalProgram.InternalProgramId;

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<InternalProgramCategoryViewDTO>(category);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteCategoryAsync(Guid internalProgramCategoryGuid)
        {
            try
            {
                var category = await _dbContextLinkway.MstInternalProgramCategories.SingleOrDefaultAsync(c => c.InternalProgramCategoryGuid == internalProgramCategoryGuid);

                _dbContextLinkway.MstInternalProgramCategories.Remove(category);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }



        public async Task<bool> IsCategoryExists(Guid internalProgramGuid, string internalProgramCategory, Guid? internalProgramCategoryGuid)
        {
            var internalProgram = await _dbContextLinkway.MstInternalPrograms.SingleOrDefaultAsync(ip => ip.InternalProgramGuid == internalProgramGuid);

            var existingInternalprogramCategory = await _dbContextLinkway.MstInternalProgramCategories.SingleOrDefaultAsync(ipc => ipc.InternalProgramCategory.ToLower() == internalProgramCategory.ToLower() && ipc.InternalProgramId == internalProgram.InternalProgramId);

            if (existingInternalprogramCategory == null) return false;

            if (existingInternalprogramCategory.InternalProgramCategoryGuid == internalProgramCategoryGuid) return false;

            return true;
        }


    }
}
