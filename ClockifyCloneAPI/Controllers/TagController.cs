﻿using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.Tag;
using ClockifyCloneAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClockifyCloneAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService TagService)
    {
        _tagService = TagService;
    }

    // GET: api/Tag
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GetAllTagResponse>>> GetTags()
    {
        try
        {
            var tags = await _tagService.GetAll();
            return Ok(tags);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Tag/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> PutTag(int id, UpdateTagRequest request)
    {
        try
        {
            var message = await _tagService.Update(id, request);
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

    // POST: api/Tag
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<string>> PostTag(PostTagRequest request)
    {
        try
        {
            var message = await _tagService.Create(request);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Tag/5
    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        try
        {
            var message = await _tagService.Delete(id);
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
    public async Task<IActionResult> GetTag(int id)
    {
        try
        {
            var tag = await _tagService.Get(id);
            return Ok(tag);
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

