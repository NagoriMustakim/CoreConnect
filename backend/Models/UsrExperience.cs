using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrExperience
{
    public int ExperienceId { get; set; }

    public Guid ExperienceGuid { get; set; }

    public int UserId { get; set; }

    public int? EmploymentTypeId { get; set; }

    public int? LocationTypeId { get; set; }

    public int DesignationId { get; set; }

    public int CompanyId { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public DateTime ExperienceStartDate { get; set; }

    public DateTime? ExperienceEndDate { get; set; }

    public bool IsExperienceActive { get; set; }

    public string? ExperienceDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual MstCompany Company { get; set; } = null!;

    public virtual MstDesignation Designation { get; set; } = null!;

    public virtual MstEmploymentType? EmploymentType { get; set; }

    public virtual MstLocationType? LocationType { get; set; }

    public virtual UsrUser User { get; set; } = null!;
    public int CalculateExperienceYears()
    {
        DateTime endDate = ExperienceEndDate ?? DateTime.UtcNow;
        TimeSpan timeSpan = endDate - ExperienceStartDate;
        int totalDays = (int)timeSpan.TotalDays;
        int totalYears = totalDays / 365;
        return totalYears;
    }
}
