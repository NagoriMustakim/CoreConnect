namespace LinkwayAPI.DTOs.User.Certificate
{
    public class UsrCertificateViewDTO
    {
        public Guid UserCertificationGuid { get; set; }

        public Guid CertificationGuid { get; set; }

        public Guid? SkillGuid { get; set; }

        public string? SkillTitle { get; set; } = null!;

        public string CertificationTitle { get; set; } = null!;

        public Guid CompanyGuid { get; set; }

        public string CompanyName { get; set; } = null!;

        public DateTime? CertificationIssueDate { get; set; }

        public DateTime? CertificationExpirationDate { get; set; }

        public string? CertificationCredentialId { get; set; }

        public string? CertificationCredentialUrl { get; set; }

        public string? CertificationPhotoName { get; set; }

        public string? CertificationDescription { get; set; }
    }
}
