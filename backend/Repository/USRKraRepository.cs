using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.User.Kra;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Repository
{
    public class USRKraRepository : IUSRKraRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        private readonly UserManager<UsrUser> _managerUser;

        public USRKraRepository(LinkwayDbContext dbContextLinkway, IMapper mapper, UserManager<UsrUser> managerUser)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
            _managerUser = managerUser;
        }

        public async Task<UsrKraViewDTO> AddKraAsync(string userId, UsrKraDTO dtoUsrKRA)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return null;

                var kra = _mapper.Map<UsrKra>(dtoUsrKRA);
                kra.UserId = user.EmployeeCode;

                await _dbContextLinkway.UsrKras.AddAsync(kra);

                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0)
                    return _mapper.Map<UsrKraViewDTO>(kra); ;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<UsrKraViewDTO>> GetAllKrasAsync(string userId)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                var kras = await _dbContextLinkway.UsrKras.Where(c => c.UserId == user.EmployeeCode).ToListAsync();

                var krasMapped = _mapper.Map<IEnumerable<UsrKraViewDTO>>(kras);

                return krasMapped;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UsrKraViewDTO> GetKraByIdAsync(string userId, Guid KraId)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);
                var kraDetail = await _dbContextLinkway.UsrKras.SingleOrDefaultAsync(c => c.Kraguid == KraId && c.UserId == user.EmployeeCode);

                var kraDetailMapped = _mapper.Map<UsrKraViewDTO>(kraDetail);

                return kraDetailMapped;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UsrKraViewDTO> UpdateKraAsync(string userId, Guid KraId, UsrKraModifyDTO dtoUsrKraModify)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return null;

                var KraDetail = await _dbContextLinkway.UsrKras.SingleOrDefaultAsync(d => d.Kraguid == KraId && d.UserId == user.EmployeeCode);

                var KraDetailMapped = _mapper.Map(dtoUsrKraModify, KraDetail);

                _dbContextLinkway.UsrKras.Update(KraDetailMapped);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return _mapper.Map<UsrKraViewDTO>(KraDetailMapped); ;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> DeleteKraAsync(string userId, Guid KraId)
        {
            try
            {
                var user = await _dbContextLinkway.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return false;

                var KraDetail = await _dbContextLinkway.UsrKras.SingleOrDefaultAsync(d => d.Kraguid == KraId && d.UserId == user.EmployeeCode);

                _dbContextLinkway.UsrKras.Remove(KraDetail);

                var result = await _dbContextLinkway.SaveChangesAsync();

                if (result > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}