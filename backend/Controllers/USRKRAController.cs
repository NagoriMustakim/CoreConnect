using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.User.Kra;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_KRA)]
    [ApiController]
    public class USRKRAController : ControllerBase
    {
        private readonly IUSRKraRepository _repositoryUSRKra;

        public USRKRAController(IUSRKraRepository repositoryUSRKra)
        {
            _repositoryUSRKra = repositoryUSRKra;
        }

        [HttpGet(UserConstant.GET_ALL_USER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrKraViewDTO>> GetAllKras(string userId)
        {
            try
            {
                if (userId == null)
                {
                    return BadRequest();
                }
                var Kras = await _repositoryUSRKra.GetAllKrasAsync(userId);
                if (Kras == null)
                {
                    return NotFound();
                }
                return Ok(Kras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.HR) + "," + nameof(RoleTypes.Manager) + "," + nameof(RoleTypes.RMG), Policy = PolicyStrings.CREATE_USER_KRA)]
        [HttpPost(AdminConstant.ADMIN_USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddKraAdmin(string userId, [FromBody] UsrKraDTO dtoUsrKra)
        {
            try
            {
                var result = await _repositoryUSRKra.AddKraAsync(userId, dtoUsrKra);

                if (result == null)
                    return BadRequest();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.HR) + "," + nameof(RoleTypes.Manager) + "," + nameof(RoleTypes.RMG), Policy = PolicyStrings.EDIT_USER_KRA)]
        [HttpGet(AdminConstant.ADMIN_USERID_KRAID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrKraViewDTO>> GetKraByIdAdmin(string userId, Guid kraId)
        {
            try
            {
                if (kraId == Guid.Empty)
                {
                    return BadRequest();
                }
                var KraDetail = await _repositoryUSRKra.GetKraByIdAsync(userId, kraId);
                return Ok(KraDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.HR) + "," + nameof(RoleTypes.Manager) + "," + nameof(RoleTypes.RMG), Policy = PolicyStrings.EDIT_USER_KRA)]
        [HttpPut(AdminConstant.ADMIN_UPDATE_KRA)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateKraAdmin(string userId, Guid kraId, [FromBody] UsrKraModifyDTO dtoUsrKraModify)
        {
            try
            {
                if (kraId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSRKra.UpdateKraAsync(userId, kraId, dtoUsrKraModify);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.HR) + "," + nameof(RoleTypes.Manager) + "," + nameof(RoleTypes.RMG), Policy = PolicyStrings.DELETE_USER_KRA)]
        [HttpDelete(AdminConstant.ADMIN_DELETE_KRA)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteKraAdmin(string userId, Guid kraId)
        {
            try
            {
                if (kraId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSRKra.DeleteKraAsync(userId, kraId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
