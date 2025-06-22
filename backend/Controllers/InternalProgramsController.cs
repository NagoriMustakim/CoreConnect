using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.InternalPrograms;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(AdminConstant.API_INTERNALPROGRAM)]
    [ApiController]
    public class InternalProgramsController : ControllerBase
    {
        private readonly IInternalProgramsRepository _repositoryInternalPrograms;
        public InternalProgramsController(IInternalProgramsRepository repositoryInternalPrograms)
        {
            _repositoryInternalPrograms = repositoryInternalPrograms;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllInternalPrograms()
        {
            try
            {
                var result = await _repositoryInternalPrograms.GetAllInternalProgramsAsync();
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_INTERNAL_PROGRAM)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateInternalProgram([FromBody] InternalProgramCreateDTO dtoInternalProgramCreate)
        {
            try
            {
                var isExists = await _repositoryInternalPrograms.IsInternalProgamExistsAsync(dtoInternalProgramCreate.InternalProgramName, null);
                if (isExists)
                    return Conflict();

                var result = await _repositoryInternalPrograms.AddInternalProgramAsync(dtoInternalProgramCreate);
                if (!result) return BadRequest();
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(AdminConstant.INTERNALPROGRAMID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetInternalProgramById(Guid internalProgramId)
        {
            try
            {
                var result = await _repositoryInternalPrograms.GetInternalProgramByIdAsync(internalProgramId);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_INTERNAL_PROGRAM)]
        [HttpPut(AdminConstant.INTERNALPROGRAMID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditInternalProgram(Guid internalProgramId, InternalProgramModifyDTO dtoInternalProgramModify)
        {
            try
            {

                var isExists = await _repositoryInternalPrograms.IsInternalProgamExistsAsync(dtoInternalProgramModify.InternalProgramName, internalProgramId);
                if (isExists)
                    return Conflict();

                var result = await _repositoryInternalPrograms.EditInternalProgramAsync(internalProgramId, dtoInternalProgramModify);
                if (result) return Ok();
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_INTERNAL_PROGRAM)]
        [HttpDelete(AdminConstant.INTERNALPROGRAMID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteInternalProgram(Guid internalProgramId)
        {
            try
            {
                var result = await _repositoryInternalPrograms.DeleteInrternalProgramAsync(internalProgramId);
                if (result) return NoContent();
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
