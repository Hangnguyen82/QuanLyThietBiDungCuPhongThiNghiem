using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuThanhLyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhieuThanhLyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var phieuThanhLys = await _context.PhieuThanhLy.ToListAsync();
            return Ok(phieuThanhLys);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var phieuThanhLy = await _context.PhieuThanhLy.FindAsync(id);
            if (phieuThanhLy == null)
            {
                return NotFound();
            }
            return Ok(phieuThanhLy);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhieuThanhLy phieuThanhLy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PhieuThanhLy.Add(phieuThanhLy);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = phieuThanhLy.MaPhieuTL }, phieuThanhLy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PhieuThanhLy updatedPhieuThanhLy)
        {
            if (id != updatedPhieuThanhLy.MaPhieuTL)
            {
                return BadRequest();
            }

            _context.Entry(updatedPhieuThanhLy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PhieuThanhLy.Any(e => e.MaPhieuTL == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }
        [HttpGet("existing")]
        public async Task<IActionResult> GetExistingPhieuThanhLy()
        {
            var phieuThanhLys = await _context.PhieuThanhLy.Select(p => new { p.MaPhieuTL }).ToListAsync();
            return Ok(phieuThanhLys);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var phieuThanhLy = await _context.PhieuThanhLy.FindAsync(id);
            if (phieuThanhLy == null)
            {
                return NotFound();
            }

            _context.PhieuThanhLy.Remove(phieuThanhLy);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
