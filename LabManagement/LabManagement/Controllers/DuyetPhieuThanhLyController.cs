using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuyetPhieuThanhLyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DuyetPhieuThanhLyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{maPhieuTL}")]
        public async Task<IActionResult> GetByPhieuThanhLyId(string maPhieuTL)
        {
            var approvals = await _context.DuyetPhieuThanhLy
                                          .Where(dp => dp.MaPhieuTL == maPhieuTL)
                                          .ToListAsync();
            return Ok(approvals);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DuyetPhieuThanhLy duyetPhieuThanhLy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DuyetPhieuThanhLy.Add(duyetPhieuThanhLy);
            await _context.SaveChangesAsync();
            return Ok(duyetPhieuThanhLy);
        }

        [HttpPut("{maPhieuTL}")]
        public async Task<IActionResult> UpdateTrangThaiOrLyDo(string maPhieuTL, [FromBody] DuyetPhieuThanhLy updatedDuyetPhieuThanhLy)
        {
            // Tìm đối tượng trong cơ sở dữ liệu theo maPhieuTL
            var existingDuyetPhieuThanhLy = await _context.DuyetPhieuThanhLy
                .FirstOrDefaultAsync(e => e.MaPhieuTL == maPhieuTL);

            if (existingDuyetPhieuThanhLy == null)
            {
                return NotFound("Không tìm thấy phiếu thanh lý với mã tương ứng.");
            }

            // Chỉ cập nhật các thuộc tính được phép
            if (!string.IsNullOrEmpty(updatedDuyetPhieuThanhLy.TrangThai))
            {
                existingDuyetPhieuThanhLy.TrangThai = updatedDuyetPhieuThanhLy.TrangThai;
            }

            if (!string.IsNullOrEmpty(updatedDuyetPhieuThanhLy.LyDoTuChoi))
            {
                existingDuyetPhieuThanhLy.LyDoTuChoi = updatedDuyetPhieuThanhLy.LyDoTuChoi;
            }

            // Giữ nguyên các thuộc tính khác
            // Lưu các thay đổi vào cơ sở dữ liệu
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw; // Xử lý ngoại lệ nếu cần
            }

            return NoContent();
        }

    }
}
