using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietLuanChuyenDCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChiTietLuanChuyenDCController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChiTietLuanChuyenDC
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietLuanChuyenDC>>> GetChiTietLuanChuyenDC()
        {
            return await _context.ChiTietLuanChuyenDC.ToListAsync();
        }

        // GET: api/ChiTietLuanChuyenDC/5/5
        [HttpGet("{maPhieuLC}/{maDungCu}")]
        public async Task<ActionResult<ChiTietLuanChuyenDC>> GetChiTietLuanChuyenDC(string maPhieuLC, string maDungCu)
        {
            var chiTietLuanChuyenDC = await _context.ChiTietLuanChuyenDC.FindAsync(maPhieuLC, maDungCu);

            if (chiTietLuanChuyenDC == null)
            {
                return NotFound();
            }

            return chiTietLuanChuyenDC;
        }

        // POST: api/ChiTietLuanChuyenDC
        [HttpPost]
        public async Task<ActionResult<ChiTietLuanChuyenDC>> PostChiTietLuanChuyenDC(ChiTietLuanChuyenDC chiTietLuanChuyenDC)
        {
            _context.ChiTietLuanChuyenDC.Add(chiTietLuanChuyenDC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChiTietLuanChuyenDC", new { maPhieuLC = chiTietLuanChuyenDC.MaPhieuLC, maDungCu = chiTietLuanChuyenDC.MaDungCu }, chiTietLuanChuyenDC);
        }

        // DELETE: api/ChiTietLuanChuyenDC/5/5
        [HttpDelete("{maPhieuLC}/{maDungCu}")]
        public async Task<IActionResult> DeleteChiTietLuanChuyenDC(string maPhieuLC, string maDungCu)
        {
            var chiTietLuanChuyenDC = await _context.ChiTietLuanChuyenDC.FindAsync(maPhieuLC, maDungCu);
            if (chiTietLuanChuyenDC == null)
            {
                return NotFound();
            }

            _context.ChiTietLuanChuyenDC.Remove(chiTietLuanChuyenDC);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/ChiTietLuanChuyenDC/maPhieuLC
        [HttpGet("{maPhieuLC}")]
        public async Task<ActionResult<IEnumerable<ChiTietLuanChuyenDC>>> GetChiTietLuanChuyenDCByMaPhieu(string maPhieuLC)
        {
            var chiTietLuanChuyenDC = await _context.ChiTietLuanChuyenDC
                .Where(c => c.MaPhieuLC == maPhieuLC)
                .ToListAsync();

            if (chiTietLuanChuyenDC == null || !chiTietLuanChuyenDC.Any())
            {
                return NotFound();
            }

            return chiTietLuanChuyenDC;
        }
        // PUT: api/ChiTietLuanChuyenDC/maPhieuLC/maDungCu
        [HttpPut("{maPhieuLC}/{maDungCu}")]
        public async Task<IActionResult> PutChiTietLuanChuyenDC(string maPhieuLC, string maDungCu, ChiTietLuanChuyenDC chiTietLuanChuyenDC)
        {
            // Check if the IDs in the URL match the IDs in the request body
            if (maPhieuLC != chiTietLuanChuyenDC.MaPhieuLC || maDungCu != chiTietLuanChuyenDC.MaDungCu)
            {
                return BadRequest("Mã Phiếu Luân Chuyển và Mã Dụng Cụ không khớp với thông tin yêu cầu.");
            }

            // Try to find the existing record in the database
            var existingRecord = await _context.ChiTietLuanChuyenDC
                .FirstOrDefaultAsync(c => c.MaPhieuLC == maPhieuLC && c.MaDungCu == maDungCu);

            if (existingRecord == null)
            {
                return NotFound("Chi tiết luân chuyển dụng cụ không tồn tại.");
            }

            // Update the existing record with the new values from the request body
            existingRecord.MaPhongDen = chiTietLuanChuyenDC.MaPhongDen;
            existingRecord.MaPhongTu = chiTietLuanChuyenDC.MaPhongTu;
            existingRecord.SoLuong = chiTietLuanChuyenDC.SoLuong;

            // Mark the entity as modified
            _context.Entry(existingRecord).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception
                var chiTietLuanChuyenDCExists = await _context.ChiTietLuanChuyenDC
                    .AnyAsync(c => c.MaPhieuLC == maPhieuLC && c.MaDungCu == maDungCu);

                if (!chiTietLuanChuyenDCExists)
                {
                    return NotFound("Chi tiết luân chuyển dụng cụ không tồn tại.");
                }
                else
                {
                    throw;
                }
            }

            // Return NoContent indicating a successful update
            return NoContent();
        }



    }
}
