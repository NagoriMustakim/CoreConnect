using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class NmsNomination
{
    public int NominationId { get; set; }

    public Guid NominationGuid { get; set; }

    public int NominatorId { get; set; }

    public int NomineeId { get; set; }

    public int InternalProgramId { get; set; }

    public int? InternalProgramCategoryId { get; set; }

    public int CycleIteration { get; set; }

    public string JustificationComment { get; set; } = null!;

    public int NominationStatus { get; set; }

    public string? NominationRejectionReason { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual MstInternalProgram InternalProgram { get; set; } = null!;

    public virtual MstInternalProgramCategory? InternalProgramCategory { get; set; }

    public virtual ICollection<NmsAttachment> NmsAttachments { get; set; } = new List<NmsAttachment>();

    public virtual UsrUser Nominator { get; set; } = null!;

    public virtual UsrUser Nominee { get; set; } = null!;
}
