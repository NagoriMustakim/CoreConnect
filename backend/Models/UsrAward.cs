using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrAward
{
    public int AwardId { get; set; }

    public Guid AwardGuid { get; set; }

    public int UserId { get; set; }

    public string AwardTitle { get; set; } = null!;

    public string? AwardIssuer { get; set; }

    public DateTime? AwardIssueDate { get; set; }

    public string? AwardDescription { get; set; }

    public string? AwardPhotoName { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual UsrUser User { get; set; } = null!;
}
