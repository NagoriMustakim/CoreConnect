using AutoMapper;
using LinkwayAPI.DTOs.EmploymentType;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class EmploymentTypeRepository : IEmploymentTypeRepository
    {

        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public EmploymentTypeRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<bool> IsEmploymentTypeExistsAsync(string EmploymentTypeTitle, Guid? employmentTypeGuid)
        {
            try
            {
                var empmtTypeexists = await _dbContextLinkway.MstEmploymentTypes.FirstOrDefaultAsync(e => e.EmploymentTypeTitle.ToLower() == EmploymentTypeTitle.ToLower());

                if (empmtTypeexists == null)
                {
                    return false;
                }
                else
                {
                    if (empmtTypeexists.EmploymentTypeGuid == employmentTypeGuid)
                    {
                        return false;
                    }
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }


        public async Task<ActionResult<EmploymentTypeDTO>> AddEmploymentTypeAsync(EmploymentTypeDTO dtoEmploymentType)
        {
            try
            {

                var empmtType = _mapper.Map<MstEmploymentType>(dtoEmploymentType);

                _dbContextLinkway.MstEmploymentTypes.Add(empmtType);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return dtoEmploymentType;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<EmploymentTypeViewDTO>> GetAllEmploymentTypeAsync()
        {
            try
            {
                var empmtTypelist = await _dbContextLinkway.MstEmploymentTypes.ToListAsync();

                return _mapper.Map<IEnumerable<EmploymentTypeViewDTO>>(empmtTypelist);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ActionResult<EmploymentTypeViewDTO>> GetEmploymentTypeByIdAsync(Guid employmentTypeId)
        {
            try
            {
                var result = await _dbContextLinkway.MstEmploymentTypes.SingleOrDefaultAsync(e => e.EmploymentTypeGuid == employmentTypeId);

                var empmtType = _mapper.Map<EmploymentTypeViewDTO>(result);

                if (empmtType == null)
                    return null;
                return empmtType;
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> UpdateEmploymentTypeAsync(EmploymentTypeModifyDTO dtoEmploymentTypeModify)
        {
            try
            {
                var empmtType = await _dbContextLinkway.MstEmploymentTypes.SingleOrDefaultAsync(e => e.EmploymentTypeGuid == dtoEmploymentTypeModify.EmploymentTypeGuid);

                var empmtTypemap = _mapper.Map(dtoEmploymentTypeModify, empmtType);

                _dbContextLinkway.MstEmploymentTypes.Update(empmtTypemap);

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

        public async Task<bool> DeleteEmploymentTypeAsync(Guid employmentTypeId)
        {
            try
            {
                var empmtType = await _dbContextLinkway.MstEmploymentTypes.SingleOrDefaultAsync(e => e.EmploymentTypeGuid == employmentTypeId);

                _dbContextLinkway.MstEmploymentTypes.Remove(empmtType);

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
