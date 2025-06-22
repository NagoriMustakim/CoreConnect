using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstCertification
{
    public int CertificationId { get; set; }

    public Guid CertificationGuid { get; set; }

    public string CertificationTitle { get; set; } = null!;

    public string? CertificationDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrCertification> UsrCertifications { get; set; } = new List<UsrCertification>();
}
