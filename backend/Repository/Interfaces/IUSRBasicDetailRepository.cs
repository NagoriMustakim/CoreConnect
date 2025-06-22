using LinkwayAPI.DTOs.User.BasicDetail;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUSRBasicDetailRepository
    {
        //Task<bool> AddBasicDetailAsync(string userId, UsrBasicDetailModifyDTO dtoUsrBasicDetail);
        Task<UsrBasicDetailViewDTO> GetAllBasicDetailsByIdAsync(string userId);
        Task<UsrBasicDetailViewDTO> UpdateBasicDetailAsync(string userId, UsrBasicDetailModifyDTO usrBasicDetailModifyDTO);
        Task<bool> UpdateAvatarAsync(string userId, IFormFile avatarPhoto);
        Task<bool> deleteAvatarAsync(string userId);
    }
}
