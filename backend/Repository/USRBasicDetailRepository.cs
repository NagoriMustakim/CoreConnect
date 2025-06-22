using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.User.BasicDetail;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class USRBasicDetailRepository : IUSRBasicDetailRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly IFileRepository _repositoryFile;
        private readonly UserManager<UsrUser> _managerUser;

        public USRBasicDetailRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, IFileRepository repositoryFile, UserManager<UsrUser> managerUser)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _repositoryFile = repositoryFile;
            _managerUser = managerUser;
        }

        public async Task<UsrBasicDetailViewDTO> GetAllBasicDetailsByIdAsync(string userId)
        {
            try
            {
                var user = await _managerUser.FindByIdAsync(userId);
                var role = await _managerUser.GetRolesAsync(user);
                var basicDetails = await _dbContextLinkway.Users.Include(u => u.BusinessUnit).Include(u => u.Pronoun).Include(u => u.Designation).SingleOrDefaultAsync(u => u.Id == userId);

                var mappedBasicDetails = _mapper.Map<UsrBasicDetailViewDTO>(basicDetails);
                mappedBasicDetails.Role = role;
                return mappedBasicDetails;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UsrBasicDetailViewDTO> UpdateBasicDetailAsync(string userId, UsrBasicDetailModifyDTO usrBasicDetailModifyDTO)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return null;

                if (usrBasicDetailModifyDTO.JoiningDate != null)
                    user.CreationDate = (DateTime)usrBasicDetailModifyDTO.JoiningDate;

                var basicDetailMapped = _mapper.Map(usrBasicDetailModifyDTO, user);

                var businessUnit = await _dbContextLinkway.MstBusinessUnits.SingleOrDefaultAsync(b => b.BusinessUnitGuid == usrBasicDetailModifyDTO.BusinessUnitGuid);
                if (businessUnit != null)
                    basicDetailMapped.BusinessUnitId = businessUnit.BusinessUnitId;

                var pronoun = await _dbContextLinkway.MstPronouns.SingleOrDefaultAsync(p => p.PronounGuid == usrBasicDetailModifyDTO.PronounGuid);
                if (pronoun != null)
                    basicDetailMapped.PronounId = pronoun.PronounId;
                else
                    basicDetailMapped.PronounId = null;

                var designation = await _dbContextLinkway.MstDesignations.SingleOrDefaultAsync(d => d.DesignationGuid == usrBasicDetailModifyDTO.DesignationGuid);
                if (designation != null)
                    basicDetailMapped.DesignationId = designation.DesignationId;
                else
                    basicDetailMapped.DesignationId = null;

                _dbContextLinkway.Users.Update(basicDetailMapped);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrBasicDetailViewDTO>(basicDetailMapped); ;

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAvatarAsync(string userId, IFormFile avatarPhoto)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (avatarPhoto != null)
                {
                    var fileName = _repositoryFile.SaveImage(avatarPhoto);

                    if (fileName.Item1 == 1)
                        user.ProfilePhotoName = fileName.Item2;
                }

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

        public async Task<bool> deleteAvatarAsync(string userId)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                var fileName = _repositoryFile.Deleteimage(user.ProfilePhotoName);

                user.ProfilePhotoName = null;
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
    }
}
