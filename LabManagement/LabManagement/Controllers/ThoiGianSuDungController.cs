using Microsoft.AspNetCore.Mvc;
using LabManagement.Data;
using LabManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThoiGianSuDungController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ThoiGianSuDungController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ThoiGianSuDung
        [HttpGet]
        public async Task<IActionResult> GetThoiGianSuDung()
        {
            var thoiGianSuDungs = await _context.ThoiGianSuDung
                .Include(p => p.PhieuDangKi)
                .Include(p => p.ThietBi)
                .Include(p => p.NhanVien)
                .ToListAsync();

            return Ok(thoiGianSuDungs);
        }

        // POST: api/ThoiGianSuDung
        [HttpPost]
        public async Task<IActionResult> CreateThoiGianSuDung([FromBody] ThoiGianSuDung newThoiGianSuDung)
        {
            try
            {
                if (newThoiGianSuDung == null)
                {
                    return BadRequest("ThoiGianSuDung cannot be null.");
                }

                if (string.IsNullOrEmpty(newThoiGianSuDung.MaPhieuDK) || string.IsNullOrEmpty(newThoiGianSuDung.MaThietBi))
                {
                    return BadRequest("Both MaPhieuDK and MaThietBi are required.");
                }

                _context.ThoiGianSuDung.Add(newThoiGianSuDung);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetThoiGianSuDung),
                    new { maPhieuDK = newThoiGianSuDung.MaPhieuDK, maThietBi = newThoiGianSuDung.MaThietBi },
                    newThoiGianSuDung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{maPhieuDK}")]
        public async Task<IActionResult> GetThoiGianSuDungByMaPhieu(string maPhieuDK)
        {
            var thoiGianSuDung = await _context.ThoiGianSuDung
                .Where(p => p.MaPhieuDK == maPhieuDK) // Lọc theo MaPhieuDK
                .Include(p => p.PhieuDangKi)         // Bao gồm thông tin phiếu đăng ký
                .Include(p => p.ThietBi)            // Bao gồm danh sách thiết bị liên quan
                .Include(p => p.NhanVien)           // Bao gồm thông tin nhân viên
                .ToListAsync();                     // Lấy danh sách tất cả các bản ghi phù hợp

            if (thoiGianSuDung == null || !thoiGianSuDung.Any())
            {
                return NotFound($"No ThoiGianSuDung found for MaPhieuDK '{maPhieuDK}'.");
            }

            return Ok(thoiGianSuDung);
        }

        // GET: api/ThoiGianSuDung/existing
        [HttpGet("existing")]
        public async Task<IActionResult> GetExistingThoiGianSuDungKeys()
        {
            var keys = await _context.ThoiGianSuDung
                .Select(p => new { p.MaPhieuDK, p.MaThietBi })
                .ToListAsync();

            return Ok(keys);
        }



    }
}
