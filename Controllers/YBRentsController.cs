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
    public class YBRentsController : ControllerBase
    {
        private readonly YBRentContext  _ordercontext;
        private readonly YBUserContext  _usercontext;
        private readonly YBCarContext  _carcontext;

        public YBRentsController(YBRentContext context,YBUserContext usercontext, YBCarContext carcontext)
        {
            _ordercontext = context;
            _usercontext = usercontext;
            _carcontext = carcontext;
        }

        // GET: api/YBRents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YBRent>> GetOrder(int id)
        {
          if (_ordercontext.Rents == null)
          {
              return NotFound();
          }
            var yBRent = await _ordercontext.Rents.FindAsync(id);

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
            if (_ordercontext.Rents == null)
            {
                return NotFound();
            }
            return await _ordercontext.Rents
                .Skip(request.PageNum * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }

        // PUT: api/YBRents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("approve/{id}")]
        public async Task<IActionResult> ApproveOrder(int id)
        {
            var existingOrder = _ordercontext.Rents.Find(id);
            if (existingOrder ==null || existingOrder.Status != YB_RentalStatus.pending.ToString() )
            {
                return BadRequest();
            }
            existingOrder.Status = YB_RentalStatus.approved.ToString();
            _ordercontext.Entry(existingOrder).State = EntityState.Modified;

            var user = _usercontext.Users.FirstOrDefault(u=>u.Id==existingOrder.UserId);
            var car  = _carcontext.Cars.FirstOrDefault(c=>c.Id==existingOrder.CarId);
            user.Balance -= car.DayRentPrice * existingOrder.RentDays;
            _usercontext.Entry(user).State = EntityState.Modified;

            try
            {
                await _ordercontext.SaveChangesAsync();
                await _usercontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!YBRentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict(e.Message);
                }
            }
            return Ok();
        }
        // PUT: api/YBRents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("reject/{id}")]
        public async Task<IActionResult> RejectOrder(int id)
        {
            var yBRent = _ordercontext.Rents.Find(id);
            if (yBRent == null || yBRent.Status != YB_RentalStatus.pending.ToString())
            {
                return BadRequest();
            }
            yBRent.Status = YB_RentalStatus.rejected.ToString();
            _ordercontext.Entry(yBRent).State = EntityState.Modified;

            try
            {
                await _ordercontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!YBRentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict(e.Message);
                }
            }
            return Ok();
        }

        // POST: api/YBRents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("add")]
        public async Task<ActionResult<YBRent>> PlaceOrder(YBRent yBRent)
        {
          if (_ordercontext.Rents == null)
          {
              return Problem("Entity set 'YBRentContext.Rents'  is null.");
          }
            Random random = new Random();
            yBRent.Id = _ordercontext.Rents.Max(o => o.Id) + random.Next(1, 101);

            _ordercontext.Rents.Add(yBRent);
            await _ordercontext.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = yBRent.Id }, yBRent);
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
            return (_ordercontext.Rents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
