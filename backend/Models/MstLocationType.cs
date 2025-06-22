using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstLocationType
{
    public int LocationTypeId { get; set; }

    public Guid LocationTypeGuid { get; set; }

    public string LocationType { get; set; } = null!;

    public string? LocationTypeDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrExperience> UsrExperiences { get; set; } = new List<UsrExperience>();
}
