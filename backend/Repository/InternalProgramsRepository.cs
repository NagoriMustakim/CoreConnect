using AutoMapper;
using LinkwayAPI.DTOs.InternalPrograms;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class InternalProgramsRepository : IInternalProgramsRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        public InternalProgramsRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InternalProgramViewDTO>> GetAllInternalProgramsAsync()
        {
            try
            {
                var internalPrograms = await _dbContextLinkway.MstInternalPrograms.ToListAsync();

                var mappedList = _mapper.Map<IEnumerable<InternalProgramViewDTO>>(internalPrograms);

                foreach (var internalProgram in mappedList)
                {
                    if (internalProgram.InternalProgramReviewCycle == 0)
                    {
                        internalProgram.IsActive = true;
                        return mappedList;
                    }

                    var todayDate = DateTime.Now;
                    var currentYear = DateTime.Now.Year;
                    var startDateOfYear = new DateTime(currentYear, 1, 1);
                    var dateAfterReviewCycle = startDateOfYear;

                    while (dateAfterReviewCycle.AddMonths(internalProgram.InternalProgramReviewCycle) <= todayDate)
                    {
                        dateAfterReviewCycle = dateAfterReviewCycle.AddMonths(internalProgram.InternalProgramReviewCycle);
                    }

                    var endDate = dateAfterReviewCycle.AddDays(internalProgram.InternalProgramActiveDays);

                    if (todayDate <= endDate)
                        internalProgram.IsActive = true;

                    internalProgram.InternalProgramStartDate = dateAfterReviewCycle;
                    internalProgram.InternalProgramEndDate = endDate;
                }

                return mappedList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddInternalProgramAsync(InternalProgramCreateDTO dtoInternalProgramCreate)
        {
            try
            {
                var internalProgram = _mapper.Map<MstInternalProgram>(dtoInternalProgramCreate);

                await _dbContextLinkway.AddAsync(internalProgram);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<InternalProgramViewDTO> GetInternalProgramByIdAsync(Guid internalProgramGuid)
        {
            try
            {
                var internalProgram = await _dbContextLinkway.MstInternalPrograms.FirstOrDefaultAsync(ip => ip.InternalProgramGuid == internalProgramGuid);

                var result = _mapper.Map<InternalProgramViewDTO>(internalProgram);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> EditInternalProgramAsync(Guid internalProgramGuid, InternalProgramModifyDTO dtoInternalProgramModify)
        {
            try
            {
                var existingInternalProgram = await _dbContextLinkway.MstInternalPrograms.FirstOrDefaultAsync(ip => ip.InternalProgramGuid == internalProgramGuid);
                if (existingInternalProgram == null) return false;

                var mappedInternalProgram = _mapper.Map(dtoInternalProgramModify, existingInternalProgram);

                _dbContextLinkway.MstInternalPrograms.Update(mappedInternalProgram);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteInrternalProgramAsync(Guid internalProgramGuid)
        {
            try
            {
                var existingInternalProgram = await _dbContextLinkway.MstInternalPrograms.SingleOrDefaultAsync(ip => ip.InternalProgramGuid == internalProgramGuid);
                if (existingInternalProgram == null) return false;
                _dbContextLinkway.Remove(existingInternalProgram);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsInternalProgamExistsAsync(string internalProgramName, Guid? internalProgramId)
        {
            var isExits = await _dbContextLinkway.MstInternalPrograms.SingleOrDefaultAsync(i => i.InternalProgramName.ToLower() == internalProgramName.ToLower());
            if (isExits == null) return false;

            if (isExits.InternalProgramGuid == internalProgramId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
