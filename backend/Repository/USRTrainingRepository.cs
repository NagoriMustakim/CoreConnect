using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.User.Training;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class USRTrainingRepository : IUSRTrainingRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly UserManager<UsrUser> _managerUser;
        public USRTrainingRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, UserManager<UsrUser> managerUser)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _managerUser = managerUser;
        }

        public async Task<UsrTrainingDisplayDTO> CreateTrainingAsync(UsrTrainingDTO trainings, string userId)
        {
            try
            {
                var trainingType = await _dbContextLinkway.MstTrainingTypes.SingleOrDefaultAsync(t => t.TrainingTypeGuid == trainings.TrainingTypeGuid);
                var trainingExists = await _dbContextLinkway.MstTrainings.SingleOrDefaultAsync(t => t.TrainingGuid == trainings.TrainingNameGuid);
                var user = await _managerUser.FindByIdAsync(userId);

                if (user == null || trainingType == null || trainingExists == null) return null;

                var training = _mapper.Map<UsrTraining>(trainings);
                training.UserTrainingGuid = Guid.NewGuid();
                training.TrainingTypeId = trainingType.TrainingTypeId;
                training.UserId = user.EmployeeCode;
                training.TrainingId = trainingExists.TrainingId;
                await _dbContextLinkway.UsrTrainings.AddAsync(training);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrTrainingDisplayDTO>(training);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<UsrTrainingDisplayDTO>> GetAllTrainingAsync(string userId)
        {
            var trainingList = await _dbContextLinkway.UsrTrainings.Where(t => t.User.Id == userId).Include(t => t.TrainingType).OrderByDescending(training => training.IsTrainingActive).ThenByDescending(training => training.TrainingStartDate).ToListAsync();

            var result = _mapper.Map<IEnumerable<UsrTrainingDisplayDTO>>(trainingList);
            return result;
        }

        public async Task<UsrTrainingDisplayDTO> GetTrainingByIdAsync(Guid trainingGuid, string userId)
        {
            var usrTraining = await _dbContextLinkway.UsrTrainings.SingleOrDefaultAsync(t => t.UserTrainingGuid == trainingGuid && t.User.Id == userId);
            var result = _mapper.Map<UsrTrainingDisplayDTO>(usrTraining);
            return result;
        }

        public async Task<UsrTrainingDisplayDTO> EditTrainingAsync(Guid trainingGuid, UsrTrainingModifyDTO training, string userId)
        {
            var trainingType = await _dbContextLinkway.MstTrainingTypes.SingleOrDefaultAsync(t => t.TrainingTypeGuid == training.TrainingTypeGuid);
            var istraining = await _dbContextLinkway.MstTrainings.SingleOrDefaultAsync(t => t.TrainingGuid == training.TrainingNameGuid);

            var user = await _managerUser.FindByIdAsync(userId);
            var trainingExists = await _dbContextLinkway.UsrTrainings.SingleOrDefaultAsync(t => t.UserTrainingGuid == trainingGuid && t.UserId == user.EmployeeCode);

            if (trainingExists == null || trainingType == null || istraining == null)
                return null;

            trainingExists.TrainingTypeId = trainingType.TrainingTypeId;
            trainingExists.TrainingId = istraining.TrainingId;
            _mapper.Map(training, trainingExists);

            var result = await _dbContextLinkway.SaveChangesAsync();

            if (result > 0)
                return _mapper.Map<UsrTrainingDisplayDTO>(trainingExists);

            return null;
        }

        public async Task<bool> DeleteTrainingAsync(Guid trainingGuid, string userId)
        {
            var usrTraining = await _dbContextLinkway.UsrTrainings.SingleOrDefaultAsync(t => t.UserTrainingGuid == trainingGuid && t.User.Id == userId);
            if (usrTraining == null) return false;

            _dbContextLinkway.Remove(usrTraining);
            var result = await _dbContextLinkway.SaveChangesAsync();

            if (result > 0) return true;
            return false;
        }
    }
}
