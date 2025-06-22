using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.User;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class USRAwardRepository : IUSRAwardRepository
    {
        private readonly UserManager<UsrUser> _managerUser;
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;

        public USRAwardRepository(UserManager<UsrUser> managerUser, SignInManager<UsrUser> managerSignIn, RoleManager<IdentityRole> managerRole, IConfiguration configuration, LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _managerUser = managerUser;
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsrAwardDTO>> GetAllAwardsAsync(string userId)
        {
            try
            {
                var user = await _managerUser.Users.SingleOrDefaultAsync(u => u.Id == userId);

                var awards = await _dbContextLinkway.UsrAwards
                    .Where(a => a.UserId == user.EmployeeCode)
                    .ToListAsync();


                return _mapper.Map<IEnumerable<UsrAwardDTO>>(awards);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ActionResult<UsrAwardDTO>> AddAwardAsync(string userId, UsrAwardDTO dtoUsrAward)
        {
            try
            {
                var user = await _managerUser.Users.SingleOrDefaultAsync(u => u.Id == userId);

                var award = _mapper.Map<UsrAward>(dtoUsrAward);
                award.UserId = user.EmployeeCode;
                award.AwardGuid = Guid.NewGuid();
                award.CreationDate = DateTime.UtcNow;
                award.ModificationDate = DateTime.UtcNow;

                _dbContextLinkway.UsrAwards.Add(award);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0)
                    return dtoUsrAward;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAwardAsync(string userId, Guid awardGuid, UsrAwardDTO dtoUsrAward)
        {
            var user = await _managerUser.Users.SingleOrDefaultAsync(u => u.Id == userId);

            var award = await _dbContextLinkway.UsrAwards.SingleOrDefaultAsync(a => a.UserId == user.EmployeeCode && a.AwardGuid == awardGuid);

            award.AwardTitle = dtoUsrAward.Title;
            award.AwardIssuer = dtoUsrAward.Issuer;
            award.AwardIssueDate = dtoUsrAward.IssueDate;
            award.AwardDescription = dtoUsrAward.Description;
            award.AwardPhotoName = dtoUsrAward.PhotoPath;
            award.ModificationDate = DateTime.UtcNow;

            _dbContextLinkway.UsrAwards.Update(award);

            var result = await _dbContextLinkway.SaveChangesAsync();

            if (result > 0)
                return true;

            return false;
        }
    }
}
