using AutoMapper;
using LinkwayAPI.DTOs.BusinessUnit;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class BusinessUnitRepository : IBusinessUnitRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly IFileRepository _reositoryFile;
        private readonly IWebHostEnvironment _environment;

        public BusinessUnitRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, IFileRepository reositoryFile, IWebHostEnvironment environment)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _reositoryFile = reositoryFile;
            _environment = environment;
        }

        public async Task<bool> CreateBusinessUnitAsync(BusinessUnitCreateEditDTO dtoBusinessUbitCreate)
        {
            try
            {

                var BusinessUnit = _mapper.Map<MstBusinessUnit>(dtoBusinessUbitCreate);
                await _dbContextLinkway.MstBusinessUnits.AddAsync(BusinessUnit);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditImageAsync(Guid businessUnitGuid, IFormFile imageFile)
        {
            try
            {
                var existingBusinessUnit = await _dbContextLinkway.MstBusinessUnits.FirstOrDefaultAsync(bu => bu.BusinessUnitGuid == businessUnitGuid);
                if (existingBusinessUnit == null) return false;

                if (existingBusinessUnit.BusinessUnitLogoName != null)
                {
                    var updateResult = _reositoryFile.EditImage(imageFile, existingBusinessUnit.BusinessUnitLogoName);
                    if (updateResult.Item1 == 1)
                    {
                        existingBusinessUnit.BusinessUnitLogoName = updateResult.Item2;
                    }
                    else
                    {
                        return false;
                    }
                }
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditBusinessUnitAsync(Guid businessUnitGuid, BusinessUnitCreateEditDTO dtoBusinessUnitEdit)
        {
            try
            {
                var existingBusinessUnit = await _dbContextLinkway.MstBusinessUnits.FirstOrDefaultAsync(bu => bu.BusinessUnitGuid == businessUnitGuid);

                if (existingBusinessUnit == null) return false;

                existingBusinessUnit.BusinessUnitName = dtoBusinessUnitEdit.BusinessUnitName;
                existingBusinessUnit.BusinessUnitDescription = dtoBusinessUnitEdit.BusinessUnitDescription;
                existingBusinessUnit.ModificationDate = DateTime.UtcNow;

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<BusinessUnitListDTO>> GetAllBusinessUnitsListAsync()
        {
            try
            {
                var businessUnits = await _dbContextLinkway.MstBusinessUnits.ToListAsync();
                if (businessUnits != null)
                    return _mapper.Map<IEnumerable<BusinessUnitListDTO>>(businessUnits);
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BusinessUnitListDTO> GetBusinessUnitByIdAsync(Guid businessUnitGuid)
        {
            try
            {
                var businessUnit = await _dbContextLinkway.MstBusinessUnits.FirstOrDefaultAsync(bu => bu.BusinessUnitGuid == businessUnitGuid);
                if (businessUnit != null)
                {
                    return _mapper.Map<BusinessUnitListDTO>(businessUnit);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteBusinessUnitAsync(Guid businessUnitGuid)
        {
            try
            {
                var exisitingBusinessUnit = await _dbContextLinkway.MstBusinessUnits.FirstOrDefaultAsync(bu => bu.BusinessUnitGuid == businessUnitGuid);

                if (exisitingBusinessUnit.BusinessUnitLogoName != null)
                {
                    var fileRemove = _reositoryFile.Deleteimage(exisitingBusinessUnit.BusinessUnitLogoName);
                }

                if (exisitingBusinessUnit == null) return false;
                _dbContextLinkway.Remove(exisitingBusinessUnit);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> isBusinessUnitExits(string bussinessUnitName, Guid? businessUnitGuid)
        {
            var businessUnit = await _dbContextLinkway.MstBusinessUnits.SingleOrDefaultAsync(b => b.BusinessUnitName.ToLower() == bussinessUnitName.ToLower());

            if (businessUnit == null) return false;

            if (businessUnit.BusinessUnitGuid == businessUnitGuid) return false;
            return true;
        }
    }
}
