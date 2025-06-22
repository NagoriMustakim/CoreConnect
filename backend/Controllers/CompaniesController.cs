using LinkwayAPI.DTOs.Componies;
using LinkwayAPI.DTOs.Designation;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesRepository _repositoryCompanies;
        public CompaniesController(ICompaniesRepository repositoryCompanies)
        {
            _repositoryCompanies = repositoryCompanies;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllComponies([FromQuery] int pageNumber , [FromQuery] int pageSize )
        {
            try
            {
                var (result, totalCount) = await _repositoryCompanies.GetAllCompaniesAsync(pageNumber, pageSize);
                return Ok(new { list = result, count = totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCompony([FromBody] CompanyAddDTO dtoCompony)
        {
            try
            {
                if (await _repositoryCompanies.DoesCompanyExists(dtoCompony.CompanyName, null))
                    return Conflict();

                var result = await _repositoryCompanies.AddComponyAsync(dtoCompony);
                if (result == null)
                    return StatusCode(500);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DesignationViewDTO>> UpdateCompany([FromBody] CompanyEditDTO dtoCompony)
        {
            if (await _repositoryCompanies.DoesCompanyExists(dtoCompony.CompanyName, dtoCompony.CompanyGuid))
                return Conflict();

            var result = await _repositoryCompanies.UpdateComponyAsync(dtoCompony);
            if (result == null)
                return StatusCode(500);

            return Ok(result);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpDelete("{componyGuid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCompony(Guid componyGuid)
        {
            var result = await _repositoryCompanies.DeleteComponyAsync(componyGuid);

            if (result == false)
                return StatusCode(500);

            return NoContent();
        }
    }
}
