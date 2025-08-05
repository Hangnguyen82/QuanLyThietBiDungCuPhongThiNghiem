using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuPhieuDeXuatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LichSuPhieuDeXuatController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LichSuPhieuDeXuat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichSuPhieuDeXuat>>> GetLichSuPhieuDeXuat()
        {
            var result = await _context.LichSuPhieuDeXuat
                .Include(ls => ls.PhieuDeXuat)
                .Include(ls => ls.NhanVien)
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/LichSuPhieuDeXuat/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LichSuPhieuDeXuat>> GetLichSuPhieuDeXuatById(int id)
        {
            var lichSu = await _context.LichSuPhieuDeXuat
                .Include(ls => ls.PhieuDeXuat)
                .Include(ls => ls.NhanVien)
                .FirstOrDefaultAsync(ls => ls.MaLichSu == id);

            if (lichSu == null)
            {
                return NotFound($"Không tìm thấy Lịch Sử Phiếu Đề Xuất với ID: {id}");
            }

            return Ok(lichSu);
        }

        // GET: api/LichSuPhieuDeXuat/ByPhieu/{maPhieu}
        [HttpGet("ByPhieu/{maPhieu}")]
        public async Task<ActionResult<IEnumerable<LichSuPhieuDeXuat>>> GetLichSuByMaPhieu(string maPhieu)
        {
            var lichSuList = await _context.LichSuPhieuDeXuat
                .Where(ls => ls.MaPhieu == maPhieu)
                .Include(ls => ls.PhieuDeXuat)
                .ToListAsync();

            if (lichSuList == null || lichSuList.Count == 0)
            {
                return NotFound($"Không tìm thấy lịch sử nào cho Mã Phiếu: {maPhieu}");
            }

            return Ok(lichSuList);
        }

        // POST: api/LichSuPhieuDeXuat
        [HttpPost]
        public async Task<ActionResult<LichSuPhieuDeXuat>> CreateLichSuPhieuDeXuat(LichSuPhieuDeXuat lichSu)
        {
            if (lichSu == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            _context.LichSuPhieuDeXuat.Add(lichSu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLichSuPhieuDeXuatById), new { id = lichSu.MaLichSu }, lichSu);
        }

        // PUT: api/LichSuPhieuDeXuat/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLichSuPhieuDeXuat(int id, LichSuPhieuDeXuat lichSu)
        {
            if (id != lichSu.MaLichSu)
            {
                return BadRequest("ID không khớp.");
            }

            var existingLichSu = await _context.LichSuPhieuDeXuat.FindAsync(id);
            if (existingLichSu == null)
            {
                return NotFound($"Không tìm thấy Lịch Sử Phiếu Đề Xuất với ID: {id}");
            }

            existingLichSu.MaPhieu = lichSu.MaPhieu;
            existingLichSu.TrangThaiTruoc = lichSu.TrangThaiTruoc;
            existingLichSu.TrangThaiSau = lichSu.TrangThaiSau;
            existingLichSu.NgayThayDoi = lichSu.NgayThayDoi;
            existingLichSu.MaNV = lichSu.MaNV;

            _context.Entry(existingLichSu).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
