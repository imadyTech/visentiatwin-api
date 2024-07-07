using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using VisentiaTwin_API.DataContexts;
using VisentiaTwin_API.DataModels;

namespace VisentiaTwin_API.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class VTComponentsController : ControllerBase
    {
        private readonly VTSystemContext _context;

        public VTComponentsController(VTSystemContext context)
        {
            _context = context;
        }

        [HttpPost("list")]
        public async Task<ActionResult<IEnumerable<VTComponent>>> ListVTComponents(PageRequest request)
        {
            if (_context.VTComponents == null)
            {
                return NotFound("Database is empty.");
            }
            return await _context.VTComponents
                .Skip(request.PageNum * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VTComponent>> GetVTComponent(int id)
        {
            var vtcomponent = await _context.VTComponents.FindAsync(id);
            if (vtcomponent == null)
            {
                return NotFound();
            }
            return vtcomponent;
        }

        [HttpPost("create")]
        public async Task<ActionResult<VTComponent>> CreateVTComponent(VTComponent vtcomponent)
        {
            _context.VTComponents.Add(vtcomponent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVTComponent), new { id = vtcomponent.VTComponentId }, vtcomponent);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateVTComponent(VTComponent vtcomponent)
        {
            _context.Entry(vtcomponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VTComponentExists(vtcomponent.VTComponentId))
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVTComponent(int id)
        {
            var vtcomponent = await _context.VTComponents.FindAsync(id);
            if (vtcomponent == null)
            {
                return NotFound();
            }

            _context.VTComponents.Remove(vtcomponent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VTComponentExists(int id)
        {
            return _context.VTComponents.Any(e => e.VTComponentId == id);
        }
    }
}