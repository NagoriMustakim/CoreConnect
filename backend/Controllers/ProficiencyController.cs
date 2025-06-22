using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.Proficiency;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(AdminConstant.API_PROFICIENCY)]
    [ApiController]
    public class ProficiencyController : ControllerBase
    {
        private readonly IProficiencyRepository _repositoryProficiency;

        public ProficiencyController(IProficiencyRepository repositoryProficiency)
        {
            _repositoryProficiency = repositoryProficiency;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProficiencies()
        {
            try
            {
                var result = await _repositoryProficiency.GetAllProficiencyAsync();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_PROFICIENCY)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProficiency([FromBody] ProficiencyCreateDTO dtoProficiencyCreate)
        {
            try
            {
                var isExisting = await _repositoryProficiency.CheckForExisitingDataAsync(dtoProficiencyCreate.ProficiencyTitle, null);
                if(isExisting) {
                    return Conflict();
                }
                var result = await _repositoryProficiency.CreateProficiencyAsync(dtoProficiencyCreate);
                if (!result) { return StatusCode(500); }
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_PROFICIENCY)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditProficiency([FromBody] ProficiencyEditDTO dtoProficiencyEdit)
        {
            try
            {
                var isExisting = await _repositoryProficiency.CheckForExisitingDataAsync(dtoProficiencyEdit.ProficiencyTitle, dtoProficiencyEdit.ProficiencyGuid);
                if (isExisting)
                {
                    return Conflict();
                }
                var result = await _repositoryProficiency.UpdateProficiencyAsync(dtoProficiencyEdit);
                if (!result)
                    return StatusCode(500);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_PROFICIENCY)]
        [HttpDelete(AdminConstant.PROFICIENCYID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProficiency(Guid proficiencyId)
        {
            try
            {
                bool result = await _repositoryProficiency.DeleteProficiencyAsync(proficiencyId);
                if (!result) { return StatusCode(500); }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
