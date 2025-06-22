using LinkwayAPI.DTOs.Department;
using LinkwayAPI.DTOs.Designation;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _repositoryDepartment;
        public DepartmentController(IDepartmentRepository repositoryDepartment)
        {
            _repositoryDepartment = repositoryDepartment;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDepartments([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var (result, totalCount) = await _repositoryDepartment.GetAllDepartmentAsync(pageNumber, pageSize);
                return Ok(new { list = result, count = totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentCreateDTO dtoDepartmentCreate)
        {
            try
            {
                if (await _repositoryDepartment.IsDepartmentExistsAsync(dtoDepartmentCreate.Department, null))
                    return Conflict();

                var result = await _repositoryDepartment.AddDeparmentAsync(dtoDepartmentCreate);


                if (result == null)
                    return StatusCode(500);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DesignationViewDTO>> UpdateDepartment([FromBody] DepartmentModifyDTO dtoDepartmentModify)
        {
            if (await _repositoryDepartment.IsDepartmentExistsAsync(dtoDepartmentModify.Department, dtoDepartmentModify.DepartmentGuid))
                return Conflict();

            var result = await _repositoryDepartment.UpdateDepartmentAsync(dtoDepartmentModify.DepartmentGuid, dtoDepartmentModify);
            if (result == null)
                return StatusCode(500);

            return Ok(result);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpDelete("{departmentGuid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCompony(Guid departmentGuid)
        {
            var result = await _repositoryDepartment.DeleteDepartmentAsync(departmentGuid);

            if (result == false)
                return StatusCode(500);

            return NoContent();
        }
    }
}
