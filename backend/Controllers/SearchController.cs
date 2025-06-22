using LinkwayAPI.Constants.API;
using LinkwayAPI.DTOs.Search;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.API_SEARCH)]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchRepository _repositorySearch;

        public SearchController(ISearchRepository repositorySearch)
        {
            _repositorySearch = repositorySearch;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search(string? keywords, [FromQuery] SearchFilterDTO dtoSearchFilter, [FromQuery] int? currentCount)
        {
            try
            {
                var (result, totalCount) = await _repositorySearch.SearchEmployeeAsync(keywords, currentCount, dtoSearchFilter);
                return Ok(new { list = result, count = totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
