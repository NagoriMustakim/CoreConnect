using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstCause
{
    public int CauseId { get; set; }

    public Guid CauseGuid { get; set; }

    public string Cause { get; set; } = null!;

    public string? CauseDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrVolunteeringExperience> UsrVolunteeringExperiences { get; set; } = new List<UsrVolunteeringExperience>();
}
