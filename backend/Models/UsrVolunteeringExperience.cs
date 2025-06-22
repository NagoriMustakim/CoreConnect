using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrVolunteeringExperience
{
    public int VolunteeringExperienceId { get; set; }

    public Guid VolunteeringExperienceGuid { get; set; }

    public int? CauseId { get; set; }

    public int UserId { get; set; }

    public string VolunteeringExperienceOrganization { get; set; } = null!;

    public string VolunteeringExperienceRole { get; set; } = null!;

    public DateTime VolunteeringExperienceStartDate { get; set; }

    public DateTime? VolunteeringExperienceEndDate { get; set; }

    public bool IsVolunteeringExperienceActive { get; set; }

    public string? VolunteeringExperienceDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual MstCause? Cause { get; set; }

    public virtual UsrUser User { get; set; } = null!;
}
