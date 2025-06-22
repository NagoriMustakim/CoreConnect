using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstSkill
{
    public int SkillId { get; set; }

    public Guid SkillGuid { get; set; }

    public string SkillTitle { get; set; } = null!;

    public string? SkillDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrSkill> UsrSkills { get; set; } = new List<UsrSkill>();

    public virtual ICollection<UsrCertification> Certifications { get; set; } = new List<UsrCertification>();

    public virtual ICollection<UsrProject> UserProjects { get; set; } = new List<UsrProject>();
}
