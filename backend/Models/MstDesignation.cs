using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class MstDesignation
{
    public int DesignationId { get; set; }

    public Guid DesignationGuid { get; set; }

    public int DepartmentId { get; set; }

    public string Designation { get; set; } = null!;

    public int DesignationLevel { get; set; }

    public string? DesignationDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrUser> UsrUsers { get; set; } = new List<UsrUser>();

    public virtual ICollection<CndGiftform> CndGiftformCurrentDesignations { get; set; } = new List<CndGiftform>();

    public virtual ICollection<CndGiftform> CndGiftformDesiredDesignations { get; set; } = new List<CndGiftform>();

    public virtual MstDepartment Department { get; set; } = null!;

    public virtual ICollection<UsrExperience> UsrExperiences { get; set; } = new List<UsrExperience>();
}
