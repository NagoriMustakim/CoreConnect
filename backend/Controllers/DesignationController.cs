using LinkwayAPI.DTOs.Designation;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationRepository _repositoryDesignation;
        public DesignationController(IDesignationRepository repositoryDesignation)
        {
            _repositoryDesignation = repositoryDesignation;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDesignations([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var (result, totalCount) = await _repositoryDesignation.GetAllDesignationsAsync(pageNumber, pageSize);
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
        public async Task<IActionResult> CreateDesignation([FromBody] DesignationAddDTO dtoDesignation)
        {
            try
            {
                if (await _repositoryDesignation.IsDesignationExists(dtoDesignation.DepartmentGuid, dtoDesignation.Designation, null))
                    return Conflict();

                var result = await _repositoryDesignation.CreateDesignationAsync(dtoDesignation);
                if (result == null)
                    return StatusCode(500);

                return Created();
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
        public async Task<ActionResult<DesignationEditDTO>> UpdateDesignation([FromBody] DesignationEditDTO dtoDesignation)
        {
            if (await _repositoryDesignation.IsDesignationExists(dtoDesignation.DepartmentGuid, dtoDesignation.Designation, dtoDesignation.DesignationGuid))
                return Conflict();

            var result = await _repositoryDesignation.UpdateDesignationAsync(dtoDesignation);
            if (result == null)
                return StatusCode(500);

            return Ok(result);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpDelete("{designationGuid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDesignation(Guid designationGuid)
        {
            var result = await _repositoryDesignation.DeleteDesignationAsync(designationGuid);

            if (result == false)
                return StatusCode(500);

            return NoContent();
        }
    }
}
