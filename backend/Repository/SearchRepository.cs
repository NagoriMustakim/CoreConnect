using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.Search;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LinkwayAPI.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly UserManager<UsrUser> _managerUser;
        private readonly RoleManager<IdentityRole> _managerRole;

        public SearchRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, UserManager<UsrUser> managerUser, RoleManager<IdentityRole> managerRole)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _managerUser = managerUser;
            _managerRole = managerRole;
        }

        public async Task<(List<SearchResultDTO> list, int Count)> SearchEmployeeAsync(string? keywords, int? currentCount, SearchFilterDTO dtoSearchFilter)
        {
            try
            {
                var admins = await _managerUser.GetUsersInRoleAsync(nameof(RoleTypes.Admin));
                var adminIds = admins.Select(admin => admin.Id).ToList();

                var query = await _dbContextLinkway.Users
                    .Include(u => u.UsrCertifications)
                    .Include(u => u.UsrExperiences)
                    .Include(u => u.UsrSkills)
                    .Include(u => u.UsrProjects)
                    .ThenInclude(u=>u.Project)
                    .Include(u => u.UsrTrainings)
                    .Include(u => u.Designation)
                    .Where(u => !adminIds.Contains(u.Id)).ToListAsync();

                var count = query.Count;

                if (!string.IsNullOrEmpty(keywords))
                {
                    query = query.Where(emp =>
                                            (emp.FirstName.ToLower().Contains(keywords.ToLower()) ||
                                             emp.LastName.ToLower().Contains(keywords.ToLower()) ||
                                            (emp.FirstName + " " + emp.LastName).ToLower().Contains(keywords.ToLower()) ||
                                             emp.UsrSkills.Any(us => us.Skill?.SkillTitle!=null && us.Skill.SkillTitle.ToLower().Contains(keywords.ToLower())) ||
                                        emp.UsrCertifications.Any(uc =>  uc.Certification?.CertificationTitle != null && uc.Certification.CertificationTitle.ToLower().Contains(keywords.ToLower())) ||
                                             emp.UsrProjects.Any(up => up.Project?.ProjectTitle!=null && up.Project.ProjectTitle.ToLower().Contains(keywords.ToLower())) ||
                                            (!string.IsNullOrEmpty(emp.Designation?.Designation) &&
                                             emp.Designation.Designation.ToLower().Contains(keywords.ToLower())) ||
                                             emp.UsrTrainings.Any(ut => ut.Training?.TrainingTitle!=null && ut.Training.TrainingTitle.ToLower().Contains(keywords.ToLower())))
                                    ).ToList();
                }
                if (!string.IsNullOrEmpty(dtoSearchFilter.Skills))
                {
                    query = query.Where(emp => emp.UsrSkills.Any(us => us.Skill?.SkillTitle != null && us.Skill.SkillTitle.ToLower().Contains(dtoSearchFilter.Skills.ToLower()))).ToList();
                }
                if (!string.IsNullOrEmpty(dtoSearchFilter.Certificates))
                {
                    query = query.Where(emp => emp.UsrCertifications.Any(uc => uc.Certification?.CertificationTitle != null &&
                                                                               uc.Certification.CertificationTitle.ToLower().Contains(dtoSearchFilter.Certificates.ToLower()))).ToList();
                }
                if (!string.IsNullOrEmpty(dtoSearchFilter.Projects))
                {
                    query = query.Where(emp => emp.UsrProjects.Any(up => up.Project?.ProjectTitle != null && up.Project.ProjectTitle.ToLower().Contains(dtoSearchFilter.Projects.ToLower()))).ToList();
                }
                if (!string.IsNullOrEmpty(dtoSearchFilter.Designation))
                {
                    query = query.Where(emp => emp.Designation?.Designation != null &&
                                               emp.Designation.Designation.ToLower().Contains(dtoSearchFilter.Designation.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(dtoSearchFilter.Training))
                {
                    query = query.Where(emp => emp.UsrTrainings.Any(ut => ut.Training?.TrainingTitle != null && ut.Training.TrainingTitle.ToLower().Contains(dtoSearchFilter.Training.ToLower()))).ToList();
                }
                if (dtoSearchFilter.Experience.HasValue)
                {
                    int minimumExperienceInYears = dtoSearchFilter.Experience.Value;
                    query = query.Where(emp => emp.UsrExperiences.Sum(exp => exp.CalculateExperienceYears()) >= minimumExperienceInYears).ToList();
                }
                count = query.Count;

                var resultList = query.OrderBy(q => q.FirstName).ThenBy(q => q.LastName).Skip(currentCount ?? 0).Take(10).ToList();
                var searchResult = new List<SearchResultDTO>();
                if (resultList.Any())
                {
                    foreach (var user in resultList)
                    {
                        if (!await _managerUser.IsInRoleAsync(user, nameof(RoleTypes.Admin)))
                        {
                            var role = await _managerUser.GetRolesAsync(user);
                            var searchResultDTO = new SearchResultDTO
                            {
                                UserId = user.Id,
                                EmployeeName = $"{user.FirstName} {user.LastName}",
                                ProfilePhotoName = user.ProfilePhotoName,
                                Designation = user.Designation?.Designation,
                                Role = role.ToList()
                            };
                            searchResult.Add(searchResultDTO);
                        }
                    }
                }

                return (searchResult, count);
            }
            catch
            {
                return (null, 0);
            }
        }
    }
}