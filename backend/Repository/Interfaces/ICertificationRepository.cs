using LinkwayAPI.DTOs.Certification;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface ICertificationRepository
    {
        Task<(IEnumerable<CertificationViewDTO> List, int Count)> GetAllCertificationsAsync(int pageNumber, int pageSize);

        Task<bool> CreateCertificationAsync(CertificationAddDTO dtoCertificationAdd);

        Task<bool> UpdateCertificationAsync(CertificationEditDTO dtoCertificationEdit);

        Task<bool> DeleteCertificationAsync(Guid certificationGuid);

        Task<bool> IsCertificationExists(string certificationTitle, Guid? certificationGuid);
    }
}
