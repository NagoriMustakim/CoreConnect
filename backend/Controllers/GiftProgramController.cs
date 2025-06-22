using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.GiftProgram;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.GiftProgram;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route(UserConstant.API_GIFT)]

    public class GiftProgramController : ControllerBase
    {
        private readonly IGiftProgramRepository _giftProgramRepository;

        public GiftProgramController(IGiftProgramRepository giftProgramRepository)
        {
            _giftProgramRepository = giftProgramRepository;
        }

        [Authorize(Roles = nameof(RoleTypes.Candidate), Policy = PolicyStrings.CREATE_GIFT_APPLICATION)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGiftProgram([FromBody] GiftProgramDTO giftProgram)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized();

                var (isExists, error) = await _giftProgramRepository.CheckGiftProgram(userId);
                if (isExists)
                {
                    var result = await _giftProgramRepository.CreateGiftProgramAsync(giftProgram, userId);

                    if (result != null)
                        return Ok(result);

                    return StatusCode(500);
                }
                return Conflict(new { message = error });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin) + UserConstant.COMMA + nameof(RoleTypes.Manager), Policy = PolicyStrings.VIEW_GIFT_APPLICATION)]
        [HttpGet(UserConstant.GET_ALL_GIFT)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGiftProgram([FromQuery] List<int> adminStatus, [FromQuery] List<int> managerStatus, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (result, totalCount) = await _giftProgramRepository.GetAllGiftProgramAsync(adminStatus, managerStatus, pageNumber, pageSize);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(new { List = result, TotalCount = totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Candidate), Policy = PolicyStrings.VIEW_GIFT_APPLICATION)]
        [HttpGet(UserConstant.GET_ALL_GIFT_BY_USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGiftProgramById()
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId.IsNullOrEmpty())
                    return Unauthorized();

                var result = await _giftProgramRepository.GetAllGiftProgramByIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(UserConstant.USER_GIFTPROGRAMID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGiftProgramById(Guid giftProgramId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return Unauthorized();

                var result = await _giftProgramRepository.GetGiftProgramByIdAsync(giftProgramId, userId);

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.UPDATE_GIFT_APPLICATION)]
        [HttpPut(UserConstant.USER_GIFTPROGRAMID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditGiftProgram(Guid giftProgramId, GiftProgramModifyDTO giftProgram)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return Unauthorized();

                var result = await _giftProgramRepository.EditGiftProgramAsync(giftProgramId, giftProgram, userId);

                if (result)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_GIFT_APPLICATION)]
        [HttpDelete(UserConstant.USER_GIFTPROGRAMID)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGiftProgram(Guid giftProgramId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return Unauthorized();

                var result = await _giftProgramRepository.DeleteGiftProgramAsync(giftProgramId, userId);

                if (result)
                    return NoContent();

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.UPDATE_GIFT_APPLICATION)]
        [HttpPut(AdminConstant.ADMIN_UPDATE_GIFT)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditGiftAdmin(string userId, Guid giftProgramId, GiftProgramModifyDTO giftProgram)
        {
            try
            {
                var result = await _giftProgramRepository.EditGiftProgramAsync(giftProgramId, giftProgram, userId);
                if (result)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.UPDATE_GIFT_APPLICATION)]
        [HttpGet(AdminConstant.ADMIN_GET_GIFT_BY_USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGiftProgramById(Guid giftProgramId, string userId)
        {
            try
            {
                if (userId == null)
                    return Unauthorized();

                var result = await _giftProgramRepository.GetGiftProgramByIdAsync(giftProgramId, userId);

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin) + UserConstant.COMMA + nameof(RoleTypes.Manager), Policy = PolicyStrings.APPROVE_GIFT_APPLICATION)]
        [HttpPut(AdminConstant.ADMIN_UPDATE_GIFT_STATUS)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStatusGiftProgram(string userId, Guid giftProgramId, GiftProgramStatusDTO dtoGiftProgramStatus)
        {
            try
            {
                if (userId == null)
                    return Unauthorized();

                var result = await _giftProgramRepository.UpdateStatusGiftProgramAsync(giftProgramId, dtoGiftProgramStatus, userId);

                if (result)
                    return Ok(result);

                return StatusCode(500, GiftProgramResponse.ERROR_GIFTPROGRAM_DATA_NOT_MODIFIED);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}