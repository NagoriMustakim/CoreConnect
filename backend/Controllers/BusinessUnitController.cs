using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.BusinessUnit;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(AdminConstant.API_BUSINESSUNIT)]
    [ApiController]
    public class BusinessUnitController : ControllerBase
    {
        private readonly IBusinessUnitRepository _repositoryBusinessUnit;
        private readonly IFileRepository _repositoryFile;

        public BusinessUnitController(IBusinessUnitRepository repositoryBusinessUnit, IFileRepository repositoryFile)
        {
            _repositoryBusinessUnit = repositoryBusinessUnit;
            _repositoryFile = repositoryFile;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBusinessUnits()
        {
            try
            {
                var result = await _repositoryBusinessUnit.GetAllBusinessUnitsListAsync();
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_BUSSINESS_UNIT)]
        [HttpGet(AdminConstant.BUSINESS_UNIT_GUIID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBusinessUnitById(Guid businessUnitId)
        {
            try
            {
                if (businessUnitId == Guid.Empty)
                    return BadRequest();

                var result = await _repositoryBusinessUnit.GetBusinessUnitByIdAsync(businessUnitId);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_BUSSINESS_UNIT)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBusinessUnit([FromForm] BusinessUnitCreateEditDTO dtoBusinessUnitCreate)
        {
            try
            {
                if (await _repositoryBusinessUnit.isBusinessUnitExits(dtoBusinessUnitCreate.BusinessUnitName, null))
                    return Conflict();

                var formCollection = await Request.ReadFormAsync();
                IFormFile businessUnitPhoto = null;

                if (formCollection.Files.Any())
                    businessUnitPhoto = formCollection.Files.First();

                if (businessUnitPhoto != null)
                {
                    var fileResult = _repositoryFile.SaveImage(businessUnitPhoto);
                    if (fileResult.Item1 == 1)
                    {
                        dtoBusinessUnitCreate.BusinessUnitLogoName = fileResult.Item2;
                    }
                }

                var result = await _repositoryBusinessUnit.CreateBusinessUnitAsync(dtoBusinessUnitCreate);
                if (!result)
                    return StatusCode(500);

                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_BUSSINESS_UNIT)]
        [HttpPut(AdminConstant.BUSINESS_UNIT_GUIID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditBusinessUnit(Guid businessUnitGuid, [FromForm] BusinessUnitCreateEditDTO dtoBusinessUnitEdit)
        {
            try
            {
                if (await _repositoryBusinessUnit.isBusinessUnitExits(dtoBusinessUnitEdit.BusinessUnitName, businessUnitGuid))
                    return Conflict();

                var formCollection = await Request.ReadFormAsync();
                IFormFile businessUnitPhoto = null;

                if (formCollection.Files.Any())
                    businessUnitPhoto = formCollection.Files.First();

                if (businessUnitPhoto != null)
                {
                    await _repositoryBusinessUnit.EditImageAsync(businessUnitGuid, businessUnitPhoto);
                }

                var result = await _repositoryBusinessUnit.EditBusinessUnitAsync(businessUnitGuid, dtoBusinessUnitEdit);
                if (!result)
                    return StatusCode(500);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_BUSSINESS_UNIT)]
        [HttpDelete(AdminConstant.BUSINESS_UNIT_GUIID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBusinessUnit(Guid businessUnitGuid)
        {
            try
            {
                if (businessUnitGuid == Guid.Empty)
                    return BadRequest();
                bool result = await _repositoryBusinessUnit.DeleteBusinessUnitAsync(businessUnitGuid);
                if (!result)
                    return StatusCode(500);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
