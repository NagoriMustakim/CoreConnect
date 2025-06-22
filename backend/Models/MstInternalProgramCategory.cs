using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstInternalProgramCategory
{
    public int InternalProgramCategoryId { get; set; }

    public Guid InternalProgramCategoryGuid { get; set; }

    public int InternalProgramId { get; set; }

    public string InternalProgramCategory { get; set; } = null!;

    public string InternalProgramCategoryDescription { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual MstInternalProgram InternalProgram { get; set; } = null!;

    public virtual ICollection<NmsNomination> NmsNominations { get; set; } = new List<NmsNomination>();
}
