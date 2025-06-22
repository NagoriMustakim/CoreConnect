using AutoMapper;
using LinkwayAPI.DTOs.LocationType;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class LocationTypeRepository : ILocationTypeRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public LocationTypeRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<bool> IsLocationTypeExistsAsync(string name, Guid? locationGuid)
        {
            try
            {
                var empmtTypeexists = await _dbContextLinkway.MstLocationTypes.SingleOrDefaultAsync(e => e.LocationType.ToLower() == name.ToLower());

                if (empmtTypeexists == null) return false;

                if (empmtTypeexists.LocationTypeGuid == locationGuid) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ActionResult<LocationTypeDTO>> AddLocationTypeAsync(LocationTypeDTO dtoLocationType)
        {
            try
            {
                var empmtType = _mapper.Map<MstLocationType>(dtoLocationType);

                _dbContextLinkway.MstLocationTypes.Add(empmtType);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return dtoLocationType;
                return null;
            }
            catch
            {
                return null;
            }

        }

        public async Task<IEnumerable<LocationTypeViewDTO>> GetAllLocationTypeAsync()
        {
            try
            {
                var empmtTypelist = await _dbContextLinkway.MstLocationTypes.ToListAsync();

                return _mapper.Map<IEnumerable<LocationTypeViewDTO>>(empmtTypelist);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ActionResult<LocationTypeViewDTO>> GetLocationTypeByIdAsync(Guid locationTypeGuid)
        {
            try
            {
                var result = await _dbContextLinkway.MstLocationTypes.SingleOrDefaultAsync(e => e.LocationTypeGuid == locationTypeGuid);

                var empmtType = _mapper.Map<LocationTypeViewDTO>(result);

                if (empmtType == null)
                    return null;
                return empmtType;
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> UpdateLocationTypeAsync(LocationTypeModifyDTO dtoLocationType)
        {
            try
            {
                var empmtType = await _dbContextLinkway.MstLocationTypes.SingleOrDefaultAsync(e => e.LocationTypeGuid == dtoLocationType.LocationTypeGuid);

                var empmtTypemap = _mapper.Map(dtoLocationType, empmtType);
                if (empmtTypemap != null)
                    empmtTypemap.ModificationDate = DateTime.UtcNow;

                _dbContextLinkway.MstLocationTypes.Update(empmtTypemap);

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

        public async Task<bool> DeleteLocationTypeAsync(Guid locationTypeGuid)
        {
            try
            {
                var empmtType = await _dbContextLinkway.MstLocationTypes.SingleOrDefaultAsync(e => e.LocationTypeGuid == locationTypeGuid);

                _dbContextLinkway.MstLocationTypes.Remove(empmtType);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
