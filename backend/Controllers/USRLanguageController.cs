using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.User.UsrLanguage;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_LANGUAGE)]
    [ApiController]
    public class USRLanguageController : ControllerBase
    {

        private readonly IUSRLanguageRepository _usrlanguageRepository;

        public USRLanguageController(IUSRLanguageRepository usrlanguageRepository)
        {
            _usrlanguageRepository = usrlanguageRepository;
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.CREATE_USER_LANGUAGE)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLanguage([FromBody] UsrLanguageDTO language)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (await _usrlanguageRepository.IsLanguageExistsAsync(language.LanguageName, userId, null))
                    return Conflict();

                var result = await _usrlanguageRepository.CreateLanguageAsync(language, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Policy = PolicyStrings.VIEW_USER_LANGUAGE)]
        [HttpGet(UserConstant.GET_ALL_USER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLanguages(string userId)
        {
            try
            {
                var result = await _usrlanguageRepository.GetAllLanguageAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_LANGUAGE)]
        [HttpGet(UserConstant.USER_LANAGUAGEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLanguageById(Guid languageId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                var result = await _usrlanguageRepository.GetLanguageByIdAsync(languageId, userId);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_LANGUAGE)]
        [HttpPut(UserConstant.USER_LANAGUAGEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditLanguage(Guid languageId, UsrLanguageModifiedDTO language)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                if (await _usrlanguageRepository.IsLanguageExistsAsync(language.LanguageName, userId, languageId))
                    return Conflict();

                var result = await _usrlanguageRepository.EditLanguageAsync(languageId, language, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.DELETE_USER_LANGUAGE)]
        [HttpDelete(UserConstant.USER_LANAGUAGEID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLanguage(Guid languageId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                var result = await _usrlanguageRepository.DeleteLanguageAsync(languageId, userId);

                if (result)
                    return NoContent();

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //Admin Endpoints
        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_USER_LANGUAGE)]
        [HttpPost(AdminConstant.ADMIN_USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLanguageAdmin(string userId, [FromBody] UsrLanguageDTO language)
        {
            try
            {

                if (await _usrlanguageRepository.IsLanguageExistsAsync(language.LanguageName, userId, null))
                    return Conflict();

                var result = await _usrlanguageRepository.CreateLanguageAsync(language, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_LANGUAGE)]
        [HttpGet(AdminConstant.ADMIN_USERID_LANGUAGEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLanguageByIdAdmin(Guid languageId, string userId)
        {
            try
            {
                var result = await _usrlanguageRepository.GetLanguageByIdAsync(languageId, userId);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_LANGUAGE)]
        [HttpPut(AdminConstant.ADMIN_UPDATE_LANGUAGE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditLanguageAdmin(Guid languageId, string userId, UsrLanguageModifiedDTO language)
        {
            try
            {

                if (await _usrlanguageRepository.IsLanguageExistsAsync(language.LanguageName, userId, languageId))
                    return Conflict();

                var result = await _usrlanguageRepository.EditLanguageAsync(languageId, language, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_USER_LANGUAGE)]
        [HttpDelete(AdminConstant.ADMIN_DELETE_LANGUAGE)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLanguageAdmin(Guid languageId, string userId)
        {
            try
            {
                var result = await _usrlanguageRepository.DeleteLanguageAsync(languageId, userId);

                if (result)
                    return NoContent();

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

