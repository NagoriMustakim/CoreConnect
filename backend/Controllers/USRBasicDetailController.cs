using LinkwayAPI.Constants.API;
using LinkwayAPI.DTOs.User.BasicDetail;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_BASICDETAILS)]
    [ApiController]
    public class USRBasicDetailController : ControllerBase
    {
        private readonly IUSRBasicDetailRepository _repositoryUSRBasicDetail;

        public USRBasicDetailController(IUSRBasicDetailRepository repositoryUSRBasicDetail)
        {
            _repositoryUSRBasicDetail = repositoryUSRBasicDetail;
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate))]
        [HttpGet(UserConstant.USER_ID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrBasicDetailViewDTO>> GetAllBasicDetailsById(string userId)
        {
            try
            {
                var basicDetail = await _repositoryUSRBasicDetail.GetAllBasicDetailsByIdAsync(userId);
                if (basicDetail == null)
                {
                    return NotFound();
                }
                return Ok(basicDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate))]
        [HttpPut(UserConstant.USER_ID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBasicDetail([FromBody] UsrBasicDetailModifyDTO dtoUsrBasicDetailModify)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await _repositoryUSRBasicDetail.UpdateBasicDetailAsync(userId, dtoUsrBasicDetailModify);

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate) + UserConstant.COMMA + nameof(RoleTypes.Admin))]
        [HttpPut(UserConstant.USER_AVATAR)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAvatar()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                IFormFile avatarPhoto = null;

                if (formCollection.Files.Any())
                    avatarPhoto = formCollection.Files.First();

                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await _repositoryUSRBasicDetail.UpdateAvatarAsync(userId, avatarPhoto);

                if (result)
                    return Ok();

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate) + UserConstant.COMMA + nameof(RoleTypes.Admin))]
        [HttpDelete(UserConstant.USER_AVATAR_DELETE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAvatar()
        {
            try
            {

                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await _repositoryUSRBasicDetail.deleteAvatarAsync(userId);

                if (result)
                    return Ok();

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
       

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate) + UserConstant.COMMA + nameof(RoleTypes.Admin))]
        [HttpDelete(UserConstant.USER_ADMIN_AVATAR_DELETE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAvatarAdmin(string userId)
        {
            try
            {
                var result = await _repositoryUSRBasicDetail.deleteAvatarAsync(userId);

                if (result)
                    return Ok();

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpGet(AdminConstant.ADMIN)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrBasicDetailViewDTO>> GetAllBasicDetailsAdmin()
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var basicDetail = await _repositoryUSRBasicDetail.GetAllBasicDetailsByIdAsync(userId);
                if (basicDetail == null)
                {
                    return NotFound();
                }

                return Ok(basicDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpPut(UserConstant.USER_AVATAR_ADMIN)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAvatarAdmin(string userId)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                IFormFile avatarPhoto = null;

                if (formCollection.Files.Any())
                    avatarPhoto = formCollection.Files.First();

                var result = await _repositoryUSRBasicDetail.UpdateAvatarAsync(userId, avatarPhoto);

                if (result)
                    return Ok();

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpGet(AdminConstant.ADMIN_USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrBasicDetailViewDTO>> GetAllBasicDetailsByIdAdmin(string userId)
        {
            try
            {
                var basicDetail = await _repositoryUSRBasicDetail.GetAllBasicDetailsByIdAsync(userId);
                return Ok(basicDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpPut(AdminConstant.ADMIN_BASICDETAI_USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBasicDetailAdmin(string userId, [FromBody] UsrBasicDetailModifyDTO dtoUsrBasicDetailModify)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }
            try
            {
                var result = await _repositoryUSRBasicDetail.UpdateBasicDetailAsync(userId, dtoUsrBasicDetailModify);

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
    }
}
