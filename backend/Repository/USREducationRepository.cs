using AutoMapper;
using LinkwayAPI.DTOs.User.Education;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class USREducationRepository : IUSREducationRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public USREducationRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<bool> AddEducationAsync(string email, UsrEducationDTO dtoUsrEducation)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Email == email);
                if (user == null)
                    return false;

                var educationDetail = _mapper.Map<UsrEducation>(dtoUsrEducation);
                educationDetail.UserId = user.EmployeeCode;

                await _dbContextLinkway.UsrEducations.AddAsync(educationDetail);

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

        public async Task<IEnumerable<UsrEducationViewDTO>> GetAllEducationDetailsAsync(string userId)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                var educationDetails = await _dbContextLinkway.UsrEducations.Where(c => c.UserId == user.EmployeeCode).ToListAsync();

                var educationDetailsmapped = _mapper.Map<IEnumerable<UsrEducationViewDTO>>(educationDetails);

                return educationDetailsmapped;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UsrEducationViewDTO> GetEducationByIdAsync(string userId, Guid educationGuid)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);
                var educationDetail = await _dbContextLinkway.UsrEducations.SingleOrDefaultAsync(c => c.EducationGuid == educationGuid && c.UserId == user.EmployeeCode);

                var educationDetailmapped = _mapper.Map<UsrEducationViewDTO>(educationDetail);

                return educationDetailmapped;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateEducationAsync(string userId, Guid educationGuid, UsrEducationModifyDTO dtoUsrEducationModify)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return false;

                var educationDetail = await _dbContextLinkway.UsrEducations.SingleOrDefaultAsync(d => d.EducationGuid == educationGuid && d.UserId == user.EmployeeCode);

                var educationDetailmapped = _mapper.Map(dtoUsrEducationModify, educationDetail);

                _dbContextLinkway.UsrEducations.Update(educationDetailmapped);

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

        public async Task<bool> DeleteEducationAsync(string userId, Guid educationGuid)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return false;

                var educationDetail = await _dbContextLinkway.UsrEducations.SingleOrDefaultAsync(d => d.EducationGuid == educationGuid && d.UserId == user.EmployeeCode);

                _dbContextLinkway.UsrEducations.Remove(educationDetail);

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
