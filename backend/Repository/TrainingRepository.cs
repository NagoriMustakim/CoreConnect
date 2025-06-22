using AutoMapper;
using LinkwayAPI.DTOs.Training;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public TrainingRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<TrainingListDTO> list, int count)> getAllTrainingAsync(int pageNumber, int pageSize)
        {
            try
            {

                if (pageNumber != 0 && pageSize != 0)
                {
                    var totalCount = await _dbContextLinkway.MstTrainings.CountAsync();
                    var trainings = await _dbContextLinkway.MstTrainings.OrderByDescending(t => t.CreationDate).Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize).ToListAsync();
                    if (trainings != null)
                        return (_mapper.Map<IEnumerable<TrainingListDTO>>(trainings), totalCount);
                    return (null, 0);
                }
                else
                {
                    var trainings = await _dbContextLinkway.MstTrainings.OrderByDescending(t => t.CreationDate).ToListAsync();
                    if (trainings != null)
                        return (_mapper.Map<IEnumerable<TrainingListDTO>>(trainings), 0);
                    return (null, 0);
                }
            }

            catch
            {
                return (null, 0);
            }
        }

        public async Task<TrainingListDTO> CreateTrainingAsync(TrainingCreateDTO dtoTrainingCreate)
        {
            try
            {
                var training = _mapper.Map<MstTraining>(dtoTrainingCreate);
                await _dbContextLinkway.MstTrainings.AddAsync(training);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return _mapper.Map<TrainingListDTO>(training); ;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TrainingListDTO> UpdateTrainingAsync(TrainingModifyDTO dtoTrainingModify)
        {
            try
            {
                var existingTraining = await _dbContextLinkway.MstTrainings.FirstOrDefaultAsync(t => t.TrainingGuid == dtoTrainingModify.TrainingGuid);

                if (existingTraining == null) return null;

                _mapper.Map(dtoTrainingModify, existingTraining);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return _mapper.Map<TrainingListDTO>(existingTraining);
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteTrainingAsync(Guid TrainingGuid)
        {
            try
            {
                var existingTraining = await _dbContextLinkway.MstTrainings.FirstOrDefaultAsync(t => t.TrainingGuid == TrainingGuid);

                if (existingTraining == null) return false;

                _dbContextLinkway.Remove(existingTraining);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;

                return false;
            }
            catch { return false; }
        }

        public async Task<bool> IsTrainingExists(string trainingTitle, Guid? trainingGuid)
        {
            var existingTraining = await _dbContextLinkway.MstTrainings.SingleOrDefaultAsync(t => t.TrainingTitle.ToLower() == trainingTitle.ToLower());

            if (existingTraining == null) return false;

            if (existingTraining.TrainingGuid == trainingGuid) return false;

            return true;

        }
    }
}
