using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.User;
using ClockifyCloneAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClockifyCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(ClockifyCloneDbContext context, IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetUsers()
        {
            try
            {
                var companies = await _userService.GetAll();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEntity>> GetUserEntity(int id)
        {
            try
            {
                var company = await _userService.Get(id);
                return Ok(company);
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

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            try
            {
                var message = await _userService.Update(id, request);
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostUser(PostUserRequest request)
        {
            try
            {
                var message = await _userService.Create(request);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
    }
}
