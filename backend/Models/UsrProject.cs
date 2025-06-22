using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrProject
{
    public int UserProjectId { get; set; }

    public Guid UserProjectGuid { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public DateTime ProjectStartDate { get; set; }

    public DateTime? ProjectEndDate { get; set; }

    public bool IsProjectActive { get; set; }

    public string? ProjectDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual MstProject Project { get; set; } = null!;

    public virtual UsrUser User { get; set; } = null!;

    public virtual ICollection<MstSkill> Skills { get; set; } = new List<MstSkill>();
}
