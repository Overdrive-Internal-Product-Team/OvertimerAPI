using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.Project;
using ClockifyCloneAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClockifyCloneAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GetAllProjectResponse>>> GetProjects()
    {
        try
        {
            var projects = await _projectService.GetAll();
            return Ok(projects);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> PatchProject(int id, UpdateProjectRequest request)
    {
        try
        {
            var message = await _projectService.Update(id, request);
            return Ok(message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<string>> PostProject(PostProjectRequest request)
    {
        try
        {
            var message = await _projectService.Create(request);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        try
        {
            var message = await _projectService.Delete(id);
            return Ok(message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetProject(int id)
    {
        try
        {
            var message = await _projectService.Get(id);
            return Ok(message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

