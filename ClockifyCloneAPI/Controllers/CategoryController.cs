using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.Category;
using ClockifyCloneAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClockifyCloneAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: api/Category
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GetAllCategoryResponse>>> GetCategories()
    {
        try
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/Category/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<GetCategoryResponse>> GetCategory(int id)
    {
        try
        {
            var category = await _categoryService.Get(id);
            return Ok(category);
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

    // PUT: api/Category/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPatch("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> PutCategory(int id, UpdateCategoryRequest request)
    {
        try
        {
            var message = await _categoryService.Update(id, request);
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

    // POST: api/Category
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<string>> PostCategory(PostCategoryRequest request)
    {
        try
        {
            var message = await _categoryService.Create(request);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Category/5
    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var message = await _categoryService.Delete(id);
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

