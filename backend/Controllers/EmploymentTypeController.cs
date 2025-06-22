using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.EmploymentType;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(AdminConstant.ADMIN_EMPLOYMENT_TYPE)]
    [ApiController]
    public class EmploymentTypeController : ControllerBase
    {
        private readonly IEmploymentTypeRepository _repositoryEmployementType;
        public EmploymentTypeController(IEmploymentTypeRepository repositoryEmployementType)
        {
            _repositoryEmployementType = repositoryEmployementType;
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_EMPLOYMENT_TYPE)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEmploymentType([FromBody] EmploymentTypeDTO dtoEmploymentType)
        {
            var isEmploymentTypeExists = await _repositoryEmployementType.IsEmploymentTypeExistsAsync(dtoEmploymentType.EmploymentTypeTitle,null);
            if (isEmploymentTypeExists)
                return Conflict();

            var employmentType = await _repositoryEmployementType.AddEmploymentTypeAsync(dtoEmploymentType);
            if (employmentType == null)
                return StatusCode(500);

            return Created();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmploymentTypeViewDTO>> GetAllEmploymentType()
        {
            var employmentTypelist = await _repositoryEmployementType.GetAllEmploymentTypeAsync();

            return Ok(employmentTypelist);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_EMPLOYMENT_TYPE)]
        [HttpGet(AdminConstant.ADMIN_EMPLOYMENT_TYPE_ID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmploymentTypeViewDTO>> GetEmploymentTypeById(Guid employmentTypeId)
        {
            var employmentType = await _repositoryEmployementType.GetEmploymentTypeByIdAsync(employmentTypeId);

            if (employmentType == null)
                return NotFound();

            return Ok(employmentType.Value);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_EMPLOYMENT_TYPE)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmploymentTypeViewDTO>> UpdateEmploymentType([FromBody] EmploymentTypeModifyDTO dtoEmploymentTypeModify)
        {
            if (await _repositoryEmployementType.IsEmploymentTypeExistsAsync(dtoEmploymentTypeModify.EmploymentTypeTitle, dtoEmploymentTypeModify.EmploymentTypeGuid))
                return Conflict();

            var result = await _repositoryEmployementType.UpdateEmploymentTypeAsync(dtoEmploymentTypeModify);
            if (result == false)
                return StatusCode(500);

            return Ok(result);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_EMPLOYMENT_TYPE)]
        [HttpDelete(AdminConstant.ADMIN_EMPLOYMENT_TYPE_ID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmploymentType(Guid employmentTypeId)
        {
            var result = await _repositoryEmployementType.DeleteEmploymentTypeAsync(employmentTypeId);

            if (result == false)
                return StatusCode(500);

            return NoContent();
        }
    }
}
