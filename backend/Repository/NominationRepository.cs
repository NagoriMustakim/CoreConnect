using AutoMapper;
using LinkwayAPI.Constants.API;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.Nomination;
using LinkwayAPI.DTOs.Notification;
using LinkwayAPI.Enums.Nomination;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class NominationRepository : INominationRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly UserManager<UsrUser> _managerUser;
        private readonly IFileRepository _repositoryFile;
        private readonly INotificationRepository _notificationRepository;
        public NominationRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, UserManager<UsrUser> managerUser, IFileRepository repositoryFile, INotificationRepository notificationRepository)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _managerUser = managerUser;
            _repositoryFile = repositoryFile;
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> CreateNominationAsync(NominationDTO nomination, string userId, int iteration, IFormCollection? files)
        {
            try
            {

                var user = await _managerUser.FindByIdAsync(userId);
                if (user == null) return false;

                var nominee = await _dbContextLinkway.Users.FirstOrDefaultAsync(u => u.Id == nomination.NomineeGuid);
                var internalProgram = await _dbContextLinkway.MstInternalPrograms.FirstOrDefaultAsync(i => i.InternalProgramGuid == nomination.InternalProgramGuid);
                var internalProgramCategory = await _dbContextLinkway.MstInternalProgramCategories.FirstOrDefaultAsync(i => i.InternalProgramCategoryGuid == nomination.InternalProgramCategoryGuid);

                if (nominee == null || internalProgram == null) return false;

                var mappedNomination = _mapper.Map<NmsNomination>(nomination);
                mappedNomination.NominatorId = user.EmployeeCode;
                mappedNomination.NomineeId = nominee.EmployeeCode;
                mappedNomination.InternalProgramId = internalProgram.InternalProgramId;

                if (nomination.InternalProgramCategoryGuid != null && internalProgramCategory != null)
                    mappedNomination.InternalProgramCategoryId = internalProgramCategory.InternalProgramCategoryId;

                mappedNomination.NominationStatus = (int)NominationStatus.Pending;
                mappedNomination.CycleIteration = iteration;

                var nmsAttachments = new List<NmsAttachment>();

                if (files.Files.Any())
                {
                    foreach (var file in files.Files)
                    {
                        var fileName = _repositoryFile.SaveImage(file);
                        if (fileName.Item1 == 1)
                        {
                            nmsAttachments.Add(new NmsAttachment()
                            {
                                AttachmentGuid = Guid.NewGuid(),
                                AttachmentName = fileName.Item2,
                                CreationDate = DateTime.UtcNow,
                                ModificationDate = DateTime.UtcNow
                            });
                        }

                    }
                }

                mappedNomination.NmsAttachments = nmsAttachments;

                await _dbContextLinkway.NmsNominations.AddAsync(mappedNomination);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                {
                    var admin = await _managerUser.GetUsersInRoleAsync(nameof(RoleTypes.Admin));

                    NotificationCreateDTO notification = new NotificationCreateDTO()
                    {
                        NotificationTitle = "Nomination created",
                        NotificationDescription = $"{nominee.FirstName} {nominee.LastName} {UserConstant.NOMINATED_BY} {user.FirstName} {user.LastName}",
                        UserGuid = admin.First().Id,
                    };
                    await _notificationRepository.CreateNotificationAsync(notification);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<NominationDisplayDTO> GetNominationByIdAsync(Guid nominationGuid)
        {
            try
            {
                var nomination = await _dbContextLinkway.NmsNominations.Include(n => n.Nominator).Include(n => n.Nominee).Include(n => n.InternalProgram)
                    .Include(n => n.InternalProgramCategory).FirstOrDefaultAsync(n => n.NominationGuid == nominationGuid);
                if (nomination == null)
                {
                    return null;
                }
                else
                {
                    return _mapper.Map<NominationDisplayDTO>(nomination);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<(IEnumerable<NominationDisplayDTO> List, int Count)> GetAllNominationByNominationIdAsync(Guid internalProgramGuid, List<int> status, int pageNumber, int pageSize)
        {
            try
            {
                var totalNominations = await _dbContextLinkway.NmsNominations.Where(n => n.InternalProgram.InternalProgramGuid == internalProgramGuid).CountAsync();
                var nominationList = await _dbContextLinkway.NmsNominations
                    .Where(i => i.InternalProgram.InternalProgramGuid == internalProgramGuid)
                    .Include(n => n.Nominator)
                    .Include(n => n.Nominee)
                    .Include(n => n.InternalProgram)
                    .Include(n => n.InternalProgramCategory)
                    .OrderByDescending(n => n.CreationDate)
                    .ToListAsync();

                if (status.Count != 0)
                {
                    nominationList = nominationList.Where(n => status.Any(a => a == n.NominationStatus)).ToList();
                    totalNominations = nominationList.Count;
                }

                nominationList = nominationList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                return (_mapper.Map<IEnumerable<NominationDisplayDTO>>(nominationList), totalNominations);
            }
            catch
            {
                return (null, 0);
            }
        }

        public async Task<bool> DeleteNominationAsync(Guid nominationGuid)
        {
            var getNomination = await _dbContextLinkway.NmsNominations.FirstOrDefaultAsync(n => n.NominationGuid == nominationGuid);
            if (getNomination == null)
            {
                return false;
            }
            _dbContextLinkway.NmsNominations.Remove(getNomination);
            await _dbContextLinkway.SaveChangesAsync();
            return true;
        }

        public async Task<(bool, string)> IsNominationExistsAsync(string nomineeId, string nominatorid, Guid internalProgramGuid)
        {
            var nominee = await _dbContextLinkway.Users.FirstOrDefaultAsync(e => e.Id == nomineeId);
            var nominator = await _dbContextLinkway.Users.FirstOrDefaultAsync(e => e.Id == nominatorid);
            var internalProgram = await _dbContextLinkway.MstInternalPrograms.FirstOrDefaultAsync(e => e.InternalProgramGuid == internalProgramGuid);

            if (internalProgram.InternalProgramReviewCycle == 0)
                return (false, string.Empty);

            var isNominationExists = await _dbContextLinkway.NmsNominations.Where(n => n.NomineeId == nominee.EmployeeCode && n.InternalProgramId == internalProgram.InternalProgramId && n.NominatorId == nominator.EmployeeCode).OrderByDescending(n => n.CreationDate).FirstOrDefaultAsync();
            if (isNominationExists == null || isNominationExists.Nominator.Id != nominatorid)
            {
                return (false, string.Empty);
            }
            else
            {
                TimeSpan threeMonthTime = DateTime.UtcNow - isNominationExists.CreationDate;
                if (threeMonthTime.TotalDays > internalProgram.InternalProgramReviewCycle * 30)
                {
                    return (false, string.Empty);
                }
                return (true, $"{nominee.FirstName} {nominee.LastName} {UserConstant.ALREADY_NOMINATED}");

            }
        }

        public async Task<(IEnumerable<NominationDisplayDTO> List, int Count)> GetAllNominationAsync(string userId, List<int> status, int pageNumber, int pageSize)
        {
            try
            {
                var count = await _dbContextLinkway.NmsNominations.CountAsync();
                var nominationList = await _dbContextLinkway.NmsNominations
                    .Include(u => u.Nominee)
                    .Include(i => i.InternalProgram)
                    .Include(u => u.Nominator)
                    .Include(u => u.InternalProgramCategory)
                    .Include(u => u.NmsAttachments)
                    .Where(u => u.Nominator.Id == userId)
                    .OrderByDescending(n => n.CreationDate)
                    .ToListAsync();

                if (status.Count != 0)
                {
                    nominationList = nominationList.Where(n => status.Any(a => a == n.NominationStatus)).ToList();
                    count = nominationList.Count;
                }

                nominationList = nominationList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                return (_mapper.Map<IEnumerable<NominationDisplayDTO>>(nominationList), count);
            }
            catch
            {
                return (null, 0);
            }
        }

        public async Task<NominationDisplayDTO> ApproveNominationAsync(Guid nominationId)
        {
            try
            {
                var nomination = await _dbContextLinkway.NmsNominations.Include(n => n.Nominator).Include(n => n.Nominee).Include(n => n.InternalProgram).FirstOrDefaultAsync(n => n.NominationGuid == nominationId);
                nomination.NominationStatus = (int)NominationStatus.Approve;

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                {
                    return _mapper.Map<NominationDisplayDTO>(nomination);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<NominationDisplayDTO> RejectNominationAsync(Guid nominationId, NominationModifyDTO dtoNominationModify)
        {
            try
            {
                var nomination = await _dbContextLinkway.NmsNominations.Include(n => n.Nominator).Include(n => n.Nominee).Include(n => n.InternalProgram).FirstOrDefaultAsync(n => n.NominationGuid == nominationId);
                nomination.NominationStatus = (int)NominationStatus.Reject;
                nomination.NominationRejectionReason = dtoNominationModify.NominationRejectionReason;

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                {
                    return _mapper.Map<NominationDisplayDTO>(nomination);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<(bool, int)> IsNominationVaildAsync(Guid internalProgramGuid)
        {
            var internalProgram = await _dbContextLinkway.MstInternalPrograms.SingleOrDefaultAsync(ip => ip.InternalProgramGuid == internalProgramGuid);
            int iteration = 0;
            if (internalProgram == null)
                return (false, 0);

            if (internalProgram.InternalProgramReviewCycle == 0)
                return (true, iteration);

            var todayDate = DateTime.Now;
            var currentYear = DateTime.Now.Year;
            var startDateOfYear = new DateTime(currentYear, 1, 1);
            var dateAfterReviewCycle = startDateOfYear;

            while (dateAfterReviewCycle.AddMonths(internalProgram.InternalProgramReviewCycle) <= todayDate)
            {
                dateAfterReviewCycle = dateAfterReviewCycle.AddMonths(internalProgram.InternalProgramReviewCycle);
                iteration++;
            }

            if (todayDate <= dateAfterReviewCycle.AddDays(internalProgram.InternalProgramActiveDays))
                return (true, iteration);

            return (false, 0);

        }
    }
}