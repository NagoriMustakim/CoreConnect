using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrCertification
{
    public int UserCertificationId { get; set; }

    public Guid CertificationGuid { get; set; }

    public int UserId { get; set; }

    public int CertificationId { get; set; }

    public int CertificateIssuingCompanyId { get; set; }

    public DateTime? CertificationIssueDate { get; set; }

    public DateTime? CertificationExpirationDate { get; set; }

    public string? CertificationCredentialId { get; set; }

    public string? CertificationCredentialUrl { get; set; }

    public string? CertificationPhotoName { get; set; }

    public string? CertificationDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual MstCompany CertificateIssuingCompany { get; set; } = null!;

    public virtual MstCertification Certification { get; set; } = null!;

    public virtual UsrUser User { get; set; } = null!;

    public virtual ICollection<MstSkill> Skills { get; set; } = new List<MstSkill>();
}
