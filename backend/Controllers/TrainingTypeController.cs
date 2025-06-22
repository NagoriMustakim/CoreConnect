using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.TrainingType;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(AdminConstant.ADMIN_TRAINING_TYPE)]
    [ApiController]

    public class TrainingTypeController : ControllerBase
    {
        private readonly ITrainingTypeRepository _repositoryTrainingType;
        public TrainingTypeController(ITrainingTypeRepository repositoryTrainingTypeRepository)
        {
            _repositoryTrainingType = repositoryTrainingTypeRepository;
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_TRAINING_TYPE)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TrainingTypeDTO>> AddTrainingType([FromBody] TrainingTypeDTO dtoTrainingType)
        {
            try
            {
                var IsTrainingTypeExists = await _repositoryTrainingType.IsTrainingTypeExistsAsync(dtoTrainingType.TrainingType, null);
                if (IsTrainingTypeExists)
                    return Conflict();

                var trainingType = await _repositoryTrainingType.AddTrainingTypeAsync(dtoTrainingType);
                if (trainingType == null)
                    return BadRequest();

                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Policy = PolicyStrings.VIEW_TRAINING_TYPE)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TrainingTypeViewDTO>> GetAllTrainingType()
        {
            try
            {
                var trainingTypeList = await _repositoryTrainingType.GetAllTrainingTypeAsync();

                return Ok(trainingTypeList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_TRAINING_TYPE)]
        [HttpGet(AdminConstant.ADMIN_TRAINING_TYPE_ID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TrainingTypeViewDTO>> GetTrainingTypeById(Guid trainingTypeId)
        {
            try
            {
                var trainingType = await _repositoryTrainingType.GetTrainingTypeByIdAsync(trainingTypeId);

                if (trainingType == null)
                    return NotFound();

                return Ok(trainingType.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_TRAINING_TYPE)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TrainingTypeViewDTO>> UpdateTrainingType([FromBody] TrainingTypeModifyDTO dtotrainingTypeModify)
        {
            try
            {
                var IsTrainingTypeExists = await _repositoryTrainingType.IsTrainingTypeExistsAsync(dtotrainingTypeModify.TrainingType, dtotrainingTypeModify.TrainingTypeGuid);
                if (IsTrainingTypeExists)
                    return Conflict();

                var result = await _repositoryTrainingType.UpdateTrainingTypeAsync(dtotrainingTypeModify.TrainingTypeGuid, dtotrainingTypeModify);

                if (result == false)
                    return StatusCode(500);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_TRAINING_TYPE)]
        [HttpDelete(AdminConstant.ADMIN_TRAINING_TYPE_ID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTrainingType(Guid trainingTypeId)
        {
            try
            {
                var result = await _repositoryTrainingType.DeleteTrainingTypeAsync(trainingTypeId);

                if (result == false)
                    return BadRequest();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
