using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuPhieuLuanChuyenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LichSuPhieuLuanChuyenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LichSuPhieuLuanChuyen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichSuPhieuLuanChuyen>>> GetLichSuPhieuLuanChuyen()
        {
            return await _context.LichSuPhieuLuanChuyen.ToListAsync();
        }

        // GET: api/LichSuPhieuLuanChuyen/maPhieuLC
        [HttpGet("{maPhieuLC}")]
        public async Task<ActionResult<IEnumerable<LichSuPhieuLuanChuyen>>> GetLichSuPhieuLuanChuyenByMaPhieuLC(string maPhieuLC)
        {
            // Tìm kiếm lịch sử theo maPhieuLC
            var lichSuPhieuLuanChuyen = await _context.LichSuPhieuLuanChuyen
                .Where(ls => ls.MaPhieuLC == maPhieuLC)
                .ToListAsync();

            if (lichSuPhieuLuanChuyen == null || !lichSuPhieuLuanChuyen.Any())
            {
                return NotFound(new { Message = "Không tìm thấy lịch sử theo mã phiếu luân chuyển." });
            }

            return Ok(lichSuPhieuLuanChuyen);
        }


        // POST: api/LichSuPhieuLuanChuyen
        [HttpPost]
        public async Task<ActionResult<LichSuPhieuLuanChuyen>> PostLichSuPhieuLuanChuyen(LichSuPhieuLuanChuyen lichSuPhieuLuanChuyen)
        {
            // Đảm bảo rằng MaLichSu không được gán giá trị, vì nó sẽ tự động tăng
            _context.LichSuPhieuLuanChuyen.Add(lichSuPhieuLuanChuyen);
            await _context.SaveChangesAsync();

            // Trả về kết quả, không cần phải truyền MaLichSu trong phản hồi vì nó tự động tăng
            return CreatedAtAction("GetLichSuPhieuLuanChuyen", new { id = lichSuPhieuLuanChuyen.MaPhieuLC }, lichSuPhieuLuanChuyen);
        }

        // DELETE: api/LichSuPhieuLuanChuyen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichSuPhieuLuanChuyen(string id)
        {
            var lichSuPhieuLuanChuyen = await _context.LichSuPhieuLuanChuyen.FindAsync(id);
            if (lichSuPhieuLuanChuyen == null)
            {
                return NotFound();
            }

            _context.LichSuPhieuLuanChuyen.Remove(lichSuPhieuLuanChuyen);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
