using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.Constants.ResponseMessages;
using LinkwayAPI.DTOs.Nomination;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(AdminConstant.API_NOMINATION)]
    [ApiController]

    public class NominationController : ControllerBase
    {
        private readonly INominationRepository _nominationRepository;

        public NominationController(INominationRepository nominationRepository)
        {
            _nominationRepository = nominationRepository;
        }

        [Authorize(nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG), Policy = PolicyStrings.CREATE_NOMINATION_POLICY)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNomination([FromForm] NominationDTO nomination)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return Unauthorized();

                var (isNominated, error) = await _nominationRepository.IsNominationExistsAsync(nomination.NomineeGuid, userId, nomination.InternalProgramGuid);
                if (isNominated)
                    return Conflict(new { message = error });

                var (isNominationValid, iteration) = await _nominationRepository.IsNominationVaildAsync(nomination.InternalProgramGuid);

                if (!isNominationValid)
                    return StatusCode(500, ResponseMessages.NOMINATIONS_CLOSED);

                var formCollection = await Request.ReadFormAsync();

                var result = await _nominationRepository.CreateNominationAsync(nomination, userId, iteration, formCollection);

                if (result)
                    return Created();
                else
                    return StatusCode(500, ResponseMessages.TRY_AGAIN);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin) + UserConstant.COMMA + nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG), Policy = PolicyStrings.VIEW_NOMINATION_POLICY)]
        [HttpGet(AdminConstant.GET_ALL_NOMINATION)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllNomination([FromQuery] List<int> status, Guid internalProgramId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (result, totalCount) = await _nominationRepository.GetAllNominationByNominationIdAsync(internalProgramId, status, pageNumber, pageSize);
                return Ok(new { list = result, count = totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin) + UserConstant.COMMA + nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG), Policy = PolicyStrings.UPDATE_NOMINATION_POLICY)]
        [HttpGet(AdminConstant.NOMINATIONID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetNominationById(Guid nominationId)
        {
            try
            {
                var nomination = await _nominationRepository.GetNominationByIdAsync(nominationId);
                if (nomination == null)
                {
                    return NotFound();
                }
                return Ok(nomination);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_NOMINATION_POLICY)]
        [HttpDelete(AdminConstant.NOMINATIONID)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid nominationId)
        {
            try
            {
                var isNomination = await _nominationRepository.DeleteNominationAsync(nominationId);
                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_NOMINATION_POLICY)]
        //[HttpPost(AdminConstant.ADMIN_USERID)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> CreateNominationAdmin(string userId, [FromBody] NominationDTO nomination)
        //{
        //    try
        //    {
        //        //var (isNominated, error) = await _nominationRepository.IsNominationExistsAsync(nomination.NomineeGuid, nomination.InternalProgramGuid);
        //        //if (isNominated)
        //        //    return Conflict(new { message = error });

        //        var result = await _nominationRepository.CreateNominationAsync(nomination, userId, 0);
        //        if (result)
        //        {
        //            return Created();
        //        }
        //        else
        //        {
        //            return StatusCode(500, ResponseMessages.TRY_AGAIN); ;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG), Policy = PolicyStrings.VIEW_NOMINATION_POLICY)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllNominations([FromQuery] List<int> status, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var (result, totalCount) = await _nominationRepository.GetAllNominationAsync(userId, status, pageNumber, pageSize);
                return Ok(new { list = result, count = totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.UPDATE_NOMINATION_POLICY)]
        [HttpPut(AdminConstant.NOMINATION_APPROVE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApproveNomination(Guid nominationId)
        {
            try
            {
                var result = await _nominationRepository.ApproveNominationAsync(nominationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.UPDATE_NOMINATION_POLICY)]
        [HttpPut(AdminConstant.NOMINATION_REJECT)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RejectNomination(Guid nominationId, NominationModifyDTO dtoNominationModify)
        {
            try
            {
                var result = await _nominationRepository.RejectNominationAsync(nominationId, dtoNominationModify);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}