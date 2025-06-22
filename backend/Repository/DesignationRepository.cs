using AutoMapper;
using LinkwayAPI.DTOs.Designation;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        public DesignationRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }


        public async Task<bool> IsDesignationExists(string designationTitle, Guid? designationGuid)
        {
            try
            {
                var existingDesignation = await _dbContextLinkway.MstDesignations.SingleOrDefaultAsync(designation => designation.Designation.ToLower() == designationTitle.ToLower());
                if (existingDesignation == null) return false;
                if (existingDesignation.DesignationGuid == designationGuid) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<(IEnumerable<DesignationViewDTO> result, int totalCount)> GetAllDesignationsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalCount = await _dbContextLinkway.MstDesignations.CountAsync();
                if (pageNumber > 0 && pageSize > 0)
                {
                    var designations = await _dbContextLinkway.MstDesignations.Include(designation=>designation.Department).OrderByDescending(designation => designation.CreationDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                    return (_mapper.Map<IEnumerable<DesignationViewDTO>>(designations), totalCount);
                }
                else
                {
                    var designations = await _dbContextLinkway.MstDesignations.Include(designation => designation.Department).OrderBy(designation=>designation.Department.Department).ThenBy(designation => designation.DesignationLevel).ToListAsync();
                    return (_mapper.Map<IEnumerable<DesignationViewDTO>>(designations), totalCount);
                }
            }
            catch
            {
                return (null, 0);
            }
        }

        public async Task<DesignationViewDTO> CreateDesignationAsync(DesignationAddDTO dtoDesignation)
        {
            try
            {
                var department = await _dbContextLinkway.MstDepartments.SingleOrDefaultAsync(d => d.DepartmentGuid == dtoDesignation.DepartmentGuid);

                var mappedDesingation = _mapper.Map<MstDesignation>(dtoDesignation);
                mappedDesingation.DepartmentId = department.DepartmentId;

                await _dbContextLinkway.MstDesignations.AddAsync(mappedDesingation);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0)
                    return _mapper.Map<DesignationViewDTO>(mappedDesingation);
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<DesignationViewDTO> UpdateDesignationAsync(DesignationEditDTO dtoDesignation)
        {
            try
            {
                var department = await _dbContextLinkway.MstDepartments.SingleOrDefaultAsync(dp => dp.DepartmentGuid == dtoDesignation.DepartmentGuid);
                var designation = await _dbContextLinkway.MstDesignations.SingleOrDefaultAsync(dg => dg.DesignationGuid == dtoDesignation.DesignationGuid);

                var updatedDesignation = _mapper.Map(dtoDesignation, designation);
                updatedDesignation.DepartmentId = department.DepartmentId;

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<DesignationViewDTO>(updatedDesignation);

                return null;
                //var existingDesingation = await _dbContextLinkway.MstDesignations.FirstOrDefaultAsync(designation => designation.DesignationGuid == dtoDesignation.DesignationGuid);
                //if (existingDesingation != null)
                //{
                //    var updatedDesignation = _mapper.Map(dtoDesignation, existingDesingation);
                //    var result = await _dbContextLinkway.SaveChangesAsync();

                //    if (result > 0)
                //        return _mapper.Map<DesignationViewDTO>(updatedDesignation);
                //}
                //return null;
            }
            catch { return null; }
        }

        public async Task<bool> DeleteDesignationAsync(Guid designationGuid)
        {
            try
            {
                var designation = await _dbContextLinkway.MstDesignations.SingleOrDefaultAsync(designation => designation.DesignationGuid == designationGuid);
                _dbContextLinkway.MstDesignations.Remove(designation);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsDesignationExists(Guid departmentGuid, string designation, Guid? designationGuid)
        {
            var department = await _dbContextLinkway.MstDepartments.SingleOrDefaultAsync(dp => dp.DepartmentGuid == departmentGuid);

            var existingdesignation = await _dbContextLinkway.MstDesignations.SingleOrDefaultAsync(dg => dg.Designation.ToLower() == designation.ToLower() && dg.DepartmentId == department.DepartmentId);

            if (existingdesignation == null) return false;

            if (existingdesignation.DesignationGuid == designationGuid) return false;

            return true;
        }

    }
}
