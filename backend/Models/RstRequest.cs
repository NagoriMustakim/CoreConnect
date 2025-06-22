using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class RstRequest
{
    public int RequestId { get; set; }

    public Guid RequestGuid { get; set; }

    public int RequesterId { get; set; }

    public int RequestType { get; set; }

    public string RequestDescription { get; set; } = null!;

    public int RequestStatus { get; set; }

    public int NoOfMembers { get; set; }

    public DateTime RequestDate { get; set; }

    public string? RequestRejectionReason { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual UsrUser Requester { get; set; } = null!;

    public virtual ICollection<UsrUser> Users { get; set; } = new List<UsrUser>();
}
