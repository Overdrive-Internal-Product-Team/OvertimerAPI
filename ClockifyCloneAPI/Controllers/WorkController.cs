using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClockifyCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly ClockifyCloneDbContext _context;

        public WorkController(ClockifyCloneDbContext context)
        {
            _context = context;
        }

        // GET: api/Work
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkEntity>>> GetWorks()
        {
            return await _context.Works.ToListAsync();
        }

        // GET: api/Work/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkEntity>> GetWorkEntity(int id)
        {
            var workEntity = await _context.Works.FindAsync(id);

            if (workEntity == null)
            {
                return NotFound();
            }

            return workEntity;
        }

        // PUT: api/Work/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkEntity(int id, WorkEntity workEntity)
        {
            if (id != workEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(workEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Work
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkEntity>> PostWorkEntity(WorkEntity workEntity)
        {
            _context.Works.Add(workEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkEntity", new { id = workEntity.Id }, workEntity);
        }

        // DELETE: api/Work/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkEntity(int id)
        {
            var workEntity = await _context.Works.FindAsync(id);
            if (workEntity == null)
            {
                return NotFound();
            }

            _context.Works.Remove(workEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkEntityExists(int id)
        {
            return _context.Works.Any(e => e.Id == id);
        }
    }
}
