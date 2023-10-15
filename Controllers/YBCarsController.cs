using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YBCarRental3D_API.DataContexts;
using YBCarRental3D_API.DataModels;

namespace YBCarRental3D_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class YBCarsController : ControllerBase
    {
        private readonly YBCarContext _context;

        public YBCarsController(YBCarContext context)
        {
            _context = context;
        }

        // POST: api/list
        [HttpPost("list")]
        public async Task<ActionResult<IEnumerable<YBCar>>> GetCars(PageRequest request)
        {
          if (_context.Cars == null)
          {
              return NotFound();
          }
            return await _context.Cars
                .Skip(request.PageNum* request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }

        // GET: api/YBCars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YBCar>> GetYBCar(int id)
        {
          if (_context.Cars == null)
          {
              return NotFound();
          }
            var yBCar = await _context.Cars.FindAsync(id);

            if (yBCar == null)
            {
                return NotFound();
            }

            return yBCar;
        }

        // PUT: api/YBCars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYBCar(int id, YBCar yBCar)
        {
            if (id != yBCar.Id)
            {
                return BadRequest();
            }

            _context.Entry(yBCar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YBCarExists(id))
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

        // POST: api/YBCars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<YBCar>> PostYBCar(YBCar yBCar)
        {
          if (_context.Cars == null)
          {
              return Problem("Entity set 'YBCarContext.Cars'  is null.");
          }
            _context.Cars.Add(yBCar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYBCar", new { id = yBCar.Id }, yBCar);
        }

        // DELETE: api/YBCars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYBCar(int id)
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
            var yBCar = await _context.Cars.FindAsync(id);
            if (yBCar == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(yBCar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool YBCarExists(int id)
        {
            return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
