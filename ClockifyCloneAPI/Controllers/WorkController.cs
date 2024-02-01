using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.Work;
using ClockifyCloneAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClockifyCloneAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WorkController : ControllerBase
{
    private readonly IWorkService _workService;

    public WorkController(IWorkService workService)
    {
        _workService = workService;
    }

    [HttpGet]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<IEnumerable<GetAllWorkResponse>>> GetWorks()
    {
        try
        {
            var works = await _workService.GetAll(User);
            return Ok(works);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<IActionResult> PutWork(int id, UpdateWorkRequest request)
    {
        try
        {
            var message = await _workService.Update(id, request);
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
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<string>> PostWork(PostWorkRequest request)
    {
        try
        {
            var message = await _workService.Create(request);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<IActionResult> DeleteWork(int id)
    {
        try
        {
            var message = await _workService.Delete(id);
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
    public async Task<IActionResult> GetWork(int id)
    {
        try
        {
            var work = await _workService.Get(id);
            return Ok(work);
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
