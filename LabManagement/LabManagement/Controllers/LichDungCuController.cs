using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;

namespace LabManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LichDungCuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LichDungCuController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.LichDungCu.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.LichDungCu
                .Include(l => l.PhongThiNghiem)
                .Include(l => l.DungCu)
                .FirstOrDefaultAsync(l => l.MaLichDC == id);

            if (item == null) return NotFound();
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LichDungCu model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.LichDungCu.Add(model);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = model.MaLichDC }, model);
            }
            catch (Exception ex)
            {
                // Log lỗi
                Console.WriteLine($"Error while creating LichDungCu: {ex.Message}");
                return StatusCode(500, "Internal server error while creating LichDungCu.");
            }
        }

        [HttpGet("phong/{maPhong}")]
        public async Task<IActionResult> GetPhongByMaPhong(string maPhong)
        {
            var room = await _context.LichDungCu
                .Include(l => l.PhongThiNghiem)
                .FirstOrDefaultAsync(l => l.MaPhong == maPhong);

            if (room == null)
            {
                return NotFound(new { message = "Phòng thí nghiệm không tồn tại" });
            }

            return Ok(room);
        }
        [HttpGet("existing/ngay-su-dung")]
        public async Task<IActionResult> GetExistingNgayDC()
        {
            var ngaySuDungList = await _context.LichDungCu
                .Select(tb => new { tb.NgaySuDung })
                .ToListAsync();
            return Ok(ngaySuDungList);
        }
        [HttpGet("existing/ngay-ket-thuc")]
        public async Task<IActionResult> GetExistingNgayKTDC()
        {
            var ngaySuDungList = await _context.LichDungCu
                .Select(tb => new { tb.NgayKetThuc })
                .ToListAsync();
            return Ok(ngaySuDungList);
        }

        [HttpGet("existing/ngay-su-dung/phong/{maPhong}")]
        public async Task<IActionResult> GetExistingsNgaySuDungByMaPhong(string maPhong)
        {
            var ngaySuDungList = await _context.LichDungCu
                .Where(tb => tb.MaPhong == maPhong)
                .Select(tb => new { tb.NgaySuDung })
                .ToListAsync();
            return Ok(ngaySuDungList);
        }
        [HttpGet("existing/ngay-ket-thuc/phong/{maPhong}")]
        public async Task<IActionResult> GetExistingsNgayKetThucByMaPhong(string maPhong)
        {
            var ngayKetThucList = await _context.LichDungCu
                .Where(tb => tb.MaPhong == maPhong)
                .Select(tb => new { tb.NgayKetThuc })
                .ToListAsync();
            return Ok(ngayKetThucList);
        }

    }
}
