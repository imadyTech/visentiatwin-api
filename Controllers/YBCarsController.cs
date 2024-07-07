using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisentiaTwin_API.DataContexts;
using VisentiaTwin_API.DataModels;

namespace VisentiaTwin_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class YBCarsController : ControllerBase
    {
        private readonly YBCarContext _carContext;
        private readonly YBRentContext _orderContext;

        public YBCarsController(YBCarContext context, YBRentContext orderContext)
        {
            _carContext = context;
            _orderContext = orderContext;
        }

        // POST: api/list
        [HttpPost("list")]
        public async Task<ActionResult<IEnumerable<YBCar>>> GetCars(PageRequest request)
        {
            if (_carContext.Cars == null)
            {
                return NotFound("Database is empty.");
            }
            return await _carContext.Cars
                .Skip(request.PageNum * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }

        // GET: api/YBCars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YBCar>> GetCar(int id)
        {
            if (_carContext.Cars == null)
            {
                return NotFound("Database is empty.");
            }
            var yBCar = await _carContext.Cars.FindAsync(id);

            if (yBCar == null)
            {
                return NotFound("Car is not found.");
            }

            return Ok(yBCar);
        }

        // PUT: api/YBCars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCar(YBCar yBCar)
        {
            var existingCar = _carContext.Cars.FirstOrDefault(u => u.Id == yBCar.Id);
            if (existingCar == null)
            {
                return BadRequest("Car is not exist.");
            }

            // Update the existingUser entity with the values from yBUser
            _carContext.Entry(existingCar).CurrentValues.SetValues(yBCar);

            try
            {
                await _carContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!YBCarExists(yBCar.Id))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict(e.Message);
                }
            }

            return Ok(yBCar);
        }

        // POST: api/YBCars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("add")]
        public async Task<ActionResult<YBCar>> AddCar(YBCar yBCar)
        {
            if (_carContext.Cars == null)
            {
                return Problem("Entity set 'YBCarContext.Cars'  is null.");
            }
            Random random = new Random();
            yBCar.Id = _carContext.Cars.Max(u => u.Id) + random.Next(1, 101);
            _carContext.Cars.Add(yBCar);
            await _carContext.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = yBCar.Id }, yBCar);
        }

        // DELETE: api/YBCars/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (_carContext.Cars == null)
            {
                return NotFound();
            }
            var yBCar = await _carContext.Cars.FindAsync(id);
            if (yBCar == null)
            {
                return NotFound();
            }
            if (_orderContext.Rents.Any(o => o.CarId == id))
                return Forbid("Order(s) link to this car exist.");

            _carContext.Cars.Remove(yBCar);
            await _carContext.SaveChangesAsync();

            return Ok();
        }

        private bool YBCarExists(int id)
        {
            return (_carContext.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
