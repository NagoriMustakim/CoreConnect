using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.User.Certificate;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class USRCertificateRepository : IUSRCertificateRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly IFileRepository _repositoryFile;

        public USRCertificateRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, UserManager<UsrUser> managerUser, IFileRepository repositoryFile)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _repositoryFile = repositoryFile;
        }

        public async Task<UsrCertificateViewDTO> AddCertificateAsync(string userId, UsrCertificateAddDTO dtoUsrCertificate, IFormFile? certificationPhoto)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return null;

                var certificateDetail = _mapper.Map<UsrCertification>(dtoUsrCertificate);
                certificateDetail.UserId = user.EmployeeCode;

                //var skill = await _dbContextLinkway.UsrSkills.SingleOrDefaultAsync(s => s.UserSkillGuid == dtoUsrCertificate.SkillGuid);
                //if (skill != null)
                //    certificateDetail.Skills = skill.SkillId;

                var company = await _dbContextLinkway.MstCompanies.SingleOrDefaultAsync(c => c.CompanyGuid == dtoUsrCertificate.CompanyGuid);

                if (company != null)
                    certificateDetail.CertificateIssuingCompanyId = company.CompanyId;

                var certification = await _dbContextLinkway.MstCertifications.SingleOrDefaultAsync(c => c.CertificationGuid == dtoUsrCertificate.CertificationGuid);

                if (certification != null)
                    certificateDetail.CertificationId = certification.CertificationId;

                if (certificationPhoto != null)
                {
                    var fileName = _repositoryFile.SaveImage(certificationPhoto);

                    if (fileName.Item1 == 1)
                        certificateDetail.CertificationPhotoName = fileName.Item2;
                }

                await _dbContextLinkway.UsrCertifications.AddAsync(certificateDetail);

                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0)
                    return _mapper.Map<UsrCertificateViewDTO>(certificateDetail);

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<UsrCertificateViewDTO>> GetAllCertificatesAsync(string userId)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                //var certificates = await _dbContextLinkway.UsrCertifications.Include(c => c.Certification).Include(c => c.CertificateIssuingCompany).Include(c => c.Skill).Where(c => c.UserId == user.EmployeeCode).OrderByDescending(certificate => certificate.CreationDate).ToListAsync();
                var certificates = await _dbContextLinkway.UsrCertifications.Include(c => c.Certification).Include(c => c.CertificateIssuingCompany).Where(c => c.UserId == user.EmployeeCode).OrderByDescending(certificate => certificate.CreationDate).ToListAsync();

                var certificatesMapped = _mapper.Map<IEnumerable<UsrCertificateViewDTO>>(certificates);

                return certificatesMapped;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UsrCertificateViewDTO> GetCertificateByIdAsync(string userId, Guid certificateGuid)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);
                //var certificateDetail = await _dbContextLinkway.UsrCertifications.Include(c => c.Certification).Include(c => c.CertificateIssuingCompany).Include(c => c.Skill).SingleOrDefaultAsync(c => c.CertificationGuid == certificateGuid && c.UserId == user.EmployeeCode);
                var certificateDetail = await _dbContextLinkway.UsrCertifications.Include(c => c.Certification).Include(c => c.CertificateIssuingCompany).SingleOrDefaultAsync(c => c.CertificationGuid == certificateGuid && c.UserId == user.EmployeeCode);

                var certificateDetailmapped = _mapper.Map<UsrCertificateViewDTO>(certificateDetail);

                return certificateDetailmapped;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UsrCertificateViewDTO> UpdateCertificateAsync(string userId, Guid certificateGuid, UsrCertificateModifyDTO dtoUsrCertificateModify, IFormFile? certificationPhoto)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return null;

                var certificateDetail = await _dbContextLinkway.UsrCertifications.SingleOrDefaultAsync(d => d.CertificationGuid == certificateGuid && d.UserId == user.EmployeeCode);
                var certificateImagePhoto = certificateDetail.CertificationPhotoName;

                var certificateDetailmapped = _mapper.Map(dtoUsrCertificateModify, certificateDetail);
                var certificate = await _dbContextLinkway.MstCertifications.SingleOrDefaultAsync(c => c.CertificationGuid == dtoUsrCertificateModify.CertificationGuid);

                if (certificate != null)
                    certificateDetailmapped.CertificationId = certificate.CertificationId;

                var company = await _dbContextLinkway.MstCompanies.SingleOrDefaultAsync(c => c.CompanyGuid == dtoUsrCertificateModify.CompanyGuid);

                if (company != null)
                    certificateDetailmapped.CertificateIssuingCompanyId = company.CompanyId;

                //var skill = await _dbContextLinkway.UsrSkills.SingleOrDefaultAsync(s => s.UserSkillGuid == dtoUsrCertificateModify.SkillGuid);
                //if (skill != null)
                //    certificateDetailmapped.SkillId = skill.SkillId;
                //else
                //    certificateDetailmapped.SkillId = null;

                if (certificationPhoto != null)
                {
                    var fileName = _repositoryFile.EditImage(certificationPhoto, dtoUsrCertificateModify.CertificationPhotoName);

                    if (fileName.Item1 == 1)
                        certificateDetail.CertificationPhotoName = fileName.Item2;
                }
                else if (certificateImagePhoto != null && dtoUsrCertificateModify.CertificationPhotoName == null)
                {
                    var res = _repositoryFile.Deleteimage(certificateImagePhoto);
                    if (res)
                    {
                        certificateDetail.CertificationPhotoName = null;
                    }

                }
                _dbContextLinkway.UsrCertifications.Update(certificateDetailmapped);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrCertificateViewDTO>(certificateDetail);

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteCertificateAsync(string userId, Guid certificateGuid)
        {
            try
            {
                string currentImageName = null;
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return false;

                var certificateDetail = await _dbContextLinkway.UsrCertifications.SingleOrDefaultAsync(d => d.CertificationGuid == certificateGuid && d.UserId == user.EmployeeCode);

                if (certificateDetail.CertificationPhotoName != null)
                    currentImageName = certificateDetail.CertificationPhotoName;

                _dbContextLinkway.UsrCertifications.Remove(certificateDetail);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                {
                    _repositoryFile.Deleteimage(currentImageName);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
