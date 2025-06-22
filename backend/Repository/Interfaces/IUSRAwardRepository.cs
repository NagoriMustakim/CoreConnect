using LinkwayAPI.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IUSRAwardRepository
    {
        Task<IEnumerable<UsrAwardDTO>> GetAllAwardsAsync(string userId);
        Task<ActionResult<UsrAwardDTO>> AddAwardAsync(string userId, UsrAwardDTO dtoUsrAward);
        Task<bool> UpdateAwardAsync(string userId, Guid awardid, UsrAwardDTO dtousraward);
    }
}
