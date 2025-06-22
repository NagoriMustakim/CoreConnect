using AutoMapper;
using LinkwayAPI.DTOs.Componies;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace LinkwayAPI.Repository
{
    public class ComponiesRepository : ICompaniesRepository
    {
        private readonly LinkwayDbContext _dbContextLinkway;
        private readonly IMapper _mapper;
        public ComponiesRepository(LinkwayDbContext dbContextLinkway, IMapper mapper)
        {
            _dbContextLinkway = dbContextLinkway;
            _mapper = mapper;
        }

        public async Task<bool> DoesCompanyExists(string componyName, Guid? componyGuid)
        {
            try
            {
                var existingCompony = await _dbContextLinkway.MstCompanies.SingleOrDefaultAsync(compony => compony.CompanyName.ToLower() == componyName.ToLower());
                if (existingCompony == null) return false;
                if (existingCompony.CompanyGuid == componyGuid) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<(IEnumerable<CompaniesViewDTO> result, int totalCount)> GetAllCompaniesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalCount = await _dbContextLinkway.MstCompanies.CountAsync();
                if (pageNumber > 0 && pageSize > 0)
                {
                    var componies = await _dbContextLinkway.MstCompanies.OrderByDescending(compony => compony.CreationDate).Skip((pageNumber - 1) * pageNumber).Take(pageSize).ToListAsync();
                    return (_mapper.Map<IEnumerable<CompaniesViewDTO>>(componies), totalCount);
                }
                else
                {
                    var componies = await _dbContextLinkway.MstCompanies.OrderByDescending(compony => compony.CreationDate).ToListAsync();
                    return (_mapper.Map<IEnumerable<CompaniesViewDTO>>(componies), totalCount);
                }
            }
            catch
            {
                return (null, 0);
            }
        }

        public async Task<CompaniesViewDTO> AddComponyAsync(CompanyAddDTO dtoComponyAdd)
        {
            try
            {
                var mappedCompny = _mapper.Map<MstCompany>(dtoComponyAdd);
                await _dbContextLinkway.AddAsync(mappedCompny);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0)
                    return _mapper.Map<CompaniesViewDTO>(mappedCompny);
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<CompaniesViewDTO> UpdateComponyAsync(CompanyEditDTO dtoCompanyEdit)
        {
            try
            {
                var exisitngCompony = await _dbContextLinkway.MstCompanies.SingleOrDefaultAsync(compony => compony.CompanyGuid == dtoCompanyEdit.CompanyGuid);
                if (exisitngCompony != null)
                {
                    var updatedCompany = _mapper.Map(dtoCompanyEdit, exisitngCompony);
                    var result = await _dbContextLinkway.SaveChangesAsync();
                    if (result > 0)
                        return _mapper.Map<CompaniesViewDTO>(updatedCompany);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> DeleteComponyAsync(Guid componyGuid)
        {
            try
            {
                var exisitngCompony = await _dbContextLinkway.MstCompanies.SingleOrDefaultAsync(compony => compony.CompanyGuid == componyGuid);
                _dbContextLinkway.MstCompanies.Remove(exisitngCompony);
                var result = await _dbContextLinkway.SaveChangesAsync();
                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
