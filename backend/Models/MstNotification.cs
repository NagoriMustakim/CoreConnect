using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class MstNotification
{
    public int NotificationId { get; set; }

    public Guid NotificationGuid { get; set; }

    public int UserId { get; set; }

    public string NotificationTitle { get; set; } = null!;

    public string? NotificationDescription { get; set; }

    public bool IsNotificationRead { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual UsrUser User { get; set; } = null!;
}
