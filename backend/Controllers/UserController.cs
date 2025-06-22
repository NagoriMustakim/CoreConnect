using LinkwayAPI.Constants.API;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_API_USERS)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repositoryUser;
        public UserController(IUserRepository repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userList = await _repositoryUser.GetAllUsersAsync(pageNumber, pageSize);
            if (userList == null)
                return StatusCode(500);

            return Ok(userList);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpDelete(UserConstant.USERID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            try
            {
                var result = await _repositoryUser.DeleteUserAsync(userId);
                if (result == false)
                    return BadRequest();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet(AdminConstant.BUSINESS_UNIT_GUIID)]
        public async Task<IActionResult> GetResumeData(Guid businessUnitGuid, [FromQuery] string userId)
        {
            try
            {
                var result = await _repositoryUser.FetchResumeDataAsync(userId, businessUnitGuid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(UserConstant.PROFILE_STRENGTH)]
        public async Task<object> GetProfileStrength()
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var profilestrength = await _repositoryUser.GetProfileStrengthAsync(userId);

                return Ok(profilestrength);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
