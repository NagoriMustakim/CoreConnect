using LinkwayAPI.DTOs.Search;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface ISearchRepository
    {
        Task<(List<SearchResultDTO> list, int Count)> SearchEmployeeAsync(string? keywords, int? currentCount, SearchFilterDTO dtoSearchFilter);
    }
}
