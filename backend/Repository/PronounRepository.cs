using AutoMapper;
using LinkwayAPI.DTOs.Pronoun;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class PronounRepository : IPronounRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public PronounRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }
        public async Task<bool> CreatePronounAsync(PronounCreateEditDTO dtoPronounCreate)
        {
            try
            {
                var pronounCount = await _dbContextLinkway.MstPronouns.CountAsync();
                var existingPronoun = await _dbContextLinkway.MstPronouns.SingleOrDefaultAsync(pronoun => pronoun.Pronoun == dtoPronounCreate.Pronoun);
                if (existingPronoun != null)
                {
                    return false;
                }
                var pronoun = _mapper.Map<MstPronoun>(dtoPronounCreate);
                await _dbContextLinkway.MstPronouns.AddAsync(pronoun);
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


        public async Task<IEnumerable<PronounDisplayDTO>> GetPronounsList()
        {
            try
            {
                var pronounList = await _dbContextLinkway.MstPronouns.ToListAsync();
                var result = _mapper.Map<IEnumerable<PronounDisplayDTO>>(pronounList);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> EditPronounAsync(Guid pronounGuid, PronounCreateEditDTO dtoPronounEdit)
        {
            try
            {
                var existingPronoun = await _dbContextLinkway.MstPronouns.FirstOrDefaultAsync(pronoun => pronoun.PronounGuid == pronounGuid);
                if (existingPronoun == null) return false;
                existingPronoun.Pronoun = dtoPronounEdit.Pronoun;
                existingPronoun.PronounDescription = dtoPronounEdit.PronounDescription;
                existingPronoun.ModificationDate = DateTime.UtcNow;
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

        public async Task<bool> DeletePronounAsync(Guid pronounGuid)
        {
            try
            {
                var existingPronoun = await _dbContextLinkway.MstPronouns.FirstOrDefaultAsync(pronoun => pronoun.PronounGuid == pronounGuid);
                if (existingPronoun == null) return false;
                _dbContextLinkway.Remove(existingPronoun);
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

        public async Task<PronounDisplayDTO> GetPronounByIdAsync(Guid? pronounGuid)
        {
            try
            {
                if (pronounGuid != null)
                {
                    var pronoun = await _dbContextLinkway.MstPronouns.FirstOrDefaultAsync(pronoun => pronoun.PronounGuid == pronounGuid);
                    var result = _mapper.Map<PronounDisplayDTO>(pronoun);
                    return result;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsPronounExitsAsync(string pronoun, Guid? pronounGuId)
        {
            var result = await _dbContextLinkway.MstPronouns.SingleOrDefaultAsync(p => p.Pronoun == pronoun);

            if (result == null) return false;

            if (result.PronounGuid == pronounGuId)
            {
                return false;
            }
            return true;
        }
    }
}
