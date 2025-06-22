using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrSkill
{
    public int UserSkillId { get; set; }

    public Guid UserSkillGuid { get; set; }

    public int SkillId { get; set; }

    public int UserId { get; set; }

    public int? ProficiencyId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual MstSkill Skill { get; set; } = null!;

    public virtual UsrUser User { get; set; } = null!;
}
