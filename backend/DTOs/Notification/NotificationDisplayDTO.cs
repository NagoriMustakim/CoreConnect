namespace LinkwayAPI.DTOs.Notification
{
    public class NotificationDisplayDTO
    {
        public Guid NotificationGuid { get; set; }

        public string UserGuid { get; set; }

        public string NotificationTitle { get; set; } = null!;

        public string? NotificationDescription { get; set; }

        public bool IsNotificationRead { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
