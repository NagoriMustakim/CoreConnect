using AutoMapper;
using LinkwayAPI.DTOs.Notification;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public NotificationRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<NotificationDisplayDTO> CreateNotificationAsync(NotificationCreateDTO notificationCreate)
        {
            try
            {
                var notication = _mapper.Map<MstNotification>(notificationCreate);
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == notificationCreate.UserGuid);

                notication.UserId = user.EmployeeCode;

                await _dbContextLinkway.MstNotifications.AddAsync(notication);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                {
                    return _mapper.Map<NotificationDisplayDTO>(notication);
                }

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<NotificationDisplayDTO>> GetAllNotificationsAsync(string userId)
        {
            try
            {
                var notifications = await _dbContextLinkway.MstNotifications.Include(n => n.User).Where(t => t.User.Id == userId).ToListAsync();
                var result = _mapper.Map<IEnumerable<NotificationDisplayDTO>>(notifications);
                return result;
            }
            catch
            {
                return null;
            }
        }


        public async Task<NotificationDisplayDTO> UpdateNotficationAsync(Guid notificationGuid)
        {
            try
            {
                var notification = await _dbContextLinkway.MstNotifications.SingleOrDefaultAsync(s => s.NotificationGuid == notificationGuid);

                if (notification == null) return null;

                notification.IsNotificationRead = true;
                notification.ModificationDate = DateTime.UtcNow;

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<NotificationDisplayDTO>(notification);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteNotificationAsync(Guid notificationGuid)
        {
            try
            {
                var notification = await _dbContextLinkway.MstNotifications.SingleOrDefaultAsync(s => s.NotificationGuid == notificationGuid);

                if (notification == null) return false;

                _dbContextLinkway.Remove(notification);

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
