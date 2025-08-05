using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuPhieuThanhLyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LichSuPhieuThanhLyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LichSuPhieuThanhLy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichSuPhieuThanhLy>>> GetLichSuPhieuThanhLy()
        {
            return await _context.LichSuPhieuThanhLy
                .Include(ls => ls.PhieuThanhLy)
                .Include(ls => ls.NhanVien)
                .ToListAsync();
        }

        // GET: api/LichSuPhieuThanhLy/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LichSuPhieuThanhLy>> GetLichSuPhieuThanhLyById(int id)
        {
            var lichSu = await _context.LichSuPhieuThanhLy
                .Include(ls => ls.PhieuThanhLy)
                .Include(ls => ls.NhanVien)
                .FirstOrDefaultAsync(ls => ls.MaLichSu == id);

            if (lichSu == null)
            {
                return NotFound();
            }

            return lichSu;
        }
        // GET: api/LichSuPhieuThanhLy/ByPhieu/{maPhieuTL}
        [HttpGet("ByPhieu/{maPhieuTL}")]
        public async Task<ActionResult<IEnumerable<LichSuPhieuThanhLy>>> GetLichSuByMaPhieuTL(string maPhieuTL)
        {
            var lichSuList = await _context.LichSuPhieuThanhLy
                .Where(ls => ls.MaPhieuTL == maPhieuTL)
                .Include(ls => ls.PhieuThanhLy) 
                .ToListAsync();

            if (lichSuList == null || lichSuList.Count == 0)
            {
                return NotFound($"Không tìm thấy lịch sử nào cho Mã Phiếu Thanh Lý: {maPhieuTL}");
            }

            return Ok(lichSuList);
        }


        // POST: api/LichSuPhieuThanhLy
        [HttpPost]
        public async Task<ActionResult<LichSuPhieuThanhLy>> CreateLichSuPhieuThanhLy(LichSuPhieuThanhLy lichSu)
        {
            if (lichSu == null)
            {
                return BadRequest("Invalid data.");
            }

            _context.LichSuPhieuThanhLy.Add(lichSu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLichSuPhieuThanhLyById), new { id = lichSu.MaLichSu }, lichSu);
        }

        // PUT: api/LichSuPhieuThanhLy/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLichSuPhieuThanhLy(int id, LichSuPhieuThanhLy lichSu)
        {
            if (id != lichSu.MaLichSu)
            {
                return BadRequest("ID mismatch.");
            }

            var existingLichSu = await _context.LichSuPhieuThanhLy.FindAsync(id);
            if (existingLichSu == null)
            {
                return NotFound();
            }

            existingLichSu.MaPhieuTL = lichSu.MaPhieuTL;
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
