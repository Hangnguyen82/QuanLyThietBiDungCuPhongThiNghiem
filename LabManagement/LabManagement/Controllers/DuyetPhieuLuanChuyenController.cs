using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuyetPhieuLuanChuyenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DuyetPhieuLuanChuyenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DuyetPhieuLuanChuyen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DuyetPhieuLuanChuyen>>> GetDuyetPhieuLuanChuyen()
        {
            return await _context.DuyetPhieuLuanChuyen.ToListAsync();
        }

        // GET: api/DuyetPhieuLuanChuyen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DuyetPhieuLuanChuyen>> GetDuyetPhieuLuanChuyen(string id)
        {
            var duyetPhieuLuanChuyen = await _context.DuyetPhieuLuanChuyen.FindAsync(id);

            if (duyetPhieuLuanChuyen == null)
            {
                return NotFound();
            }

            return duyetPhieuLuanChuyen;
        }

        // POST: api/DuyetPhieuLuanChuyen
        [HttpPost]
        public async Task<ActionResult<DuyetPhieuLuanChuyen>> PostDuyetPhieuLuanChuyen(DuyetPhieuLuanChuyen duyetPhieuLuanChuyen)
        {
            _context.DuyetPhieuLuanChuyen.Add(duyetPhieuLuanChuyen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDuyetPhieuLuanChuyen", new { id = duyetPhieuLuanChuyen.MaPhieuLC }, duyetPhieuLuanChuyen);
        }

        // DELETE: api/DuyetPhieuLuanChuyen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDuyetPhieuLuanChuyen(string id)
        {
            var duyetPhieuLuanChuyen = await _context.DuyetPhieuLuanChuyen.FindAsync(id);
            if (duyetPhieuLuanChuyen == null)
            {
                return NotFound();
            }

            _context.DuyetPhieuLuanChuyen.Remove(duyetPhieuLuanChuyen);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
