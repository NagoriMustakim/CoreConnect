using LinkwayAPI.DTOs.User.Kra;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUSRKraRepository
    {
        Task<UsrKraViewDTO> AddKraAsync(string? userId, UsrKraDTO dtoUsrKra);
        Task<UsrKraViewDTO> GetKraByIdAsync(string userId, Guid KraId);
        Task<UsrKraViewDTO> UpdateKraAsync(string userId, Guid KraId, UsrKraModifyDTO dtoUsrKraModify);
        Task<bool> DeleteKraAsync(string userId, Guid KraId);
        Task<IEnumerable<UsrKraViewDTO>> GetAllKrasAsync(string userId);
    }
}
