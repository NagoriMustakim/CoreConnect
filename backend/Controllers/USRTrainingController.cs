using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.User.Training;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_API + UserConstant.USER_TRAINING)]
    [ApiController]
    public class USRTrainingController : ControllerBase
    {
        private readonly IUSRTrainingRepository _usrTrainingRepository;
        public USRTrainingController(IUSRTrainingRepository usrTrainingRepository)
        {
            _usrTrainingRepository = usrTrainingRepository;
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.CREATE_USER_TRAINING)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTraining([FromBody] UsrTrainingDTO usrTraining)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return StatusCode(500);

                var result = await _usrTrainingRepository.CreateTrainingAsync(usrTraining, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Policy = PolicyStrings.VIEW_USER_TRAINING)]
        [HttpGet(UserConstant.GET_ALL_USER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTraining(string userId)
        {
            try
            {
                var result = await _usrTrainingRepository.GetAllTrainingAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_TRAINING)]
        [HttpGet(UserConstant.USER_TRAININGID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrainingById(Guid trainingId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return StatusCode(500);

                var result = await _usrTrainingRepository.GetTrainingByIdAsync(trainingId, userId);

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.EDIT_USER_TRAINING)]
        [HttpPut(UserConstant.USER_TRAININGID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditTraining(Guid trainingId, [FromBody] UsrTrainingModifyDTO usrTraining)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return StatusCode(500);

                var result = await _usrTrainingRepository.EditTrainingAsync(trainingId, usrTraining, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Candidate), Policy = PolicyStrings.DELETE_USER_TRAINING)]
        [HttpDelete(UserConstant.USER_TRAININGID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTraining(Guid trainingId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return StatusCode(500);

                var result = await _usrTrainingRepository.DeleteTrainingAsync(trainingId, userId);

                if (result)
                    return NoContent();

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_USER_TRAINING)]
        [HttpPost(AdminConstant.ADMIN_USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTrainingAdmin(string userId, [FromBody] UsrTrainingDTO usrTraining)
        {
            try
            {
                if (userId == null)
                    return StatusCode(500);

                var result = await _usrTrainingRepository.CreateTrainingAsync(usrTraining, userId);
                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_TRAINING)]
        [HttpGet(AdminConstant.ADMIN_USERID_TRAININGID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrainingByIdAdmin(string userId, Guid trainingId)
        {
            try
            {
                if (userId == null)
                    return NotFound();

                var result = await _usrTrainingRepository.GetTrainingByIdAsync(trainingId, userId);

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_TRAINING)]
        [HttpPut(AdminConstant.ADMIN_UPDATE_TRAINING)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditTrainingAdmin(string userId, Guid trainingId, [FromBody] UsrTrainingModifyDTO usrTraining)
        {
            try
            {
                if (userId == null)
                    return NotFound();

                var result = await _usrTrainingRepository.EditTrainingAsync(trainingId, usrTraining, userId);

                if (result != null)
                    return Ok(result);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_USER_TRAINING)]
        [HttpDelete(AdminConstant.ADMIN_DELETE_TRAINING)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTrainingAdmin(string userId, Guid trainingId)
        {
            try
            {
                if (userId == null)
                    return StatusCode(500);

                var result = await _usrTrainingRepository.DeleteTrainingAsync(trainingId, userId);

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
