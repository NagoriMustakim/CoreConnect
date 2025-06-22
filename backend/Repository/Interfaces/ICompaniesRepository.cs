
using LinkwayAPI.DTOs.Componies;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface ICompaniesRepository
    {
        Task<(IEnumerable<CompaniesViewDTO> result, int totalCount)> GetAllCompaniesAsync(int pageNumber, int pageSize);
        Task<bool> DoesCompanyExists(string componyName, Guid? componyGuid);
        Task<CompaniesViewDTO> AddComponyAsync(CompanyAddDTO dtoComponyAdd);
        Task<CompaniesViewDTO> UpdateComponyAsync(CompanyEditDTO dtoComponyEdit);
        Task<bool> DeleteComponyAsync(Guid componyId);
    }
}
