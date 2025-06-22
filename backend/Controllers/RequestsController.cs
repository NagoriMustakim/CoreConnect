using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.Request;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_REQUESTS)]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestRepository _repositoryRequest;
        private readonly UserManager<UsrUser> _userManager;

        public RequestsController(IRequestRepository repositoryRequest, UserManager<UsrUser> userManager)
        {
            _repositoryRequest = repositoryRequest;
            _userManager = userManager;
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.VIEW_REQUEST)]
        [HttpGet(AdminConstant.ADMIN)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RequestListAdminDTO>>> GetAllRequests([FromQuery] List<int> status, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (allRequestList, totalCount) = await _repositoryRequest.GetAllRequestsAsync(status, pageNumber, pageSize);
                var response = new
                {
                    List = allRequestList,
                    TotalCount = totalCount
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG), Policy = PolicyStrings.VIEW_REQUEST)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RequestListDTO>>> GetRequestsByUserId([FromQuery] List<int> status, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null)
            {
                return Unauthorized();
            }
            var user = await _userManager.FindByEmailAsync(userEmail);
            try
            {
                var (allRequestList, totalCount) = await _repositoryRequest.GetAllRequestsByUserIdAsync(user.EmployeeCode, status, pageNumber, pageSize);
                var response = new
                {
                    List = allRequestList,
                    TotalCount = totalCount
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_REQUEST)]
        [HttpGet(UserConstant.USER_REQUESTID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RequestListDTO>> GetRequestByRequestId(Guid requestId)
        {
            if (requestId == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                var request = await _repositoryRequest.GetRequestByIdAsync(requestId);
                if (request == null)
                {
                    return NotFound();
                }
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG), Policy = PolicyStrings.CREATE_REQUEST)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRequest([FromBody] RequestCreateDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);
                if (userEmail == null)
                { return Unauthorized(); }
                var result = await _repositoryRequest.CreateRequestAsync(request, userEmail);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin), Policy = PolicyStrings.APPROVE_REQUEST)]
        [HttpPut(UserConstant.USER_REQUESTID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRequestStatus(Guid requestId, [FromBody] RequestStatusUpdateDTO requestUpdate)
        {
            try
            {
                if (requestId == Guid.Empty)
                    return BadRequest();

                var updatedRequest = await _repositoryRequest.ApproveOrRejectRequestAsync(requestId, requestUpdate);
                if (updatedRequest == false)
                    return StatusCode(500);

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_REQUEST)]
        [HttpDelete(UserConstant.USER_REQUESTID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRequest(Guid requestId)
        {
            try
            {
                var result = await _repositoryRequest.DeleteRequestAsync(requestId);
                if (result)
                {
                    return NoContent();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
