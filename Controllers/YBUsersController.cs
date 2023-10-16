using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using YBCarRental3D_API.DataContexts;
using YBCarRental3D_API.DataModels;

namespace YBCarRental3D_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class YBUsersController : ControllerBase
    {
        private readonly YBUserContext _context; 

        public YBUsersController(YBUserContext context)
        {
            _context = context;
        }

        // GET: api/YBUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YBUser>> GetUser(int id)
        {
            //github deploy test
            if (_context.Users == null)
            {
                return NotFound();
            }
            var yBUser = await _context.Users.FindAsync(id);

            if (yBUser == null)
            {
                return NotFound();
            }

            return yBUser;
        }


        // GET: api/YBUsers/5
        [HttpPost("login")]
        public async Task<ActionResult<YBUser>> UserLogin(LoginRequest loginRequest)
        {
            Console.WriteLine($"attempt of login:{loginRequest.UserName}:{loginRequest.Password}");
            if (loginRequest == null) return BadRequest("Invalid login request.");

            var yBUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginRequest.UserName);
            if (yBUser == null) return NotFound("User not found.");

            if (loginRequest.Password != yBUser.Password)
                return Unauthorized("Invalid password.");

            yBUser.LoginStatus = true;
            _context.Entry(yBUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YBUserExists(yBUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(yBUser);
        }

        // PUT: api/YBUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(YBUser yBUser)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == yBUser.Id);
            if (existingUser == null)
            {
                return BadRequest();
            }

            _context.Entry(yBUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YBUserExists(yBUser.Id))
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

        // POST: api/YBUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<ActionResult<YBUser>> UserRegister(YBUser yBUser)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'YBUserContext.User'  is null.");
            }
            _context.Users.Add(yBUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYBUser", new { id = yBUser.Id }, yBUser);
        }

        //// DELETE: api/YBUsers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteYBUser(int id)
        //{
        //    if (_context.Users == null)
        //    {
        //        return NotFound();
        //    }
        //    var yBUser = await _context.Users.FindAsync(id);
        //    if (yBUser == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(yBUser);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool YBUserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
