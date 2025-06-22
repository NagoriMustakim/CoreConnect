using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstInternalProgram
{
    public int InternalProgramId { get; set; }

    public Guid InternalProgramGuid { get; set; }

    public string InternalProgramName { get; set; } = null!;

    public string? InternalProgramDescription { get; set; }

    public int InternalProgramReviewCycle { get; set; }

    public int InternalProgramActiveDays { get; set; }

    public bool IsInternalProgramCategoryExists { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<MstInternalProgramCategory> MstInternalProgramCategories { get; set; } = new List<MstInternalProgramCategory>();

    public virtual ICollection<NmsNomination> NmsNominations { get; set; } = new List<NmsNomination>();
}
