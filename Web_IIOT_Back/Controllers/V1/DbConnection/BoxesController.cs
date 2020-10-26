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
    public class BoxesController : ControllerBase
    {
        private readonly BoxContext _context;

        public BoxesController(BoxContext context)
        {
            _context = context;
        }

        // GET: api/V1/Db/Boxes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Box>>> GetTodoItems()
        {
            return await _context.Boxes.ToListAsync();
        }

        // GET: api/V1/Db/Boxes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Box>> GetBox(int id)
        {
            var box = await _context.Boxes.FindAsync(id);

            if (box == null)
            {
                return NotFound();
            }

            return box;
        }

        // PUT: api/V1/Db/Boxes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBox(int id, Box box)
        {
            if (id != box.Id)
            {
                return BadRequest();
            }

            _context.Entry(box).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoxExists(id))
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

        // POST: api/V1/Db/Boxes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Box>> PostBox(Box box)
        {
            _context.Boxes.Add(box);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBox", new { id = box.Id }, box);
        }

        // DELETE: api/V1/Db/Boxes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Box>> DeleteBox(int id)
        {
            var box = await _context.Boxes.FindAsync(id);
            if (box == null)
            {
                return NotFound();
            }

            _context.Boxes.Remove(box);
            await _context.SaveChangesAsync();

            return box;
        }

        private bool BoxExists(int id)
        {
            return _context.Boxes.Any(e => e.Id == id);
        }
    }
}
