using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.User.UsrLanguage;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class USRLanguageRepository : IUSRLanguageRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly UserManager<UsrUser> _managerUser;


        public USRLanguageRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, UserManager<UsrUser> managerUser)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _managerUser = managerUser;
        }
        public async Task<UsrLanguageDisplayDTO> CreateLanguageAsync(UsrLanguageDTO language, string userId)
        {
            try
            {
                var user = await _managerUser.FindByIdAsync(userId);
                var proficiency = await _dbContextLinkway.MstProficiencies.SingleOrDefaultAsync(l => l.ProficiencyGuid == language.ProficiencyGuid);
                if (user == null) return null;

                var usrLanguage = _mapper.Map<UsrLanguage>(language);
                usrLanguage.UserId = user.EmployeeCode;

                if (proficiency != null)
                    usrLanguage.ProficiencyId = proficiency.ProficiencyId;

                await _dbContextLinkway.UsrLanguages.AddAsync(usrLanguage);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return _mapper.Map<UsrLanguageDisplayDTO>(usrLanguage);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<UsrLanguageDisplayDTO>> GetAllLanguageAsync(string userId)
        {
            try
            {
                var usrLanguageList = await _dbContextLinkway.UsrLanguages.Where(t => t.User.Id == userId).Include(t => t.Proficiency).ToListAsync();
                var result = _mapper.Map<IEnumerable<UsrLanguageDisplayDTO>>(usrLanguageList);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UsrLanguageDisplayDTO> GetLanguageByIdAsync(Guid languageGuid, string userId)
        {
            try
            {
                var usrLanguage = await _dbContextLinkway.UsrLanguages.Include(t => t.Proficiency).SingleOrDefaultAsync(t => t.LanguageGuid == languageGuid && t.User.Id == userId);
                var result = _mapper.Map<UsrLanguageDisplayDTO>(usrLanguage);
                return result;

            }
            catch
            {
                return null;
            }
        }

        public async Task<UsrLanguageDisplayDTO> EditLanguageAsync(Guid languageGuid, UsrLanguageModifiedDTO language, string userId)
        {
            try
            {
                var usrLanguage = await _dbContextLinkway.UsrLanguages.SingleOrDefaultAsync(t => t.LanguageGuid == languageGuid && t.User.Id == userId);
                var proficiency = await _dbContextLinkway.MstProficiencies.SingleOrDefaultAsync(l => l.ProficiencyGuid == language.ProficiencyGuid);

                if (usrLanguage == null) return null;

                _mapper.Map(language, usrLanguage);

                if (proficiency != null)
                    usrLanguage.ProficiencyId = proficiency.ProficiencyId;
                else
                    usrLanguage.ProficiencyId = null;

                _dbContextLinkway.UsrLanguages.Update(usrLanguage);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrLanguageDisplayDTO>(usrLanguage);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteLanguageAsync(Guid languageGuid, string userId)
        {
            try
            {
                var usrLanguage = await _dbContextLinkway.UsrLanguages.SingleOrDefaultAsync(t => t.LanguageGuid == languageGuid && t.User.Id == userId);

                _dbContextLinkway.Remove(usrLanguage);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsLanguageExistsAsync(string languageName, string userId, Guid? languageGuid)
        {
            try
            {
                var isExists = await _dbContextLinkway.UsrLanguages.SingleOrDefaultAsync(l => l.LanguageName.ToLower() == languageName.ToLower() && l.User.Id == userId);
                if (isExists == null) return false;

                if (isExists.LanguageGuid == languageGuid) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
