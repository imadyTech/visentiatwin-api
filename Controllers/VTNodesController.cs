using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisentiaTwin_API.DataContexts;
using VisentiaTwin_API.DataModels;


namespace VisentiaTwin_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VTNodesController : ControllerBase
    {
        private readonly VTSystemContext _context;

        public VTNodesController(VTSystemContext context)
        {
            _context = context;
        }

        [HttpPost("list")]
        public async Task<ActionResult<IEnumerable<VTNode>>> ListVTNodes(PageRequest request)
        {
            var nodes = await _context.VTNodes
                .Include(n => n.VTNodeComponents)
                .ThenInclude(c => c.VTComponent)
                .Skip(request.PageNum * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            if (nodes == null) {
                return NotFound();
            }
            return Ok(nodes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VTNode>> GetVTNode(int id)
        {
            var vtnode = await _context.VTNodes
                .Include(n => n.VTNodeComponents)
                .FirstOrDefaultAsync(n => n.VTNodeId == id);
            if (vtnode == null)
            {
                return NotFound();
            }
            return vtnode;
        }

        [HttpPost("addnode")]
        public async Task<ActionResult<VTNode>> AddVtNode([FromBody] VTNode node)
        {
            var vtSystem = _context.VTSystems.Find(node.VTSystemId);
            if (vtSystem == null)
            {
                return NotFound("The VTSystem designated in the VTNode is not found.");
            }
            node.VTSystem = vtSystem; // Ensure the relationship is established
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        node.VTSystem = vtSystem; // Ensure the relationship is established

                        await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.VTNodes ON");

                        _context.VTNodes.Add(node);
                        await _context.SaveChangesAsync();

                        await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.VTNodes OFF");

                        await transaction.CommitAsync();

                        return CreatedAtAction(nameof(GetVTNode), "VTNodes", new { id = node.VTNodeId }, node);
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateVTNode(int id, VTNode vtnode)
        {
            if (id != vtnode.VTNodeId)
            {
                return BadRequest();
            }

            _context.Entry(vtnode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VTNodeExists(id))
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
        public async Task<IActionResult> DeleteVTNode(int id)
        {
            var vtnode = await _context.VTNodes.FindAsync(id);
            if (vtnode == null)
            {
                return NotFound();
            }

            _context.VTNodes.Remove(vtnode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VTNodeExists(int id)
        {
            return _context.VTNodes.Any(e => e.VTNodeId == id);
        }
    }
}