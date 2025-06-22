using AutoMapper;
using LinkwayAPI.DTOs.Department;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        public DepartmentRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<DepartmentViewDTO> AddDeparmentAsync(DepartmentCreateDTO dtoDepartmentCreate)
        {
            try
            {
                var department = _mapper.Map<MstDepartment>(dtoDepartmentCreate);

                _dbContextLinkway.MstDepartments.Add(department);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<DepartmentViewDTO>(department);
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteDepartmentAsync(Guid departmentGuid)
        {
            try
            {
                var department = await _dbContextLinkway.MstDepartments.SingleOrDefaultAsync(d => d.DepartmentGuid == departmentGuid);

                _dbContextLinkway.MstDepartments.Remove(department);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateDepartmentAsync(Guid departmentGuid, DepartmentModifyDTO dtodepartmentModify)
        {
            try
            {
                var department = await _dbContextLinkway.MstDepartments.SingleOrDefaultAsync(d => d.DepartmentGuid == departmentGuid);

                var deaprtmentmapped = _mapper.Map(dtodepartmentModify, department);

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


        public async Task<(IEnumerable<DepartmentViewDTO> result, int totalCount)> GetAllDepartmentAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _dbContextLinkway.MstDepartments.CountAsync();
            if (pageNumber > 0 && pageSize > 0)
            {
                var departmentlist = await _dbContextLinkway.MstDepartments.OrderByDescending(department => department.CreationDate).Skip((pageNumber - 1) * pageNumber).Take(pageSize).ToListAsync();
                return (_mapper.Map<IEnumerable<DepartmentViewDTO>>(departmentlist), totalCount);
            }
            else
            {
                var departmentlist = await _dbContextLinkway.MstDepartments.OrderByDescending(department => department.CreationDate).ToListAsync();
                return (_mapper.Map<IEnumerable<DepartmentViewDTO>>(departmentlist), totalCount);
            }
        }

        //public async Task<DepartmentViewDTO> GetDepartmentByIdAsync(Guid departmentId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> IsDepartmentExistsAsync(string DepartmentName, Guid? departmentGuid)
        {
            try
            {
                var departmentExists = await _dbContextLinkway.MstDepartments.SingleOrDefaultAsync(d => d.Department.ToLower() == DepartmentName.ToLower());

                if (departmentExists != null) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
