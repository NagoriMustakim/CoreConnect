using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.Constants.UserResponseMessage;
using LinkwayAPI.DTOs.User.Project;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_PROJECT)]
    [ApiController]
    public class USRProjectController : ControllerBase
    {
        private readonly IUSRProjectRepository _usrProjectRepository;
        public USRProjectController(IUSRProjectRepository USRProjectRepository)
        {
            _usrProjectRepository = USRProjectRepository;
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.CREATE_USER_PROJECT)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProject([FromBody] UsrProjectDTO project)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await _usrProjectRepository.CreateProjectAsync(project, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Policy = PolicyStrings.VIEW_USER_PROJECT)]
        [HttpGet(UserConstant.GET_ALL_USER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProject(string userId)
        {
            try
            {
                var result = await _usrProjectRepository.GetAllProjectAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_PROJECT)]
        [HttpGet(UserConstant.USER_PROJECTID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProjectById(Guid projectId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _usrProjectRepository.GetProjectByIdAsync(projectId, userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_PROJECT)]
        [HttpPut(UserConstant.USER_PROJECTID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditProject(Guid projectId, UsrProjectModifyDTO project)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _usrProjectRepository.EditProjectAsync(projectId, project, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500, UserResponseMessage.ERROR_PROJECT_DATA_NOT_MODIFIED);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.DELETE_USER_PROJECT)]
        [HttpDelete(UserConstant.USER_PROJECTID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProject(Guid projectId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _usrProjectRepository.DeleteProjectAsync(projectId, userId);

                if (result)
                    return NoContent();

                return NotFound(UserResponseMessage.ERROR_PROJECT_NOT_FOUND);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_USER_PROJECT)]
        [HttpPost(AdminConstant.ADMIN_USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProjectAdmin(string userId, [FromBody] UsrProjectDTO project)
        {
            try
            {
                var result = await _usrProjectRepository.CreateProjectAsync(project, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_PROJECT)]
        [HttpGet(AdminConstant.ADMIN_UPDATE_PROJECT)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProjectByIdAdmin(string userId, Guid projectId)
        {
            try
            {
                var result = await _usrProjectRepository.GetProjectByIdAsync(projectId, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_PROJECT)]
        [HttpPut(AdminConstant.ADMIN_UPDATE_PROJECT)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditProjectAdmin(string userId, Guid projectId, UsrProjectModifyDTO project)
        {
            try
            {
                var result = await _usrProjectRepository.EditProjectAsync(projectId, project, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_USER_PROJECT)]
        [HttpDelete(AdminConstant.ADMIN_DELETE_PROJECT)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProjectAdmin(string userId, Guid projectId)
        {
            try
            {
                var result = await _usrProjectRepository.DeleteProjectAsync(projectId, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}