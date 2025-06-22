using LinkwayAPI.DTOs.Skill;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkwayAPI.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillRepository _repositorySkill;

        public SkillController(ISkillRepository repositorySkill)
        {
            _repositorySkill = repositorySkill;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSkills([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var (result, tolalCount) = await _repositorySkill.GetAllSkillsAsync(pageNumber, pageSize);
                if (result == null)
                    return NotFound();
                return Ok(new { List = result, count = tolalCount });
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
        public async Task<IActionResult> CreateSkill([FromBody] SkillCreateUpdateDTO dtoSkillCreateUpdate)
        {
            try
            {
                if (await _repositorySkill.IsSkillExists(dtoSkillCreateUpdate.SkillTitle, null))
                    return Conflict();

                var result = await _repositorySkill.CreateSkillAsync(dtoSkillCreateUpdate);
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
        [HttpPut("update/{skillGuid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SkillListDTO>> UpdateSkill([FromBody] SkillCreateUpdateDTO dtoSkillCreateUpdate)
        {
            try
            {
                if (await _repositorySkill.IsSkillExists(dtoSkillCreateUpdate.SkillTitle, dtoSkillCreateUpdate.SkillGuid))
                    return Conflict();

                var result = await _repositorySkill.UpdateSkillAsync(dtoSkillCreateUpdate);
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
        [HttpDelete("delete/{skillGuid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSkill(Guid skillGuid)
        {
            try
            {
                var result = await _repositorySkill.DeleteSkillAsync(skillGuid);

                if (result == false)
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
