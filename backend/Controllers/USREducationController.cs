using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.User.Education;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_EDUCATION)]
    [ApiController]
    public class USREducationController : ControllerBase
    {
        private readonly IUSREducationRepository _repositoryUSREducation;

        public USREducationController(IUSREducationRepository repositoryUSREducation)
        {
            _repositoryUSREducation = repositoryUSREducation;
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.CREATE_USER_EDUCATION)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEducation([FromBody] UsrEducationDTO dtoUsrEducation)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _repositoryUSREducation.AddEducationAsync(userId, dtoUsrEducation);

                if (result == false)
                    return StatusCode(500);


                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(UserConstant.GET_ALL_USER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrEducationViewDTO>> GetAllEducationDetails(string userId)
        {
            try
            {
                if (userId == null)
                {
                    return Unauthorized();
                }
                var educations = await _repositoryUSREducation.GetAllEducationDetailsAsync(userId);
                if (educations == null)
                {
                    return NotFound();
                }
                return Ok(educations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_EDUCATION)]
        [HttpGet(UserConstant.USER_EDUCATIONID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrEducationViewDTO>> GetEducationById(Guid educationId)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                if (educationId == Guid.Empty)
                {
                    return BadRequest();
                }
                var educationDetail = await _repositoryUSREducation.GetEducationByIdAsync(userId, educationId);
                if (educationDetail == null)
                {
                    return NotFound();
                }
                return Ok(educationDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_EDUCATION)]
        [HttpPut(UserConstant.USER_EDUCATIONID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEducation(Guid educationId, [FromBody] UsrEducationModifyDTO dtoUsrEducationModify)
        {

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                if (educationId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSREducation.UpdateEducationAsync(userId, educationId, dtoUsrEducationModify);

                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.DELETE_USER_EDUCATION)]
        [HttpDelete(UserConstant.USER_EDUCATIONID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEducation(Guid educationId)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    return Unauthorized();
                }

                if (educationId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSREducation.DeleteEducationAsync(userId, educationId);

                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //Admin Endpoints
        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_USER_EDUCATION)]
        [HttpPost(AdminConstant.ADMIN_USERID)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEducationAdmin(string userId, [FromBody] UsrEducationDTO dtoUsrEducation)
        {

            try
            {
                var result = await _repositoryUSREducation.AddEducationAsync(userId, dtoUsrEducation);

                if (result == false)
                    return BadRequest();


                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_EDUCATION)]
        [HttpGet(AdminConstant.ADMIN_USERID_EXPRIENCEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrEducationViewDTO>> GetEducationByIdAdmin(string userId, Guid educationId)
        {
            try
            {
                if (educationId == Guid.Empty)
                {
                    return BadRequest();
                }
                var educationDetail = await _repositoryUSREducation.GetEducationByIdAsync(userId, educationId);
                if (educationDetail == null)
                {
                    return NotFound();
                }
                return Ok(educationDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_EDUCATION)]
        [HttpPut(AdminConstant.ADMIN_USERID_EXPRIENCEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEducation(string userId, Guid educationId, [FromBody] UsrEducationModifyDTO dtoUsrEducationModify)
        {
            try
            {
                if (educationId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSREducation.UpdateEducationAsync(userId, educationId, dtoUsrEducationModify);

                if (!result)
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

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_USER_EDUCATION)]
        [HttpDelete(AdminConstant.ADMIN_USERID_EXPRIENCEID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEducation(string userId, Guid educationId)
        {
            try
            {
                if (educationId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSREducation.DeleteEducationAsync(userId, educationId);

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
