using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuDeXuatLuanChuyenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhieuDeXuatLuanChuyenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PhieuDeXuatLuanChuyen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhieuDeXuatLuanChuyen>>> GetPhieuDeXuatLuanChuyen()
        {
            return await _context.PhieuDeXuatLuanChuyen.ToListAsync();
        }

        // GET: api/PhieuDeXuatLuanChuyen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhieuDeXuatLuanChuyen>> GetPhieuDeXuatLuanChuyen(string id)
        {
            var phieuDeXuatLuanChuyen = await _context.PhieuDeXuatLuanChuyen.FindAsync(id);

            if (phieuDeXuatLuanChuyen == null)
            {
                return NotFound();
            }

            return phieuDeXuatLuanChuyen;
        }

        // PUT: api/PhieuDeXuatLuanChuyen/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhieuDeXuatLuanChuyen(string id, [FromBody] PhieuDeXuatLuanChuyen phieuDeXuatLuanChuyen)
        {
            if (id != phieuDeXuatLuanChuyen.MaPhieuLC)
            {
                return BadRequest();
            }

            // Tìm phiếu trong cơ sở dữ liệu
            var existingPhieu = await _context.PhieuDeXuatLuanChuyen.FindAsync(id);
            if (existingPhieu == null)
            {
                return NotFound();
            }

            // Cập nhật trạng thái của phiếu
            existingPhieu.TrangThai = phieuDeXuatLuanChuyen.TrangThai;

            // Cập nhật ngày luân chuyển và ngày hoàn tất
            existingPhieu.NgayLuanChuyen = phieuDeXuatLuanChuyen.NgayLuanChuyen;
            existingPhieu.NgayHoanTat = phieuDeXuatLuanChuyen.NgayHoanTat;

            // Cập nhật các trường khác nếu cần (nếu không, giữ nguyên)
            _context.Entry(existingPhieu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhieuDeXuatLuanChuyenExists(id))
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


        // POST: api/PhieuDeXuatLuanChuyen
        [HttpPost]
        public async Task<ActionResult<PhieuDeXuatLuanChuyen>> PostPhieuDeXuatLuanChuyen(PhieuDeXuatLuanChuyen phieuDeXuatLuanChuyen)
        {
            _context.PhieuDeXuatLuanChuyen.Add(phieuDeXuatLuanChuyen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhieuDeXuatLuanChuyen", new { id = phieuDeXuatLuanChuyen.MaPhieuLC }, phieuDeXuatLuanChuyen);
        }

        // DELETE: api/PhieuDeXuatLuanChuyen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhieuDeXuatLuanChuyen(string id)
        {
            var phieuDeXuatLuanChuyen = await _context.PhieuDeXuatLuanChuyen.FindAsync(id);
            if (phieuDeXuatLuanChuyen == null)
            {
                return NotFound();
            }

            _context.PhieuDeXuatLuanChuyen.Remove(phieuDeXuatLuanChuyen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhieuDeXuatLuanChuyenExists(string id)
        {
            return _context.PhieuDeXuatLuanChuyen.Any(e => e.MaPhieuLC == id);
        }
        // GET: api/PhieuDeXuatLuanChuyen/existing
        [HttpGet("existing")]
        public async Task<IActionResult> GetExistingPhieuLuanChuyen()
        {
            var phieuLuanChuyens = await _context.PhieuDeXuatLuanChuyen.Select(p => new { p.MaPhieuLC }).ToListAsync();
            return Ok(phieuLuanChuyens);
        }


    }
}
