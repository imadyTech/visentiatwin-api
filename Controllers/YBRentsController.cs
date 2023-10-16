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
    public class YBRentsController : ControllerBase
    {
        private readonly YBRentContext _context;

        public YBRentsController(YBRentContext context)
        {
            _context = context;
        }

        // GET: api/YBRents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YBRent>> GetOrder(int id)
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

        // POST: api/list
        [HttpPost("list")]
        public async Task<ActionResult<IEnumerable<YBRent>>> ListOrders(PageRequest request)
        {
            if (_context.Rents == null)
            {
                return NotFound();
            }
            return await _context.Rents
                .Skip(request.PageNum * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }

        // PUT: api/YBRents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("approve")]
        public async Task<IActionResult> ApproveOrder(int id)
        {
            var yBRent = _context.Rents.Find(id);
            if (yBRent ==null || yBRent.Status != YB_RentalStatus.pending.ToString() )
            {
                return BadRequest();
            }
            yBRent.Status = YB_RentalStatus.approved.ToString();
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
        // PUT: api/YBRents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("reject")]
        public async Task<IActionResult> RejectOrder(int id)
        {
            var yBRent = _context.Rents.Find(id);
            if (yBRent == null || yBRent.Status != YB_RentalStatus.pending.ToString())
            {
                return BadRequest();
            }
            yBRent.Status = YB_RentalStatus.rejected.ToString();
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
        [HttpPost("placeorder")]
        public async Task<ActionResult<YBRent>> PlaceOrder(YBRent yBRent)
        {
          if (_context.Rents == null)
          {
              return Problem("Entity set 'YBRentContext.Rents'  is null.");
          }
            _context.Rents.Add(yBRent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYBRent", new { id = yBRent.Id }, yBRent);
        }

        //// DELETE: api/YBRents/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteYBRent(int id)
        //{
        //    if (_context.Rents == null)
        //    {
        //        return NotFound();
        //    }
        //    var yBRent = await _context.Rents.FindAsync(id);
        //    if (yBRent == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Rents.Remove(yBRent);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool YBRentExists(int id)
        {
            return (_context.Rents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
