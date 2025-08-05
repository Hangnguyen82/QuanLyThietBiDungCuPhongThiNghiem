using Microsoft.AspNetCore.Mvc;
using LabManagement.Model;
using LabManagement.Data;  // Đảm bảo sử dụng đúng namespace cho DbContext
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuNhapController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhieuNhapController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PhieuNhap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhieuNhap>>> GetPhieuNhaps()
        {
            // Lấy danh sách tất cả các phiếu nhập với thông tin nhân viên, không cần kiểm tra MaPhieu
            var phieuNhaps = await _context.PhieuNhap
                .Include(p => p.NhanVien) // Bao gồm thông tin nhân viên
                .ToListAsync();

            // Kiểm tra nếu không có phiếu nhập nào
            if (phieuNhaps == null || !phieuNhaps.Any())
            {
                return NotFound("Không tìm thấy phiếu nhập.");
            }

            return Ok(phieuNhaps);
        }


        // GET: api/PhieuNhap/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PhieuNhap>> GetPhieuNhap(string id)
        {
            var phieuNhap = await _context.PhieuNhap
                .Include(p => p.NhanVien)
                .FirstOrDefaultAsync(p => p.MaPhieuNhap == id);

            if (phieuNhap == null)
            {
                return NotFound();
            }

            return Ok(phieuNhap);
        }

        // POST: api/PhieuNhap
        [HttpPost]
        public async Task<ActionResult<PhieuNhap>> PostPhieuNhap(PhieuNhap phieuNhap)
        {
            // Kiểm tra xem MaPhieuNhap đã tồn tại hay chưa
            if (_context.PhieuNhap.Any(p => p.MaPhieuNhap == phieuNhap.MaPhieuNhap))
            {
                return Conflict("Mã phiếu nhập đã tồn tại.");
            }

            _context.PhieuNhap.Add(phieuNhap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhieuNhap", new { id = phieuNhap.MaPhieuNhap }, phieuNhap);
        }
    }
}
