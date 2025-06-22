using LinkwayAPI.DTOs.Project;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _repositoryProject;
        public ProjectController(IProjectRepository repositoryProject)
        {
            _repositoryProject = repositoryProject;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProjects([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var (result, totalCount) = await _repositoryProject.GetAllProjectsAsync(pageNumber, pageSize);
                return Ok(new { list = result, count = totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProject([FromBody] ProjectAddDTO dtoProjectAdd)
        {
            try
            {
                if (await _repositoryProject.DoesProjectExists(dtoProjectAdd.ProjectTitle, null))
                    return Conflict();
                ProjectViewDTO result = await _repositoryProject.CreateProjectAsync(dtoProjectAdd);
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
        public async Task<IActionResult> UpdateProject([FromBody] ProjectEditDTO dtoProjectEdit)
        {
            try
            {
                if (await _repositoryProject.DoesProjectExists(dtoProjectEdit.ProjectTitle, dtoProjectEdit.ProjectGuid))
                    return Conflict();
                ProjectViewDTO result = await _repositoryProject.UpdateProjectAsync(dtoProjectEdit);
                if(result == null)
                    return StatusCode(500);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [HttpDelete("{projectGuid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDesignation(Guid projectGuid)
        {
            bool result = await _repositoryProject.DeleteProjectAsync(projectGuid);

            if (result == false)
                return StatusCode(500);

            return NoContent();
        }
    }
}
