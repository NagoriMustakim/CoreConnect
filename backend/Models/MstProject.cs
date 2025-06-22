using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstProject
{
    public int ProjectId { get; set; }

    public Guid ProjectGuid { get; set; }

    public string ProjectTitle { get; set; } = null!;

    public string? ProjectDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrProject> UsrProjects { get; set; } = new List<UsrProject>();
}
