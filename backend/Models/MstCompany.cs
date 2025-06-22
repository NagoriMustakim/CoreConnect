using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstCompany
{
    public int CompanyId { get; set; }

    public Guid CompanyGuid { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? CompanyDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrCertification> UsrCertifications { get; set; } = new List<UsrCertification>();

    public virtual ICollection<UsrExperience> UsrExperiences { get; set; } = new List<UsrExperience>();
}
