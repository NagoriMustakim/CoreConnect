using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrPatent
{
    public int PatentId { get; set; }

    public Guid PatentGuid { get; set; }

    public int UserId { get; set; }

    public string PatentTitle { get; set; } = null!;

    public string PatentApplicationNumber { get; set; } = null!;

    public int PatentStatus { get; set; }

    public DateTime PatentIssueDate { get; set; }

    public string? PatentUrl { get; set; }

    public string? PatentDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual UsrUser User { get; set; } = null!;
}
