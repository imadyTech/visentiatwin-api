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
    public class VTSystemsController : ControllerBase
    {
        private readonly VTSystemContext _context;

        public VTSystemsController(VTSystemContext context)
        {
            _context = context;
        }

        [HttpPost("list")]
        public async Task<ActionResult<IEnumerable<VTSystem>>> ListVTSystems(PageRequest request)
        {
            if (_context.VTSystems== null)
            {
                return NotFound("Database is empty.");
            }
            var systems = await _context.VTSystems
                .Include(s => s.VTNodes)
                .ThenInclude(nc => nc.VTNodeComponents)
                .ThenInclude(nc => nc.VTComponent)
                .Skip(request.PageNum * request.PageSize)
                .Take(request.PageSize)
                .Select(s => new
                {
                    s.SystemId,
                    s.Name,
                    s.Description,
                    s.Version,
                    s.Author,
                    s.estimatorString,
                    VTNodes = s.VTNodes.Select(n => new
                    {
                        n.VTNodeId,
                        n.Name,
                        n.Description,
                        n.Version,
                        n.Author,
                        n.VTSystemId,
                        VTNodeComponents = n.VTNodeComponents.Select(nc => new
                        {
                            nc.VTNodeId,
                            nc.VTComponentId,
                            nc.isSelected,
                            nc.posX,
                            nc.posY,
                            nc.posZ,
                            nc.sclX,
                            nc.sclY,
                            nc.sclZ,
                            nc.rotX,
                            nc.rotY,
                            nc.rotZ,
                            VTComponent = new
                            {
                                nc.VTComponent.VTComponentId,
                                nc.VTComponent.Name,
                                nc.VTComponent.Description,
                                nc.VTComponent.Version,
                                nc.VTComponent.Author,
                                nc.VTComponent.Cost,
                                nc.VTComponent.modelId,
                                nc.VTComponent.estimatorString,
                                // Do not include VTNodeComponents here
                            }
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
            return Ok(systems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VTSystem>> GetVTSystem(int id)
        {
            var vtsystem = await _context.VTSystems.FindAsync(id);
            if (vtsystem == null)
            {
                return NotFound();
            }
            return vtsystem;
        }

        [HttpPost("create")]
        public async Task<ActionResult<VTSystem>> CreateVTSystem(VTSystem vtsystem)
        {
            _context.VTSystems.Add(vtsystem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVTSystem), new { id = vtsystem.SystemId }, vtsystem);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateVTSystem(int id, VTSystem vtsystem)
        {
            if (id != vtsystem.SystemId)
            {
                return BadRequest();
            }

            _context.Entry(vtsystem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VTSystemExists(id))
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
        public async Task<IActionResult> DeleteVTSystem(int id)
        {
            var vtsystem = await _context.VTSystems.FindAsync(id);
            if (vtsystem == null)
            {
                return NotFound();
            }

            _context.VTSystems.Remove(vtsystem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VTSystemExists(int id)
        {
            return _context.VTSystems.Any(e => e.SystemId == id);
        }
    }

}