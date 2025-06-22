using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstDepartment
{
    public int DepartmentId { get; set; }

    public Guid DepartmentGuid { get; set; }

    public string Department { get; set; } = null!;

    public string? DepartmentDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<MstDesignation> MstDesignations { get; set; } = new List<MstDesignation>();
}
