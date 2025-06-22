using LinkwayAPI.Constants.API;
using LinkwayAPI.DTOs.InternalProgramsCategories;
using LinkwayAPI.DTOs.LocationType;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Route(AdminConstant.ADMIN_INTERNAL_PROGRAM_CATEGORY)]
    [ApiController]
    public class InternalProgramCategoryController : ControllerBase
    {
        private readonly IInternalProgramCategoryRepository _repositoryInternalProgramCategories;

        public InternalProgramCategoryController(IInternalProgramCategoryRepository repositoryInternalProgramCategories)
        {
            _repositoryInternalProgramCategories = repositoryInternalProgramCategories;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (result, totalCount) = await _repositoryInternalProgramCategories.GetAllCategoriesAsync(pageNumber, pageSize);

                return Ok(new { List = result, count = totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{internalProgramGuid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoriesByInternalProgramId(Guid internalProgramGuid)
        {
            try
            {
                var result = await _repositoryInternalProgramCategories.GetCategoriesByInternalProgramIdAsync(internalProgramGuid);

                return Ok(result);
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
        public async Task<IActionResult> CreateCategory([FromBody] InternalProgramCategoryCreateDTO dtoInternalProgramCategoryCreate)
        {
            try
            {
                if (await _repositoryInternalProgramCategories.IsCategoryExists(dtoInternalProgramCategoryCreate.InternalProgramGuid, dtoInternalProgramCategoryCreate.InternalProgramCategory, null))
                    return Conflict();

                var result = await _repositoryInternalProgramCategories.CreateCategoryAsync(dtoInternalProgramCategoryCreate);
                if (result == null)
                    return StatusCode(500);

                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpPut(AdminConstant.ADMIN_INTERNAL_PROGRAM_CATEGORY_ID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationTypeViewDTO>> UpdateCategory([FromBody] InternalProgramCategoryModifyDTO dtoInternalProgramCategoryModify)
        {
            if (await _repositoryInternalProgramCategories.IsCategoryExists(dtoInternalProgramCategoryModify.InternalProgramGuid, dtoInternalProgramCategoryModify.InternalProgramCategory, dtoInternalProgramCategoryModify.InternalProgramCategoryGuid))
                return Conflict();

            var result = await _repositoryInternalProgramCategories.UpdateCategoryAsync(dtoInternalProgramCategoryModify);
            if (result == null)
                return StatusCode(500);

            return Ok(result);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpDelete(AdminConstant.ADMIN_INTERNAL_PROGRAM_CATEGORY_ID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(Guid internalProgramCategoryId)
        {
            var result = await _repositoryInternalProgramCategories.DeleteCategoryAsync(internalProgramCategoryId);

            if (result == false)
                return StatusCode(500);

            return NoContent();
        }
    }
}
