using LinkwayAPI.DTOs.Certification;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Route("api/certifications")]
    [ApiController]
    public class CertificationController : ControllerBase
    {
        private readonly ICertificationRepository _repositoryCertification;

        public CertificationController(ICertificationRepository repositoryCertification)
        {
            _repositoryCertification = repositoryCertification;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCertifications([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (result, totalCount) = await _repositoryCertification.GetAllCertificationsAsync(pageNumber, pageSize);
                if (result == null)
                    return NotFound();

                return Ok(new { list = result, count = totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCertification([FromBody] CertificationAddDTO dtoCertificationAdd)
        {
            try
            {
                if (await _repositoryCertification.IsCertificationExists(dtoCertificationAdd.CertificationTitle, null))
                    return Conflict();

                var result = await _repositoryCertification.CreateCertificationAsync(dtoCertificationAdd);
                if (!result)
                    return StatusCode(500);

                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpPut("{certificationGuid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CertificationViewDTO>> UpdateCertification([FromBody] CertificationEditDTO dtoCertificationEdit)
        {
            if (await _repositoryCertification.IsCertificationExists(dtoCertificationEdit.CertificationTitle, dtoCertificationEdit.CertificationGuid))
                return Conflict();

            var result = await _repositoryCertification.UpdateCertificationAsync(dtoCertificationEdit);
            if (result == false)
                return StatusCode(500);

            return Ok(result);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpDelete("{certificationGuid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCertification(Guid certificationGuid)
        {
            var result = await _repositoryCertification.DeleteCertificationAsync(certificationGuid);

            if (result == false)
                return StatusCode(500);

            return NoContent();
        }
    }
}
