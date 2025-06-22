namespace LinkwayAPI.DTOs.Notification
{
    public class NotificationCreateDTO
    {
        public string UserGuid { get; set; }
        public string NotificationTitle { get; set; } = null!;
        public string? NotificationDescription { get; set; }
    }
}
