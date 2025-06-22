using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.User.Skill;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class USRSkillRepository : IUSRSkillRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly UserManager<UsrUser> _managerUser;

        public USRSkillRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, UserManager<UsrUser> managerUser)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _managerUser = managerUser;
        }

        public async Task<UsrSkillDisplayDTO> CreateSkillAsync(UsrSkillDTO dtoSkill, string userId)
        {
            try
            {
                var user = await _managerUser.FindByIdAsync(userId);
                if (user == null) return null;

                var usrSkill = _mapper.Map<UsrSkill>(dtoSkill);
                usrSkill.UserSkillGuid = Guid.NewGuid();
                usrSkill.CreationDate = DateTime.UtcNow;
                usrSkill.UserId = user.EmployeeCode;
                usrSkill.ProficiencyId = dtoSkill.ProficiencyId;

                var selectedSkill = await _dbContextLinkway.MstSkills.SingleOrDefaultAsync(skill => skill.SkillGuid == dtoSkill.SkillGuid);
                if (selectedSkill != null)
                    usrSkill.SkillId = selectedSkill.SkillId;

                await _dbContextLinkway.UsrSkills.AddAsync(usrSkill);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrSkillDisplayDTO>(usrSkill);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<UsrSkillDisplayDTO>> GetAllSkillAsync(string userId)
        {
            try
            {
                var skillList = await _dbContextLinkway.UsrSkills.Include(skill => skill.Skill).Where(t => t.User.Id == userId).ToListAsync();
                var result = _mapper.Map<IEnumerable<UsrSkillDisplayDTO>>(skillList);
                return result;

            }
            catch
            {
                return null;
            }
        }

        public async Task<UsrSkillDisplayDTO> GetSkillByIdAsync(Guid skillGuid, string userId)
        {
            try
            {
                var skillList = await _dbContextLinkway.UsrSkills.SingleOrDefaultAsync(t => t.UserSkillGuid == skillGuid && t.User.Id == userId);
                var result = _mapper.Map<UsrSkillDisplayDTO>(skillList);
                return result;

            }
            catch
            {
                return null;
            }
        }

        public async Task<UsrSkillDisplayDTO> EditSkillAsync(Guid skillGuid, UsrSkillDTO dtoUsrSkill, string userId)
        {
            try
            {
                var skill = await _dbContextLinkway.MstSkills.SingleOrDefaultAsync(s => s.SkillGuid == dtoUsrSkill.SkillGuid);

                var existingSkill = await _dbContextLinkway.UsrSkills.Include(s => s.Skill).FirstOrDefaultAsync(t => t.UserSkillGuid == dtoUsrSkill.UserSkillGuid && t.User.Id == userId);

                if (existingSkill == null) return null;

                existingSkill.SkillId = skill.SkillId;
                existingSkill.ProficiencyId = dtoUsrSkill.ProficiencyId;


                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrSkillDisplayDTO>(existingSkill);

                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> DeleteSkillAsync(Guid skillGuid, string userId)
        {
            try
            {
                var skillExists = await _dbContextLinkway.UsrSkills.SingleOrDefaultAsync(t => t.UserSkillGuid == skillGuid && t.User.Id == userId);
                if (skillExists == null) return false;
                _dbContextLinkway.Remove(skillExists);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsSkillExistsAsync(Guid? skillGuid, string userId)
        {
            try
            {
                var isSkillExists = await _dbContextLinkway.UsrSkills.Include(s => s.Skill).AnyAsync(s => s.Skill.SkillGuid == skillGuid && s.User.Id == userId);
                if (isSkillExists) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
