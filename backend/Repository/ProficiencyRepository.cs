using AutoMapper;
using LinkwayAPI.DTOs.Proficiency;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class ProficiencyRepository : IProficiencyRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public ProficiencyRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }



        public async Task<IEnumerable<ProficiencyViewDTO>> GetAllProficiencyAsync()
        {
            try
            {
                var proficiencies = await _dbContextLinkway.MstProficiencies.ToListAsync();

                var proficiencyList = _mapper.Map<IEnumerable<ProficiencyViewDTO>>(proficiencies);
                return proficiencyList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateProficiencyAsync(ProficiencyCreateDTO dtoProficiencyCreate)
        {
            try
            {
                var proficiency = _mapper.Map<MstProficiency>(dtoProficiencyCreate);
                await _dbContextLinkway.AddAsync(proficiency);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0) return true;
                return false;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateProficiencyAsync(ProficiencyEditDTO dtoProficiencyEdit)
        {
            try
            {
                var exisitingProficiency = await _dbContextLinkway.MstProficiencies.SingleOrDefaultAsync(proficiency => proficiency.ProficiencyGuid == dtoProficiencyEdit.ProficiencyGuid);
                if (exisitingProficiency == null) return false;
                exisitingProficiency.ProficiencyTitle = dtoProficiencyEdit.ProficiencyTitle;
                exisitingProficiency.ProficiencyDescription = dtoProficiencyEdit.ProficiencyDescription;
                exisitingProficiency.ModificationDate = DateTime.UtcNow;
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProficiencyAsync(Guid proficiencyGuid)
        {
            try
            {
                var exisitingProficiency = await _dbContextLinkway.MstProficiencies.SingleOrDefaultAsync(proficiency => proficiency.ProficiencyGuid == proficiencyGuid);
                _dbContextLinkway.Remove(exisitingProficiency);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> CheckForExisitingDataAsync(string proficiencyTitle, Guid? proficiencyGuid)
        {
            try
            {
                var existsResult = await _dbContextLinkway.MstProficiencies.FirstOrDefaultAsync(proficiency => proficiency.ProficiencyTitle == proficiencyTitle);
                if (existsResult == null) return false;

                if (existsResult.ProficiencyGuid == proficiencyGuid) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
