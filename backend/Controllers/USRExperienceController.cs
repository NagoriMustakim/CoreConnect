using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.User.Experience;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_EXPRIENCE)]
    [ApiController]
    public class USRExperienceController : ControllerBase
    {
        private readonly IUSRExperienceRepository _repositoryUSRExperience;

        public USRExperienceController(IUSRExperienceRepository repositoryUSRExperience)
        {
            _repositoryUSRExperience = repositoryUSRExperience;
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.CREATE_USER_EXPERIENCE)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddExperience([FromBody] UsrExperienceDTO dtoUsrExperience)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _repositoryUSRExperience.AddExperienceAsync(userId, dtoUsrExperience);

                if (result == null)
                    return BadRequest();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Policy = PolicyStrings.VIEW_USER_EXPERIENCE)]
        [HttpGet(UserConstant.GET_ALL_USER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrExperienceViewDTO>> GetAllExperienceDetails(string userId)
        {
            try
            {
                if (userId == null)
                {
                    return BadRequest();
                }
                var experiences = await _repositoryUSRExperience.GetAllExperienceDetailsAsync(userId);
                return Ok(experiences);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_EXPERIENCE)]
        [HttpGet(UserConstant.USER_EXPRIENCEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrExperienceViewDTO>> GetExperienceById(Guid experienceId)
        {
            try
            {
                if (experienceId == Guid.Empty)
                {
                    return BadRequest();
                }
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var experienceDetail = await _repositoryUSRExperience.GetExperienceByIdAsync(userId, experienceId);
                return Ok(experienceDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_EXPERIENCE)]
        [HttpPut(UserConstant.USER_EXPRIENCEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateExperience(Guid experienceId, [FromBody] UsrExperienceModifyDTO dtoUsrExperienceModify)
        {
            try
            {
                if (experienceId == Guid.Empty)
                {
                    return BadRequest();
                }
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _repositoryUSRExperience.UpdateExperienceAsync(userId, experienceId, dtoUsrExperienceModify);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.DELETE_USER_EXPERIENCE)]
        [HttpDelete(UserConstant.USER_EXPRIENCEID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteExperience(Guid experienceId)
        {
            try
            {
                if (experienceId == Guid.Empty)
                {
                    return BadRequest();
                }
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _repositoryUSRExperience.DeleteExperienceAsync(userId, experienceId);

                if (!result)
                {
                    return StatusCode(500);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //Admin Endpoints
        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_USER_EXPERIENCE)]
        [HttpPost("admin/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddExperienceAdmin(string userId, [FromBody] UsrExperienceDTO dtoUsrExperience)
        {
            try
            {
                var result = await _repositoryUSRExperience.AddExperienceAsync(userId, dtoUsrExperience);

                if (result == null)
                    return StatusCode(500);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.VIEW_USER_EXPERIENCE)]
        [HttpGet(AdminConstant.ADMIN_USERID_EXPRIENCEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrExperienceViewDTO>> GetExperienceByIdAdmin(string userId, Guid experienceId)
        {
            try
            {
                if (experienceId == Guid.Empty)
                {
                    return BadRequest();
                }
                var experienceDetail = await _repositoryUSRExperience.GetExperienceByIdAsync(userId, experienceId);
                if (experienceDetail == null)
                {
                    return NotFound();
                }
                return Ok(experienceDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_EXPERIENCE)]
        [HttpPut(AdminConstant.ADMIN_USERID_EXPRIENCEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateExperienceAdmin(string userId, Guid experienceId, [FromBody] UsrExperienceModifyDTO dtoUsrExperienceModify)
        {
            try
            {
                if (experienceId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSRExperience.UpdateExperienceAsync(userId, experienceId, dtoUsrExperienceModify);

                if (result == null)
                {
                    return StatusCode(500);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_USER_EXPERIENCE)]
        [HttpDelete(AdminConstant.ADMIN_USERID_EXPRIENCEID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteExperienceAdmin(string userId, Guid experienceId)
        {
            try
            {
                if (experienceId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSRExperience.DeleteExperienceAsync(userId, experienceId);

                if (!result)
                {
                    return StatusCode(500);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
