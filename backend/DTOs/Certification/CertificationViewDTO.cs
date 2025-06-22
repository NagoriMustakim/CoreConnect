namespace LinkwayAPI.DTOs.Certification
{
    public class CertificationViewDTO
    {
        public Guid CertificationGuid { get; set; }

        public string CertificationTitle { get; set; } = null!;

        public string? CertificationDescription { get; set; }
    }
}
