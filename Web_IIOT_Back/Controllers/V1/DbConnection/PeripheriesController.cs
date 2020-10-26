using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_IIOT_Back.Models;

namespace Web_IIOT_Back.Controllers.V1.DbConnection
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/V1/Db/[controller]")]
    [ApiController]
    public class PeripheriesController : ControllerBase
    {
        private readonly PeripheryContext _context;

        public PeripheriesController(PeripheryContext context)
        {
            _context = context;
        }

        // GET: api/V1/Db/Peripheries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Periphery>>> GetTodoItems()
        {
            return await _context.Peripheries.ToListAsync();
        }

        // GET: api/V1/Db/Peripheries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Periphery>> GetPeriphery(int id)
        {
            var periphery = await _context.Peripheries.FindAsync(id);

            if (periphery == null)
            {
                return NotFound();
            }

            return periphery;
        }

        // PUT: api/V1/Db/Peripheries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeriphery(int id, Periphery periphery)
        {
            if (id != periphery.Id)
            {
                return BadRequest();
            }

            _context.Entry(periphery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeripheryExists(id))
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

        // POST: api/V1/Db/Peripheries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Periphery>> PostPeriphery(Periphery periphery)
        {
            _context.Peripheries.Add(periphery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeriphery", new { id = periphery.Id }, periphery);
        }

        // DELETE: api/V1/Db/Peripheries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Periphery>> DeletePeriphery(int id)
        {
            var periphery = await _context.Peripheries.FindAsync(id);
            if (periphery == null)
            {
                return NotFound();
            }

            _context.Peripheries.Remove(periphery);
            await _context.SaveChangesAsync();

            return periphery;
        }

        private bool PeripheryExists(int id)
        {
            return _context.Peripheries.Any(e => e.Id == id);
        }
    }
}
