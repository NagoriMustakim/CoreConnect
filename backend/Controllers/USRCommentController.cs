using LinkwayAPI.Constants.API;
using LinkwayAPI.Constants.Permission;
using LinkwayAPI.DTOs.Comment;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkwayAPI.Controllers
{
    [Authorize]
    [Route(UserConstant.USER_COMMENTS)]
    [ApiController]
    public class USRCommentController : ControllerBase
    {
        private readonly ICommentRepository _repositoryComment;

        public USRCommentController(ICommentRepository repositoryComment)
        {
            _repositoryComment = repositoryComment;
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Admin), Policy = PolicyStrings.VIEW_USER_CERTIFICATE)]
        [HttpGet(UserConstant.USERID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCommentByUserId(string userId)
        {
            try
            {
                var commentsList = await _repositoryComment.GetAllCommentsAsync(userId);
                return Ok(commentsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Admin), Policy = PolicyStrings.CREATE_USER_COMMENT)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddComment([FromBody] CommentCreateDTO dtoCreateComment)
        {
            try
            {
                var commenterId = User.FindFirst(ClaimTypes.Email)?.Value;

                var result = await _repositoryComment.CreateCommentAsync(dtoCreateComment.UserId, commenterId, dtoCreateComment.Comment);
                if (!result)
                    return StatusCode(500);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Admin), Policy = PolicyStrings.EDIT_USER_COMMENT)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditComment([FromBody] CommentEditDTO dtoCommentEdit)
        {
            try
            {
                var result = await _repositoryComment.EditCommentAsync(dtoCommentEdit);
                if (!result)
                    return StatusCode(500);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + UserConstant.COMMA + nameof(RoleTypes.Manager) + UserConstant.COMMA + nameof(RoleTypes.RMG) + UserConstant.COMMA + nameof(RoleTypes.Admin), Policy = PolicyStrings.DELETE_USER_COMMENT)]
        [HttpDelete(UserConstant.USER_COMMENTID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            try
            {
                var result = await _repositoryComment.DeleteCommentAsync(commentId);
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
