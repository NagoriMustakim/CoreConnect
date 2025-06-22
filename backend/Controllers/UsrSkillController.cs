using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.Constants.UserResponseMessage;
using LinkwayAPI.DTOs.User.Skill;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_SKILL)]
    [ApiController]
    public class USRSkillController : ControllerBase
    {
        private readonly IUSRSkillRepository _usrSkillRepository;

        public USRSkillController(IUSRSkillRepository usrSkillRepository)
        {
            _usrSkillRepository = usrSkillRepository;
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.CREATE_USER_SKILL)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSkill([FromBody] UsrSkillDTO skill)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return StatusCode(500);

                if (await _usrSkillRepository.IsSkillExistsAsync(skill.SkillGuid, userId))
                    return Conflict();

                var result = await _usrSkillRepository.CreateSkillAsync(skill, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Policy = PolicyStrings.VIEW_USER_SKILL)]
        [HttpGet(UserConstant.GET_ALL_USER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSkill(string userId)
        {
            try
            {
                var result = await _usrSkillRepository.GetAllSkillAsync(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_SKILL)]
        [HttpGet(UserConstant.USER_SKILLID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSkillById(Guid skillId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return StatusCode(500);

                var result = await _usrSkillRepository.GetSkillByIdAsync(skillId, userId);

                return Ok(result);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_SKILL)]
        [HttpPut(UserConstant.USER_SKILLID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditSkill(Guid skillId, UsrSkillDTO skill)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return StatusCode(500);


                if (await _usrSkillRepository.IsSkillExistsAsync(skill.SkillGuid, userId))
                    return Conflict();

                var result = await _usrSkillRepository.EditSkillAsync(skillId, skill, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500, UserResponseMessage.ERROR_SKILL_DATA_NOT_MODIFIED);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.DELETE_USER_SKILL)]
        [HttpDelete(UserConstant.USER_SKILLID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSkill(Guid skillId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return StatusCode(500);

                var result = await _usrSkillRepository.DeleteSkillAsync(skillId, userId);

                if (result)
                    return NoContent();

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_USER_SKILL)]
        [HttpPost(AdminConstant.ADMIN_USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSkillAdmin(string userId, [FromBody] UsrSkillDTO skill)
        {
            try
            {
                if (await _usrSkillRepository.IsSkillExistsAsync(skill.SkillGuid, userId))
                    return Conflict();

                var result = await _usrSkillRepository.CreateSkillAsync(skill, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_SKILL)]
        [HttpGet(AdminConstant.ADMIN_USERID_SKILLID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSkillByIdAdmin(string userId, Guid skillId)
        {
            try
            {
                var result = await _usrSkillRepository.GetSkillByIdAsync(skillId, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_SKILL)]
        [HttpPut(AdminConstant.ADMIN_UPDATE_SKILL)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditSkillAdmin(string userId, Guid skillId, UsrSkillDTO skill)
        {
            try
            {
                if (await _usrSkillRepository.IsSkillExistsAsync(skill.UserSkillGuid, userId))
                    return Conflict();

                var result = await _usrSkillRepository.EditSkillAsync(skillId, skill, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_USER_SKILL)]
        [HttpDelete(AdminConstant.ADMIN_DELETE_SKILL)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSkillAdmin(string userId, Guid skillId)
        {
            try
            {
                var result = await _usrSkillRepository.DeleteSkillAsync(skillId, userId);

                if (result)
                    return NoContent();

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
