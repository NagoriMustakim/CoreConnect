using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstProficiency
{
    public int ProficiencyId { get; set; }

    public Guid ProficiencyGuid { get; set; }

    public string ProficiencyTitle { get; set; } = null!;

    public string? ProficiencyDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrLanguage> UsrLanguages { get; set; } = new List<UsrLanguage>();
}
