using AutoMapper;
using LinkwayAPI.Constants.API;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.Notification;
using LinkwayAPI.DTOs.Request;
using LinkwayAPI.Enums.Request;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace LinkwayAPI.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly UserManager<UsrUser> _managerUser;
        private readonly IMapper _mapper;
        private readonly INotificationRepository _notificationRepository;
        public RequestRepository(LinkwayDbContext dbContextLinkway, UserManager<UsrUser> managerUser, IMapper mapper, INotificationRepository notificationRepository)
        {
            _dbContextLinkway = dbContextLinkway;
            _managerUser = managerUser;
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }


        public async Task<(IEnumerable<RequestListAdminDTO> List, int TotalCount)> GetAllRequestsAsync(List<int> status, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var totalCount = await _dbContextLinkway.RstRequests.CountAsync();
                var requestsList = await _dbContextLinkway.RstRequests
                    .Include(request => request.Requester)
                    .Include(request => request.Users)
                    .OrderByDescending(request => request.CreationDate)
                    .ToListAsync();

                if (status.Count != 0)
                {
                    requestsList = requestsList.Where(n => status.Any(a => a == n.RequestStatus)).ToList();
                    totalCount = requestsList.Count;
                }

                requestsList = requestsList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var result = _mapper.Map<IEnumerable<RequestListAdminDTO>>(requestsList);
                return (result, totalCount);
            }
            catch
            {
                return (null, 0);
            }
        }



        public async Task<RequestListDTO> CreateRequestAsync(RequestCreateDTO request, Claim? email)
        {
            try
            {

                var user = await _managerUser.FindByEmailAsync(email.Value.ToString());

                var rstRequest = _mapper.Map<RstRequest>(request);

                rstRequest.RequestGuid = Guid.NewGuid();
                rstRequest.RequesterId = user.EmployeeCode;
                rstRequest.RequestStatus = (int)RequestStatus.Pending;

                ICollection<UsrUser> users = await _dbContextLinkway.Users.Where(u => request.Users.Contains(u.Id)).ToListAsync();
                rstRequest.Users = users;

                await _dbContextLinkway.RstRequests.AddAsync(rstRequest);

                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0)
                {
                    var admin = await _managerUser.GetUsersInRoleAsync(nameof(RoleTypes.Admin));

                    NotificationCreateDTO notification = new NotificationCreateDTO()
                    {
                        NotificationTitle = "Request by manager",
                        NotificationDescription = $"{UserConstant.REQUEST_DONE} {user.FirstName} {user.LastName}",
                        UserGuid = admin.First().Id,
                    };
                    await _notificationRepository.CreateNotificationAsync(notification);

                    return _mapper.Map<RequestListDTO>(rstRequest);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public async Task<bool> DeleteRequestAsync(Guid requestGuid)
        {
            try
            {
                var curretRequest = await _dbContextLinkway.RstRequests.FirstOrDefaultAsync(r => r.RequestGuid == requestGuid);
                if (curretRequest == null)
                {
                    return false;
                }
                _dbContextLinkway.RstRequests.Remove(curretRequest);
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

        public async Task<bool> ApproveOrRejectRequestAsync(Guid requestGuid, RequestStatusUpdateDTO requestUpdate)
        {
            try
            {
                var curretRequest = await _dbContextLinkway.RstRequests.FirstOrDefaultAsync(request => request.RequestGuid == requestGuid);
                if (curretRequest == null)
                {
                    return false;
                }

                _mapper.Map(requestUpdate, curretRequest);

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
        public async Task<bool> ModifyRequestAsync(RequestModifyDTO request)
        {
            try
            {
                var curretRequest = await _dbContextLinkway.RstRequests.FirstOrDefaultAsync(req => req.RequestGuid == request.RequestId);
                if (curretRequest == null)
                {
                    return false;
                }
                _mapper.Map(request, curretRequest);

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

        public async Task<RequestListDTO> GetRequestByIdAsync(Guid requestGuid)
        {
            try
            {
                var curretRequest = await _dbContextLinkway.RstRequests.FirstOrDefaultAsync(request => request.RequestGuid == requestGuid);
                if (curretRequest == null)
                {
                    return null;
                }
                return _mapper.Map<RequestListDTO>(curretRequest);
            }
            catch
            {
                return null;
            }
        }

        public async Task<(IEnumerable<RequestListDTO> List, int TotalCount)> GetAllRequestsByUserIdAsync(int employeeCode, List<int> status, int pageNumber, int pageSize)
        {
            try
            {
                var totalCount = await _dbContextLinkway.RstRequests.Where(request => request.RequesterId == employeeCode).CountAsync();
                var requestsList = await _dbContextLinkway.RstRequests
                    .Where(request => request.RequesterId == employeeCode)
                    .OrderByDescending(request => request.CreationDate)
                    .ToListAsync();

                if (status.Count != 0)
                {
                    requestsList = requestsList.Where(n => status.Any(a => a == n.RequestStatus)).ToList();
                    totalCount = requestsList.Count;
                }

                requestsList = requestsList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var result = _mapper.Map<IEnumerable<RequestListDTO>>(requestsList);
                return (result, totalCount);
            }
            catch
            {
                return (null, 0);
            }
        }
    }
}
