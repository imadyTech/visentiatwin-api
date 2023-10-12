using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YBCarRental3D_API.DataContexts;
using YBCarRental3D_API.DataModels;

namespace YBCarRental3D_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YBRentsController : ControllerBase
    {
        private readonly YBRentContext _context;

        public YBRentsController(YBRentContext context)
        {
            _context = context;
        }

        // GET: api/YBRents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YBRent>>> GetRents()
        {
          if (_context.Rents == null)
          {
              return NotFound();
          }
            return await _context.Rents.ToListAsync();
        }

        // GET: api/YBRents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YBRent>> GetYBRent(int id)
        {
          if (_context.Rents == null)
          {
              return NotFound();
          }
            var yBRent = await _context.Rents.FindAsync(id);

            if (yBRent == null)
            {
                return NotFound();
            }

            return yBRent;
        }

        // PUT: api/YBRents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYBRent(int id, YBRent yBRent)
        {
            if (id != yBRent.Id)
            {
                return BadRequest();
            }

            _context.Entry(yBRent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YBRentExists(id))
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

        // POST: api/YBRents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<YBRent>> PostYBRent(YBRent yBRent)
        {
          if (_context.Rents == null)
          {
              return Problem("Entity set 'YBRentContext.Rents'  is null.");
          }
            _context.Rents.Add(yBRent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYBRent", new { id = yBRent.Id }, yBRent);
        }

        // DELETE: api/YBRents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYBRent(int id)
        {
            if (_context.Rents == null)
            {
                return NotFound();
            }
            var yBRent = await _context.Rents.FindAsync(id);
            if (yBRent == null)
            {
                return NotFound();
            }

            _context.Rents.Remove(yBRent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool YBRentExists(int id)
        {
            return (_context.Rents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
