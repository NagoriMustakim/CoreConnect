using AutoMapper;
using LinkwayAPI.DTOs.TrainingType;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class TrainingTypeRepository : ITrainingTypeRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public TrainingTypeRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<bool> IsTrainingTypeExistsAsync(string title, Guid? trainingGuid)
        {
            try
            {
                var trainingTypeExists = await _dbContextLinkway.MstTrainingTypes.SingleOrDefaultAsync(e => e.TrainingType.ToLower() == title.ToLower());

                if (trainingTypeExists == null) return false;

                if (trainingTypeExists.TrainingTypeGuid == trainingGuid) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ActionResult<TrainingTypeDTO>> AddTrainingTypeAsync(TrainingTypeDTO dtoTrainingType)
        {
            try
            {
                var trainingType = _mapper.Map<MstTrainingType>(dtoTrainingType);

                _dbContextLinkway.MstTrainingTypes.Add(trainingType);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return dtoTrainingType;
                return null;
            }
            catch
            {
                return null;
            }

        }

        public async Task<IEnumerable<TrainingTypeViewDTO>> GetAllTrainingTypeAsync()
        {
            try
            {
                var trainTypelist = await _dbContextLinkway.MstTrainingTypes.ToListAsync();

                return _mapper.Map<IEnumerable<TrainingTypeViewDTO>>(trainTypelist);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ActionResult<TrainingTypeViewDTO>> GetTrainingTypeByIdAsync(Guid trainingTypeGuid)
        {
            try
            {
                var result = await _dbContextLinkway.MstTrainingTypes.SingleOrDefaultAsync(e => e.TrainingTypeGuid == trainingTypeGuid);

                var trainingType = _mapper.Map<TrainingTypeViewDTO>(result);

                if (trainingType == null)
                    return null;
                return trainingType;
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> UpdateTrainingTypeAsync(Guid trainingTypeGuid, [FromBody] TrainingTypeModifyDTO dtoTrainingType)
        {
            try
            {
                var trainingType = await _dbContextLinkway.MstTrainingTypes.SingleOrDefaultAsync(e => e.TrainingTypeGuid == trainingTypeGuid);

                var trainTypemap = _mapper.Map(dtoTrainingType, trainingType);


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

        public async Task<bool> DeleteTrainingTypeAsync(Guid trainingTypeGuid)
        {
            try
            {
                var trainingType = await _dbContextLinkway.MstTrainingTypes.SingleOrDefaultAsync(e => e.TrainingTypeGuid == trainingTypeGuid);

                _dbContextLinkway.MstTrainingTypes.Remove(trainingType);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
