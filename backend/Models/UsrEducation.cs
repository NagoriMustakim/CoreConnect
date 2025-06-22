using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrEducation
{
    public int EducationId { get; set; }

    public Guid EducationGuid { get; set; }

    public int UserId { get; set; }

    public string EducationInstituteName { get; set; } = null!;

    public string? EducationDegree { get; set; }

    public DateTime EducationStartDate { get; set; }

    public DateTime? EducationEndDate { get; set; }

    public bool IsEducationActive { get; set; }

    public string? EducationGrade { get; set; }

    public string? EducationDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual UsrUser User { get; set; } = null!;
}
