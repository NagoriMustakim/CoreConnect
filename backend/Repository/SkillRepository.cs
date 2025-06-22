using AutoMapper;
using LinkwayAPI.DTOs.Skill;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public SkillRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<SkillListDTO> List, int Count)> GetAllSkillsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalskills = await _dbContextLinkway.MstSkills.CountAsync();
                if (pageNumber > 0 && pageSize > 0)
                {
                    var skills = await _dbContextLinkway.MstSkills.OrderByDescending(skill => skill.CreationDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                    if (skills != null)
                        return (_mapper.Map<IEnumerable<SkillListDTO>>(skills), totalskills);
                }
                else
                {
                    var skills = await _dbContextLinkway.MstSkills.OrderBy(skill => skill.SkillTitle).ToListAsync();
                    if (skills != null)
                        return (_mapper.Map<IEnumerable<SkillListDTO>>(skills), totalskills);
                }

                return (null, 0);
            }
            catch
            {
                return (null, 0);
            }
        }

        public async Task<SkillListDTO> CreateSkillAsync(SkillCreateUpdateDTO dtoSkillCreateEdit)
        {
            try
            {
                var skill = _mapper.Map<MstSkill>(dtoSkillCreateEdit);
                await _dbContextLinkway.MstSkills.AddAsync(skill);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<SkillListDTO>(skill);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<SkillListDTO> UpdateSkillAsync(SkillCreateUpdateDTO dtoSkillCreateUpdate)
        {
            try
            {
                var existingSkill = await _dbContextLinkway.MstSkills.FirstOrDefaultAsync(s => s.SkillGuid == dtoSkillCreateUpdate.SkillGuid);

                if (existingSkill == null) return null;

                existingSkill.SkillTitle = dtoSkillCreateUpdate.SkillTitle;
                existingSkill.SkillDescription = dtoSkillCreateUpdate.SkillDescription;
                existingSkill.ModificationDate = DateTime.UtcNow;

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<SkillListDTO>(existingSkill);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteSkillAsync(Guid skillGuid)
        {
            try
            {
                var existingSkill = await _dbContextLinkway.MstSkills.FirstOrDefaultAsync(s => s.SkillGuid == skillGuid);

                if (existingSkill == null) return false;

                _dbContextLinkway.Remove(existingSkill);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsSkillExists(string skillTitle, Guid? skillGuid)
        {
            var existingSkill = await _dbContextLinkway.MstSkills.SingleOrDefaultAsync(s => s.SkillTitle.ToLower() == skillTitle.ToLower());

            if (existingSkill == null) return false;

            if (existingSkill.SkillGuid == skillGuid) return false;

            return true;
        }
    }
}
