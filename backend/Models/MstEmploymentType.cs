using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstEmploymentType
{
    public int EmploymentTypeId { get; set; }

    public Guid EmploymentTypeGuid { get; set; }

    public string EmploymentTypeTitle { get; set; } = null!;

    public string? EmploymentTypeDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrExperience> UsrExperiences { get; set; } = new List<UsrExperience>();
}
