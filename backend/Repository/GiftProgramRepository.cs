using AutoMapper;
using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.GiftProgram;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.GiftProgram;
using LinkwayAPI.DTOs.Notification;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class GiftProgramRepository : IGiftProgramRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly UserManager<UsrUser> _managerUser;
        private readonly INotificationRepository _notificationRepository;
        public GiftProgramRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, UserManager<UsrUser> managerUser, INotificationRepository notificationRepository)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _managerUser = managerUser;
            _notificationRepository = notificationRepository;
        }

        public async Task<GiftProgramDisplayDTO> CreateGiftProgramAsync(GiftProgramDTO giftProgram, string userId)
        {
            try
            {
                var user = await _managerUser.FindByIdAsync(userId);
                if (user == null) return null;
                var currentDesignaton = await _dbContextLinkway.MstDesignations.SingleOrDefaultAsync(d => d.Designation == giftProgram.CurrentDesignationTitle);
                var desiredDesignation = await _dbContextLinkway.MstDesignations.SingleOrDefaultAsync(d => d.DesignationGuid == giftProgram.DesiredDesignationGuid);

                if (currentDesignaton == null || desiredDesignation == null) return null;

                var mappedGiftProgram = _mapper.Map<CndGiftform>(giftProgram);

                mappedGiftProgram.CandidateId = user.EmployeeCode;
                mappedGiftProgram.DesiredDesignationId = desiredDesignation.DesignationId;
                mappedGiftProgram.CurrentDesignationId = currentDesignaton.DesignationId;

                await _dbContextLinkway.CndGiftforms.AddAsync(mappedGiftProgram);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<GiftProgramDisplayDTO>(mappedGiftProgram);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<(bool, string)> CheckGiftProgram(string userId)
        {
            try
            {
                var user = await _managerUser.FindByIdAsync(userId);
                var userJoinDate = user.CreationDate;
                TimeSpan ts = DateTime.UtcNow - userJoinDate;
                if (ts.Days >= 365 && user != null)
                {
                    var isGiftProgramExists = await _dbContextLinkway.CndGiftforms.Where(U => U.CandidateId == user.EmployeeCode).OrderByDescending(u => u.CreationDate).FirstOrDefaultAsync();

                    if (isGiftProgramExists == null)
                        return (true, string.Empty);

                    TimeSpan isGiftProgramExistsWithinSixMonth = DateTime.UtcNow - isGiftProgramExists.CreationDate;

                    if (isGiftProgramExistsWithinSixMonth.TotalDays >= 183)
                    {
                        return (true, string.Empty);
                    }
                    else
                    {
                        return (false, GiftProgramResponse.WAIT_6_MONTH);
                    }
                }

                return (false, GiftProgramResponse.WAIT_1_YEAR);
            }
            catch
            {
                return (false, string.Empty);
            }
        }

        public async Task<(IEnumerable<GiftProgramDisplayDTO> List, int TotalCount)> GetAllGiftProgramAsync(List<int> adminStatus, List<int> managerStatus, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var totalCount = await _dbContextLinkway.CndGiftforms.CountAsync();
                var giftPrograms = await _dbContextLinkway.CndGiftforms
                    .Include(g => g.Candidate)
                    .Include(g => g.GiftformReviewer)
                    .Include(g => g.CurrentDesignation)
                    .Include(g => g.DesiredDesignation)
                    .OrderByDescending(g => g.CreationDate)

                    .ToListAsync();

                if (adminStatus.Count != 0)
                {
                    giftPrograms = giftPrograms.Where(g => adminStatus.Any(a => a == g.GiftformAdminStatus)).ToList();
                    totalCount = giftPrograms.Count;
                }

                if (managerStatus.Count != 0)
                {
                    giftPrograms = giftPrograms.Where(g => managerStatus.Any(m => m == g.GiftformManagerStatus)).ToList();
                    totalCount = giftPrograms.Count;
                }

                giftPrograms = giftPrograms.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var result = _mapper.Map<IEnumerable<GiftProgramDisplayDTO>>(giftPrograms);
                return (result, totalCount);
            }
            catch
            {
                return (null, 0);
            }
        }

        public async Task<IEnumerable<GiftProgramDisplayDTO>> GetAllGiftProgramByIdAsync(string userId)
        {
            try
            {
                var user = await _managerUser.FindByIdAsync(userId);
                if (user == null) return null;

                var giftPrograms = await _dbContextLinkway.CndGiftforms.Include(g => g.CurrentDesignation).Include(g => g.DesiredDesignation).Where(g => g.CandidateId == user.EmployeeCode).ToListAsync();
                var result = _mapper.Map<IEnumerable<GiftProgramDisplayDTO>>(giftPrograms);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<GiftProgramDisplayDTO> GetGiftProgramByIdAsync(Guid giftProgramGuid, string userId)
        {
            try
            {
                var giftProgram = await _dbContextLinkway.CndGiftforms.FirstOrDefaultAsync(g => g.GiftformGuid == giftProgramGuid && g.Candidate.Id == userId);
                var result = _mapper.Map<GiftProgramDisplayDTO>(giftProgram);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> EditGiftProgramAsync(Guid giftProgramGuid, GiftProgramModifyDTO giftProgram, string userId)
        {
            try
            {
                var userGiftProgram = await _dbContextLinkway.CndGiftforms.FirstOrDefaultAsync(g => g.GiftformGuid == giftProgramGuid && g.Candidate.Id == userId);

                if (userGiftProgram == null)
                    return false;

                userGiftProgram.GiftformManagerStatus = giftProgram.GiftformManagerStatus;
                userGiftProgram.GiftformAdminStatus = giftProgram.GiftformAdminStatus;
                userGiftProgram.GiftformRejectionReason = giftProgram.GiftformRejectionReason;

                _dbContextLinkway.CndGiftforms.Update(userGiftProgram);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                {
                    var admin = await _managerUser.GetUsersInRoleAsync(nameof(RoleTypes.Admin));

                    NotificationCreateDTO notification = new NotificationCreateDTO()
                    {
                        NotificationTitle = $"Gift approved",
                        NotificationDescription = $"{UserConstant.GIFT_FROM_OF} {userGiftProgram.Candidate.FirstName} {userGiftProgram.Candidate.FirstName} {UserConstant.APPROVED_BY_MANAGER}",
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

        public async Task<bool> UpdateStatusGiftProgramAsync(Guid giftProgramGuid, GiftProgramStatusDTO dtoGiftProgramStatus, string userId)
        {
            try
            {
                var userGiftProgram = await _dbContextLinkway.CndGiftforms.FirstOrDefaultAsync(g => g.GiftformGuid == giftProgramGuid && g.Candidate.Id == userId);

                if (userGiftProgram == null)
                    return false;

                var reviewer = await _managerUser.FindByIdAsync(dtoGiftProgramStatus.GiftformReviewerId);

                if (reviewer == null) return false;

                userGiftProgram.GiftformManagerStatus = dtoGiftProgramStatus.GiftformManagerStatus;
                userGiftProgram.GiftformAdminStatus = dtoGiftProgramStatus.GiftformAdminStatus;
                if (userGiftProgram.GiftformAdminStatus == 1)
                {
                    userGiftProgram.Candidate.Designation = userGiftProgram.DesiredDesignation;
                }
                userGiftProgram.GiftformRejectionReason = dtoGiftProgramStatus.GiftformRejectionReason;
                userGiftProgram.GiftformReviewerId = reviewer.EmployeeCode;

                _dbContextLinkway.CndGiftforms.Update(userGiftProgram);
                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteGiftProgramAsync(Guid giftProgramGuid, string userId)
        {
            try
            {
                var userGiftProgram = await _dbContextLinkway.CndGiftforms.FirstOrDefaultAsync(g => g.GiftformGuid == giftProgramGuid && g.Candidate.Id == userId);

                if (userGiftProgram == null)
                    return false;

                _dbContextLinkway.Remove(userGiftProgram);
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
    }
}
