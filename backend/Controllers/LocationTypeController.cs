using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.LocationType;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(AdminConstant.ADMIN_LOCATION_TYPE)]
    [ApiController]
    public class LocationTypeController : ControllerBase
    {

        private readonly ILocationTypeRepository _repositoryLocationType;
        public LocationTypeController(ILocationTypeRepository repositoryLocationType)
        {
            _repositoryLocationType = repositoryLocationType;
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_LOCATION_TYPE)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationTypeDTO>> AddLocationType([FromBody] LocationTypeDTO dtoLocationType)
        {
            var IsLocationTypeExists = await _repositoryLocationType.IsLocationTypeExistsAsync(dtoLocationType.LocationType, null);

            if (IsLocationTypeExists)
                return Conflict();

            var locationType = await _repositoryLocationType.AddLocationTypeAsync(dtoLocationType);
            if (locationType == null)
                return StatusCode(500);

            return Created();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<LocationTypeViewDTO>> GetAllLocationType()
        {
            var locationTypeList = await _repositoryLocationType.GetAllLocationTypeAsync();

            return Ok(locationTypeList);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpGet(AdminConstant.ADMIN_LOCATION_TYPE_ID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LocationTypeViewDTO>> GetLocationTypeById(Guid locationTypeId)
        {
            var locationType = await _repositoryLocationType.GetLocationTypeByIdAsync(locationTypeId);

            if (locationType == null)
                return NotFound();

            return Ok(locationType.Value);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_LOCATION_TYPE)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationTypeViewDTO>> UpdateLocationType([FromBody] LocationTypeModifyDTO dtoLocationTypeModify)
        {
            var IsLocationTypeExists = await _repositoryLocationType.IsLocationTypeExistsAsync(dtoLocationTypeModify.LocationType, dtoLocationTypeModify.LocationTypeGuid);

            if (IsLocationTypeExists)
                return Conflict();

            var result = await _repositoryLocationType.UpdateLocationTypeAsync(dtoLocationTypeModify);

            if (result == false)
                return StatusCode(500);

            return Ok(result);
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_LOCATION_TYPE)]
        [HttpDelete(AdminConstant.ADMIN_LOCATION_TYPE_ID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLocationType(Guid locationTypeId)
        {
            var result = await _repositoryLocationType.DeleteLocationTypeAsync(locationTypeId);

            if (result == false)
                return StatusCode(500);

            return NoContent();
        }
    }
}
