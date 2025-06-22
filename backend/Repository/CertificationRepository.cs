using AutoMapper;
using LinkwayAPI.DTOs.Certification;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class CertificationRepository : ICertificationRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public CertificationRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<CertificationViewDTO> List, int Count)> GetAllCertificationsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalCertifications = await _dbContextLinkway.MstCertifications.CountAsync();

                if (pageNumber > 0 && pageSize > 0)
                {
                    var certifications = await _dbContextLinkway.MstCertifications.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                    return (_mapper.Map<IEnumerable<CertificationViewDTO>>(certifications), totalCertifications);
                }
                else
                {
                    var certifications = await _dbContextLinkway.MstCertifications.ToListAsync();
                    return (_mapper.Map<IEnumerable<CertificationViewDTO>>(certifications), totalCertifications);
                }
            }
            catch
            {
                return (null, 0);
            }
        }

        public async Task<bool> CreateCertificationAsync(CertificationAddDTO dtoCertificationAdd)
        {
            try
            {
                var certification = _mapper.Map<MstCertification>(dtoCertificationAdd);

                _dbContextLinkway.MstCertifications.Add(certification);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCertificationAsync(CertificationEditDTO dtoCertificationEdit)
        {
            try
            {
                var certification = await _dbContextLinkway.MstCertifications.SingleOrDefaultAsync(c => c.CertificationGuid == dtoCertificationEdit.CertificationGuid);

                _mapper.Map(dtoCertificationEdit, certification);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCertificationAsync(Guid certificationGuid)
        {
            try
            {
                var certification = await _dbContextLinkway.MstCertifications.SingleOrDefaultAsync(c => c.CertificationGuid == certificationGuid);

                _dbContextLinkway.MstCertifications.Remove(certification);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsCertificationExists(string certificationTitle, Guid? certificationGuid)
        {
            var existingCertification = await _dbContextLinkway.MstCertifications.SingleOrDefaultAsync(s => s.CertificationTitle.ToLower() == certificationTitle.ToLower());

            if (existingCertification == null) return false;

            if (existingCertification.CertificationGuid == certificationGuid) return false;

            return true;
        }
    }
}
