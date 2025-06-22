using AutoMapper;
using LinkwayAPI.DTOs.User.Experience;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class USRExperienceRepository : IUSRExperienceRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public USRExperienceRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<UsrExperienceViewDTO> AddExperienceAsync(string userId, UsrExperienceDTO dtoUsrExperience)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                    return null;

                var experienceDetail = _mapper.Map<UsrExperience>(dtoUsrExperience);
                experienceDetail.UserId = user.EmployeeCode;

                var employmentType = await _dbContextLinkway.MstEmploymentTypes.SingleOrDefaultAsync(e => e.EmploymentTypeGuid == dtoUsrExperience.EmploymentTypeGuid);
                if (employmentType != null)
                    experienceDetail.EmploymentTypeId = employmentType.EmploymentTypeId;

                var locationType = await _dbContextLinkway.MstLocationTypes.SingleOrDefaultAsync(l => l.LocationTypeGuid == dtoUsrExperience.LocationTypeGuid);
                if (locationType != null)
                    experienceDetail.LocationTypeId = locationType.LocationTypeId;

                var company = await _dbContextLinkway.MstCompanies.SingleOrDefaultAsync(company=>company.CompanyGuid == dtoUsrExperience.CompanyGuid);
                if (company != null)
                    experienceDetail.CompanyId = company.CompanyId;

                var designation = await _dbContextLinkway.MstDesignations.SingleOrDefaultAsync(designation => designation.DesignationGuid == dtoUsrExperience.DesignationGuid);
                if (designation != null)
                    experienceDetail.DesignationId = designation.DesignationId;

                await _dbContextLinkway.UsrExperiences.AddAsync(experienceDetail);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrExperienceViewDTO>(experienceDetail);

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<UsrExperienceViewDTO>> GetAllExperienceDetailsAsync(string userId)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                var experienceDetails = await _dbContextLinkway.UsrExperiences.Include(e => e.EmploymentType).Include(e => e.LocationType).Where(u => u.UserId == user.EmployeeCode).OrderByDescending(e => e.ExperienceStartDate).ToListAsync();

                var experienceDetailsMapped = _mapper.Map<IEnumerable<UsrExperienceViewDTO>>(experienceDetails);

                return experienceDetailsMapped;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UsrExperienceViewDTO> GetExperienceByIdAsync(string userId, Guid experienceGuid)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                var experienceDetail = await _dbContextLinkway.UsrExperiences.Include(e => e.EmploymentType).Include(e => e.LocationType).SingleOrDefaultAsync(d => d.ExperienceGuid == experienceGuid && d.UserId == user.EmployeeCode);

                var experienceDetailsMapped = _mapper.Map<UsrExperienceViewDTO>(experienceDetail);

                return experienceDetailsMapped;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UsrExperienceViewDTO> UpdateExperienceAsync(string userId, Guid experienceGuid, UsrExperienceModifyDTO dtoUsrExperienceModify)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return null;

                var experienceDetail = await _dbContextLinkway.UsrExperiences.SingleOrDefaultAsync(d => d.ExperienceGuid == experienceGuid && d.UserId == user.EmployeeCode);

                var experienceDetailmapped = _mapper.Map(dtoUsrExperienceModify, experienceDetail);

                var employmentType = await _dbContextLinkway.MstEmploymentTypes.SingleOrDefaultAsync(e => e.EmploymentTypeGuid == dtoUsrExperienceModify.EmploymentTypeGuid);
                if (employmentType != null)
                    experienceDetailmapped.EmploymentTypeId = employmentType.EmploymentTypeId;
                else
                    experienceDetailmapped.EmploymentTypeId = null;


                var locationType = await _dbContextLinkway.MstLocationTypes.SingleOrDefaultAsync(l => l.LocationTypeGuid == dtoUsrExperienceModify.LocationTypeGuid);
                if (locationType != null)
                    experienceDetailmapped.LocationTypeId = locationType.LocationTypeId;
                else
                    experienceDetailmapped.LocationTypeId = null;
                
                var company = await _dbContextLinkway.MstCompanies.SingleOrDefaultAsync(company => company.CompanyGuid == dtoUsrExperienceModify.CompanyGuid);
                if (company != null)
                    experienceDetailmapped.CompanyId = company.CompanyId;
                else
                    return null;

                var designation = await _dbContextLinkway.MstDesignations.SingleOrDefaultAsync(designation => designation.DesignationGuid == dtoUsrExperienceModify.DesignationGuid);
                if (designation != null)
                    experienceDetailmapped.DesignationId = designation.DesignationId;
                else
                    return null;

                _dbContextLinkway.UsrExperiences.Update(experienceDetailmapped);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrExperienceViewDTO>(experienceDetailmapped);

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> DeleteExperienceAsync(string userId, Guid experienceGuid)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return false;

                var experienceDetail = await _dbContextLinkway.UsrExperiences.SingleOrDefaultAsync(d => d.ExperienceGuid == experienceGuid && d.UserId == user.EmployeeCode);

                _dbContextLinkway.UsrExperiences.Remove(experienceDetail);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
