using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LabManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanGioTBController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuanGioTBController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QuanGioTB
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuanLyGioTB>>> GetAll()
        {
            return await _context.QuanLyGioTB
                .Include(q => q.PhieuDangKi)
                .Include(q => q.PhongThiNghiem)
                .Include(q => q.ThietBi)
                .ToListAsync();
        }

        // GET: api/QuanGioTB/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<QuanLyGioTB>> GetById(int id)
        {
            var quanLyGioTB = await _context.QuanLyGioTB
                .Include(q => q.PhieuDangKi)
                .Include(q => q.PhongThiNghiem)
                .Include(q => q.ThietBi)
                .FirstOrDefaultAsync(q => q.MaQuanLyTB == id);

            if (quanLyGioTB == null)
            {
                return NotFound();
            }

            return quanLyGioTB;
        }

        // POST: api/QuanGioTB
        [HttpPost]
        public async Task<ActionResult<QuanLyGioTB>> AddQuanLyGioTB(QuanLyGioTB quanLyGioTB)
        {
            _context.QuanLyGioTB.Add(quanLyGioTB);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = quanLyGioTB.MaQuanLyTB }, quanLyGioTB);
        }

        // PUT: api/QuanGioTB/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuanLyGioTB(int id, QuanLyGioTB quanLyGioTB)
        {
            if (id != quanLyGioTB.MaQuanLyTB)
            {
                return BadRequest();
            }

            _context.Entry(quanLyGioTB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuanLyGioTBExists(id))
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

        // DELETE: api/QuanGioTB/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuanLyGioTB(int id)
        {
            var quanLyGioTB = await _context.QuanLyGioTB.FindAsync(id);
            if (quanLyGioTB == null)
            {
                return NotFound();
            }

            _context.QuanLyGioTB.Remove(quanLyGioTB);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuanLyGioTBExists(int id)
        {
            return _context.QuanLyGioTB.Any(e => e.MaQuanLyTB == id);
        }
    }
}
