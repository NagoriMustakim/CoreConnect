using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.BusinessUnit;
using LinkwayAPI.DTOs.Resume;
using LinkwayAPI.DTOs.User;
using LinkwayAPI.DTOs.User.Certificate;
using LinkwayAPI.DTOs.User.Experience;
using LinkwayAPI.DTOs.User.Project;
using LinkwayAPI.DTOs.User.Skill;
using LinkwayAPI.DTOs.User.Training;
using LinkwayAPI.DTOs.User.UsrLanguage;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UsrUser> _managerUser;
        private readonly RoleManager<IdentityRole> _managerRole;
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<UsrUser> managerUser, SignInManager<UsrUser> managerSignIn, RoleManager<IdentityRole> managerRole, IConfiguration configuration, LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _managerUser = managerUser;
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _managerRole = managerRole;
        }

        public async Task<object> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            try
            {
                var admin = await _managerRole.FindByNameAsync(nameof(RoleTypes.Admin));

                var userList = await _managerUser.Users.ToListAsync();
                var resultList = new List<UserDTO>();
                if (userList.Count > 0)
                {
                    foreach (var user in userList)
                    {
                        if (!await _managerUser.IsInRoleAsync(user, nameof(RoleTypes.Admin)))
                        {
                            var result = new UserDTO()
                            {
                                Id = user.Id,
                                UserName = user.UserName
                            };
                            resultList.Add(result);
                        }
                    }
                }
                var totalCount = resultList.Count();
                resultList = resultList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();


                return new { list = resultList, count = totalCount };
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> DeleteUserAsync(string userId)
        {
            try
            {
                var user = await _managerUser.Users.SingleOrDefaultAsync(u => u.Id == userId);

                _dbContextLinkway.Remove(user);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }


        public async Task<ResumeExportDTO> FetchResumeDataAsync(string userId, Guid businessUnitId)
        {
            try
            {

                var user = await _managerUser.FindByIdAsync(userId);
                if (user == null) return null;
                var expirience = await _dbContextLinkway.UsrExperiences.Where(exp => exp.UserId == user.EmployeeCode).OrderByDescending(exp => exp.ExperienceStartDate).Take(3).ToListAsync();
                var skills = await _dbContextLinkway.UsrSkills.Where(skill => skill.UserId == user.EmployeeCode).Take(3).ToListAsync();
                var languages = await _dbContextLinkway.UsrLanguages.Include(lang => lang.Proficiency).Where(lang => lang.UserId == user.EmployeeCode).Take(3).ToListAsync();
                var businessUnit = await _dbContextLinkway.MstBusinessUnits.SingleOrDefaultAsync(bu => bu.BusinessUnitGuid == businessUnitId);
                var project = await _dbContextLinkway.UsrProjects.Where(project => project.UserId == user.EmployeeCode).Take(3).ToListAsync();
                var certificates = await _dbContextLinkway.UsrCertifications.Where(certificate => certificate.UserId == user.EmployeeCode).Take(3).ToListAsync();
                var trainings = await _dbContextLinkway.UsrTrainings.Where(training => training.UserId == user.EmployeeCode).Take(3).ToListAsync();

                var mappedExperience = _mapper.Map<List<UsrExperienceViewDTO>>(expirience);
                var mappedSkills = _mapper.Map<List<UsrSkillDisplayDTO>>(skills);
                var mappedLanguages = _mapper.Map<List<UsrLanguageDisplayDTO>>(languages);
                var mappedBusinessUnit = _mapper.Map<BusinessUnitListDTO>(businessUnit);
                var mappedProjects = _mapper.Map<List<UsrProjectDisplayDTO>>(project);
                var mappedCertifications = _mapper.Map<List<UsrCertificateViewDTO>>(certificates);
                var mappedTrainings = _mapper.Map<List<UsrTrainingDisplayDTO>>(trainings);

                return new ResumeExportDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Designation = user.Designation.Designation,
                    About = user.About,
                    BusinessUnit = mappedBusinessUnit,
                    Experiences = mappedExperience,
                    Skills = mappedSkills,
                    Language = mappedLanguages,
                    Projects = mappedProjects,
                    Certifications = mappedCertifications,
                    Trainings = mappedTrainings,
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<object> GetProfileStrengthAsync(string userId)
        {
            try
            {

                var user = await _dbContextLinkway.Users.Include(u => u.Designation).SingleOrDefaultAsync(u => u.Id == userId);

                //var mappedBasicDetails = _mapper.Map<UsrBasicDetailViewDTO>(basicDetails);

                var countSkills = await _dbContextLinkway.UsrSkills.Where(u => u.UserId == user.EmployeeCode).CountAsync();
                var countCertificates = await _dbContextLinkway.UsrCertifications.Where(u => u.UserId == user.EmployeeCode).CountAsync();
                var countTrainings = await _dbContextLinkway.UsrTrainings.Where(u => u.UserId == user.EmployeeCode).CountAsync();
                var countProjects = await _dbContextLinkway.UsrProjects.Where(u => u.UserId == user.EmployeeCode).CountAsync();
                var latestGiftApplication = await _dbContextLinkway.CndGiftforms.Where(u => u.CandidateId == user.EmployeeCode).OrderByDescending(u => u.CreationDate).FirstOrDefaultAsync();
                TimeSpan extendedDate = DateTime.UtcNow.AddMonths(6) - DateTime.UtcNow;
                int giftTimeRemaining = 0;
                if (latestGiftApplication != null)
                {

                    TimeSpan giftTimePassed = DateTime.UtcNow - latestGiftApplication.CreationDate;

                    giftTimeRemaining = (int)extendedDate.TotalDays - (int)giftTimePassed.TotalDays;

                    if (giftTimeRemaining < 0)
                    {
                        giftTimeRemaining = 0;
                    }
                }

                int strengthTotal = 0;

                if (countSkills >= 3)
                    strengthTotal += 3;
                else
                    strengthTotal += countSkills;

                if (countCertificates >= 3)
                    strengthTotal += 3;
                else
                    strengthTotal += countCertificates;

                if (countTrainings >= 3)
                    strengthTotal += 3;
                else
                    strengthTotal += countTrainings;

                if (countProjects >= 3)
                    strengthTotal += 3;
                else
                    strengthTotal += countProjects;

                if (!string.IsNullOrEmpty(user.ProfilePhotoName))
                    strengthTotal += 1;

                if (!string.IsNullOrEmpty(user.About))
                    strengthTotal += 1;

                if (user.BirthDate != null)
                    strengthTotal += 1;

                if (!string.IsNullOrEmpty(user.City))
                    strengthTotal += 1;

                if (!string.IsNullOrEmpty(user.Designation.Designation))
                    strengthTotal += 1;

                if (!string.IsNullOrEmpty(user.PhoneNumber))
                    strengthTotal += 1;

                int strengthPercentage = Convert.ToInt32(((decimal)strengthTotal / 18) * 100);

                return new { countSkills, countCertificates, countTrainings, countProjects, giftTimeRemaining, strengthPercentage };
            }
            catch
            {
                return null;
            }
        }

    }
}
