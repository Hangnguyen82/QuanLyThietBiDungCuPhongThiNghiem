using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;
using LabManagement.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiThietBiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoaiThietBiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LoaiThietBi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiThietBi>>> GetLoaiThietBis()
        {
            return await _context.LoaiThietBi.ToListAsync();
        }

        // GET: api/LoaiThietBi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoaiThietBi>> GetLoaiThietBi(string id)
        {
            var loaiThietBi = await _context.LoaiThietBi.FindAsync(id);

            if (loaiThietBi == null)
            {
                return NotFound();
            }

            return loaiThietBi;
        }

        // POST: api/LoaiThietBi
        [HttpPost]
        public async Task<ActionResult<LoaiThietBi>> PostLoaiThietBi(LoaiThietBi loaiThietBi)
        {
            _context.LoaiThietBi.Add(loaiThietBi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLoaiThietBi), new { id = loaiThietBi.MaLoaiThietBi }, loaiThietBi);
        }

        // PUT: api/LoaiThietBi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoaiThietBi(string id, LoaiThietBi loaiThietBi)
        {
            if (id != loaiThietBi.MaLoaiThietBi)
            {
                return BadRequest();
            }

            _context.Entry(loaiThietBi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiThietBiExists(id))
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

        // DELETE: api/LoaiThietBi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoaiThietBi(string id)
        {
            var loaiThietBi = await _context.LoaiThietBi.FindAsync(id);
            if (loaiThietBi == null)
            {
                return NotFound();
            }

            _context.LoaiThietBi.Remove(loaiThietBi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoaiThietBiExists(string id)
        {
            return _context.LoaiThietBi.Any(e => e.MaLoaiThietBi == id);
        }
    }
}
