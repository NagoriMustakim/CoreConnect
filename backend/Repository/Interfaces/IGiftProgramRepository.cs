using LinkwayAPI.DTOs.GiftProgram;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IGiftProgramRepository
    {
        Task<GiftProgramDisplayDTO> CreateGiftProgramAsync(GiftProgramDTO giftProgram, string userId);
        Task<bool> DeleteGiftProgramAsync(Guid giftProgramGuid, string userId);
        Task<bool> EditGiftProgramAsync(Guid giftProgramGuid, GiftProgramModifyDTO giftProgram, string userId);
        Task<bool> UpdateStatusGiftProgramAsync(Guid giftProgramId, GiftProgramStatusDTO giftProgram, string userId);
        Task<(IEnumerable<GiftProgramDisplayDTO> List, int TotalCount)> GetAllGiftProgramAsync(List<int> adminStatus, List<int> managerStatus, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<GiftProgramDisplayDTO>> GetAllGiftProgramByIdAsync(string userId);
        Task<GiftProgramDisplayDTO> GetGiftProgramByIdAsync(Guid giftProgramGuid, string userId);
        Task<(bool, string)> CheckGiftProgram(string userId);
    }
}