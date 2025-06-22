using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.Constants.User;
using LinkwayAPI.DTOs.User.Certificate;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_CERTIFICATES)]
    [ApiController]
    public class USRCertificateController : ControllerBase
    {
        private readonly IUSRCertificateRepository _repositoryUSRCertificate;

        public USRCertificateController(IUSRCertificateRepository repositoryUSRCertificate)
        {
            _repositoryUSRCertificate = repositoryUSRCertificate;
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.CREATE_USER_CERTIFICATE)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCertificate([FromForm] UsrCertificateAddDTO dtoUsrCertificate)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var formCollection = await Request.ReadFormAsync();
                IFormFile certificationPhoto = null;

                if (formCollection.Files.Any())
                    certificationPhoto = formCollection.Files.First();

                var result = await _repositoryUSRCertificate.AddCertificateAsync(userId, dtoUsrCertificate, certificationPhoto);
                if (result == null)
                    return StatusCode(500);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet(UserConstant.GET_ALL_USER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrCertificateViewDTO>> GetAllCertificates(string userId)
        {
            try
            {
                if (userId == null)
                {
                    return BadRequest();
                }
                var certificates = await _repositoryUSRCertificate.GetAllCertificatesAsync(userId);

                return Ok(certificates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_CERTIFICATE)]
        [HttpGet(UserConstant.USER_CERTIFICATEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrCertificateViewDTO>> GetCertificateById(Guid certificateId)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                if (certificateId == Guid.Empty)
                {
                    return BadRequest();
                }
                var certificateDetail = await _repositoryUSRCertificate.GetCertificateByIdAsync(userId, certificateId);

                return Ok(certificateDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_CERTIFICATE)]
        [HttpPut(UserConstant.USER_CERTIFICATEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCertificate(Guid certificateId, [FromForm] UsrCertificateModifyDTO dtoUsrCertificateModify)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                IFormFile certificationPhoto = null;

                if (formCollection.Files.Any())
                    certificationPhoto = formCollection.Files.First();

                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                if (certificateId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSRCertificate.UpdateCertificateAsync(userId, certificateId, dtoUsrCertificateModify, certificationPhoto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.DELETE_USER_CERTIFICATE)]
        [HttpDelete(UserConstant.USER_CERTIFICATEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCertificate(Guid certificateId)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                if (certificateId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSRCertificate.DeleteCertificateAsync(userId, certificateId);

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

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_USER_CERTIFICATE)]
        [HttpPost(AdminConstant.ADMIN_USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCertificateAdmin(string userId, [FromForm] UsrCertificateAddDTO dtoUsrCertificate)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                IFormFile certificationPhoto = null;

                if (formCollection.Files.Any())
                    certificationPhoto = formCollection.Files.First();

                var result = await _repositoryUSRCertificate.AddCertificateAsync(userId, dtoUsrCertificate, certificationPhoto);

                if (result == null)
                    return BadRequest();


                return Ok(true);
            }
            catch
            {
                return StatusCode(500, UserResponseMessages.ERROR_INVALID_CERTIFICATE_DATA);
            }

        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.VIEW_USER_CERTIFICATE)]
        [HttpGet(AdminConstant.ADMIN_USERID_CERTIFICATEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsrCertificateViewDTO>> GetCertificateByIdAdmin(string userId, Guid certificateId)
        {
            try
            {
                if (certificateId == Guid.Empty)
                {
                    return BadRequest();
                }
                var certificateDetail = await _repositoryUSRCertificate.GetCertificateByIdAsync(userId, certificateId);

                return Ok(certificateDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_CERTIFICATE)]
        [HttpPut(AdminConstant.ADMIN_USERID_CERTIFICATEID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCertificateAdmin(string userId, Guid certificateId, [FromForm] UsrCertificateModifyDTO dtoUsrCertificateModify)
        {
            try
            {
                if (certificateId == Guid.Empty)
                {
                    return BadRequest();
                }

                var formCollection = await Request.ReadFormAsync();
                IFormFile certificationPhoto = null;

                if (formCollection.Files.Any())
                    certificationPhoto = formCollection.Files.First();

                var result = await _repositoryUSRCertificate.UpdateCertificateAsync(userId, certificateId, dtoUsrCertificateModify, certificationPhoto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_USER_CERTIFICATE)]
        [HttpDelete(AdminConstant.ADMIN_USERID_CERTIFICATEID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCertificateAdmin(string userId, Guid certificateId)
        {
            try
            {
                if (certificateId == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _repositoryUSRCertificate.DeleteCertificateAsync(userId, certificateId);

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
    }
}
