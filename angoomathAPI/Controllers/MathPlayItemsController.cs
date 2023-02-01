using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using angoomathAPI.Context;
using angoomathAPI.DataModels;

namespace angoomathAPI.Controllers
{
    [Route("api/MathPlayItems")]
    [ApiController]
    public class MathPlayItemsController : ControllerBase
    {
        private readonly MathPlayContext _context;

        public MathPlayItemsController(MathPlayContext context)
        {
            _context = context;
        }

        // GET: api/MathPlayItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MathPlayItem>>> GetPlayItems()
        {
            return await _context.PlayItems.ToListAsync();
        }

        // GET: api/MathPlayItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MathPlayItem>> GetMathPlayItem(long id)
        {
            var mathPlayItem = await _context.PlayItems.FindAsync(id);

            if (mathPlayItem == null)
            {
                return NotFound();
            }

            return mathPlayItem;
        }

        // PUT: api/MathPlayItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMathPlayItem(long id, MathPlayItem mathPlayItem)
        {
            if (id != mathPlayItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(mathPlayItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MathPlayItemExists(id))
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

        // POST: api/MathPlayItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MathPlayItem>> PostMathPlayItem(MathPlayItem mathPlayItem)
        {
            _context.PlayItems.Add(mathPlayItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMathPlayItem", new { id = mathPlayItem.Id }, mathPlayItem);
        }

        // DELETE: api/MathPlayItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMathPlayItem(long id)
        {
            var mathPlayItem = await _context.PlayItems.FindAsync(id);
            if (mathPlayItem == null)
            {
                return NotFound();
            }

            _context.PlayItems.Remove(mathPlayItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MathPlayItemExists(long id)
        {
            return _context.PlayItems.Any(e => e.Id == id);
        }
    }
}
