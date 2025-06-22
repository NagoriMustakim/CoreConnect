using LinkwayAPI.DTOs.Notification;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface INotificationRepository
    {
        Task<NotificationDisplayDTO> CreateNotificationAsync(NotificationCreateDTO notificationCreate);
        Task<bool> DeleteNotificationAsync(Guid notificationGuid);
        Task<IEnumerable<NotificationDisplayDTO>> GetAllNotificationsAsync(string userId);
        Task<NotificationDisplayDTO> UpdateNotficationAsync(Guid notificationGuid);
    }
}