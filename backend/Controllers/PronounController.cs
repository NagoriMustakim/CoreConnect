using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.Pronoun;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(AdminConstant.API_PRONOUNS)]
    [ApiController]
    public class PronounController : ControllerBase
    {
        private readonly IPronounRepository _repositoryPronoun;

        public PronounController(IPronounRepository repositoryPronoun)
        {
            _repositoryPronoun = repositoryPronoun;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPronoun()
        {
            try
            {
                var result = await _repositoryPronoun.GetPronounsList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_PRONOUN)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePronoun([FromBody] PronounCreateEditDTO dtoPronounCreate)
        {
            try
            {
                if (await _repositoryPronoun.IsPronounExitsAsync(dtoPronounCreate.Pronoun, null))
                {
                    return Conflict();
                }

                var result = await _repositoryPronoun.CreatePronounAsync(dtoPronounCreate);
                if (!result)
                    return StatusCode(500);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_PRONOUN)]
        [HttpPut(AdminConstant.PRONOUNID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditPronoun(Guid pronounId, [FromBody] PronounCreateEditDTO dtoPronounCreate)
        {
            try
            {
                if (await _repositoryPronoun.IsPronounExitsAsync(dtoPronounCreate.Pronoun, pronounId))
                {
                    return Conflict();
                }
                var result = await _repositoryPronoun.EditPronounAsync(pronounId, dtoPronounCreate);
                if (!result)
                    return StatusCode(500);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_PRONOUN)]
        [HttpGet(AdminConstant.PRONOUNID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPronounById(Guid pronounId)
        {
            try
            {
                var result = await _repositoryPronoun.GetPronounByIdAsync(pronounId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_PRONOUN)]
        [HttpDelete(AdminConstant.PRONOUNID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePronoun(Guid pronounId)
        {
            try
            {
                var result = await _repositoryPronoun.DeletePronounAsync(pronounId);
                if (!result) return StatusCode(500);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
