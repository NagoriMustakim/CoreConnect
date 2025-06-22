using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.Skill;
using LinkwayAPI.DTOs.User.Project;
using LinkwayAPI.DTOs.User.Skill;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class USRProjectRepository : IUSRProjectRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly UserManager<UsrUser> _managerUser;
        private readonly IUSRSkillRepository _repositorySkill;

        public USRProjectRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, UserManager<UsrUser> managerUser, IUSRSkillRepository repositorySkill)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _managerUser = managerUser;
            _repositorySkill = repositorySkill;
        }

        public async Task<UsrProjectDisplayDTO> CreateProjectAsync(UsrProjectDTO project, string userId)
        {
            try
            {
                var user = await _managerUser.FindByIdAsync(userId);
                if (user == null) return null;

                var usrProject = _mapper.Map<UsrProject>(project);

                var selectedPorject = await _dbContextLinkway.MstProjects.SingleOrDefaultAsync(p => p.ProjectGuid == project.ProjectGuid);
                if (selectedPorject == null) return null;
                usrProject.ProjectId = selectedPorject.ProjectId;
                usrProject.UserId = user.EmployeeCode;

                ICollection<MstSkill> skills = await _dbContextLinkway.MstSkills.Where(skill => project.SkillsGuids.Contains(skill.SkillGuid)).ToListAsync();
                usrProject.Skills = skills;

                await AddRemainingSkill(user,skills);

                await _dbContextLinkway.UsrProjects.AddAsync(usrProject);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrProjectDisplayDTO>(usrProject);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<UsrProjectDisplayDTO>> GetAllProjectAsync(string userId)
        {
            try
            {
                var usrProject = await _dbContextLinkway.UsrProjects.Include(p=>p.Project).Include(p => p.Skills).Where(t => t.User.Id == userId).OrderByDescending(p => p.IsProjectActive).ThenByDescending(p => p.ProjectStartDate).ToListAsync();
                var result = _mapper.Map<IEnumerable<UsrProjectDisplayDTO>>(usrProject);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UsrProjectDisplayDTO> GetProjectByIdAsync(Guid projectGuid, string userId)
        {
            try
            {
                var skillList = await _dbContextLinkway.UsrProjects.SingleOrDefaultAsync(t => t.UserProjectGuid == projectGuid && t.User.Id == userId);
                var result = _mapper.Map<UsrProjectDisplayDTO>(skillList);
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<UsrProjectDisplayDTO> EditProjectAsync(Guid projectGuid, UsrProjectModifyDTO project, string userId)
        {
            try
            {
                var user = await _managerUser.FindByIdAsync(userId);
                if (user == null) return null;

                var projectExists = await _dbContextLinkway.UsrProjects.FirstOrDefaultAsync(t => t.UserProjectGuid == project.UserProjectGuid && t.User.Id == userId);
                if (projectExists == null) return null;

                var selectedPorject = await _dbContextLinkway.MstProjects.SingleOrDefaultAsync(p => p.ProjectGuid == project.ProjectGuid);
                if (selectedPorject == null) return null;
                projectExists.ProjectId = selectedPorject.ProjectId;

                ICollection<MstSkill> skills = await _dbContextLinkway.MstSkills.Where(skill => project.SkillsGuids.Contains(skill.SkillGuid)).ToListAsync();
                projectExists.Skills = skills;

                await AddRemainingSkill(user, skills);

                _mapper.Map(project, projectExists);

                _dbContextLinkway.UsrProjects.Update(projectExists);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrProjectDisplayDTO>(projectExists); ;

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteProjectAsync(Guid projectGuid, string userId)
        {
            try
            {
                var projectExists = await _dbContextLinkway.UsrProjects.FirstOrDefaultAsync(t => t.UserProjectGuid == projectGuid && t.User.Id == userId);
                if (projectExists == null) return false;

                _dbContextLinkway.Remove(projectExists);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        private async Task AddRemainingSkill(UsrUser user, ICollection<MstSkill> skills)
        {
            try
            {
                ICollection<int> usrSkills = await _dbContextLinkway.UsrSkills.Where(skills => skills.UserId == user.EmployeeCode).Select(s => s.SkillId).ToListAsync();
                foreach (var skill in skills)
                {
                    if (!usrSkills.Contains(skill.SkillId))
                    {
                        UsrSkillDTO skillDto = new UsrSkillDTO()
                        {
                            SkillGuid = skill.SkillGuid,
                        };
                        await _repositorySkill.CreateSkillAsync(skillDto, user.Id);
                    }
                }
            }
            catch
            {

            }
        }
    }
}
