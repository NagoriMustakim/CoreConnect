using LinkwayAPI.Constants.API;
using LinkwayAPI.DTOs.Training;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(AdminConstant.API_TRAINING)]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingRepository _trainingRepository;

        public TrainingController(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTrainings([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (result, count) = await _trainingRepository.getAllTrainingAsync(pageNumber, pageSize);
                return Ok(new { list = result, count = count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(UserConstant.ALL_TRAININGS)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTraining()
        {
            try
            {
                var result = await _trainingRepository.getAllTrainingAsync(0, 0);
                return Ok(result.list);
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
        public async Task<IActionResult> CreateTraining([FromBody] TrainingCreateDTO dtoTrainingCreate)
        {
            try
            {
                if (await _trainingRepository.IsTrainingExists(dtoTrainingCreate.TrainingTitle, null))
                    return Conflict();

                var result = await _trainingRepository.CreateTrainingAsync(dtoTrainingCreate);
                if (result != null) return Ok(result);

                return StatusCode(500);

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
        public async Task<ActionResult> UpdateTraining(TrainingModifyDTO dtoTrainingModify)
        {
            try
            {
                if (await _trainingRepository.IsTrainingExists(dtoTrainingModify.TrainingTitle, dtoTrainingModify.TrainingGuid))
                    return Conflict();

                var result = await _trainingRepository.UpdateTrainingAsync(dtoTrainingModify);

                if (result != null) return Ok(result);
                return StatusCode(500);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpDelete("{trainingGuid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTraining(Guid trainingGuid)
        {
            try
            {
                var result = await _trainingRepository.DeleteTrainingAsync(trainingGuid);
                if (result) return NoContent();
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
