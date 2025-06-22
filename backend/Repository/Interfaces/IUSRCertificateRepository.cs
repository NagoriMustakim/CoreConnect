using LinkwayAPI.DTOs.User.Certificate;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUSRCertificateRepository
    {
        Task<UsrCertificateViewDTO> AddCertificateAsync(string userId, UsrCertificateAddDTO dtoUsrCertificate, IFormFile? certificationPhoto);
        Task<UsrCertificateViewDTO> GetCertificateByIdAsync(string userId, Guid CertificateId);
        Task<UsrCertificateViewDTO> UpdateCertificateAsync(string userId, Guid certificateGuid, UsrCertificateModifyDTO dtoUsrCertificateModify, IFormFile? certificationPhoto);
        Task<bool> DeleteCertificateAsync(string userId, Guid CertificateId);
        Task<IEnumerable<UsrCertificateViewDTO>> GetAllCertificatesAsync(string userId);
     
    }
}
