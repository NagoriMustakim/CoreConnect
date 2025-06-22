using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrComment
{
    public int CommentId { get; set; }

    public Guid CommentGuid { get; set; }

    public int CommenterId { get; set; }

    public int UserId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual UsrUser Commenter { get; set; } = null!;

    public virtual UsrUser User { get; set; } = null!;
}
