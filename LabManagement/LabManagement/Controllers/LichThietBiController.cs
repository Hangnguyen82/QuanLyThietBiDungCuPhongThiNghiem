using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;

namespace LabManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LichThietBiController :ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LichThietBiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.LichThietBi.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.LichThietBi
                .Include(l => l.PhongThiNghiem)
                .Include(l => l.ThietBi)
                .FirstOrDefaultAsync(l => l.MaLichTB == id);

            if (item == null) return NotFound();
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LichThietBi model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.LichThietBi.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.MaLichTB }, model);
        }

        [HttpGet("phong/{maPhong}")]
        public async Task<IActionResult> GetPhongByMaPhong(string maPhong)
        {
            var room = await _context.LichThietBi
                .Include(l => l.PhongThiNghiem)
                .FirstOrDefaultAsync(l => l.MaPhong == maPhong);

            if (room == null)
            {
                return NotFound(new { message = "Phòng thí nghiệm không tồn tại" });
            }

            return Ok(room);
        }
        [HttpGet("existing/ngay-thiet-bi")]
        public async Task<IActionResult> GetExistingNgayThietBi()
        {
            var ngaySuDungList = await _context.LichThietBi
                .Select(tb => new { tb.NgaySuDung })
                .ToListAsync();
            return Ok(ngaySuDungList);
        }
        [HttpGet("existing/ngay-ket-thuc")]
        public async Task<IActionResult> GetExistingNgayKTThietBi()
        {
            var ngaySuDungList = await _context.LichThietBi
                .Select(tb => new { tb.NgayKetThuc })
                .ToListAsync();
            return Ok(ngaySuDungList);
        }
        [HttpGet("existing/ngay-su-dung/phong/{maPhong}")]
        public async Task<IActionResult> GetExistingsNgaySuDungByMaPhong(string maPhong)
        {
            var ngaySuDungList = await _context.LichThietBi
                .Where(tb => tb.MaPhong == maPhong)
                .Select(tb => new { tb.NgaySuDung })
                .ToListAsync();
            return Ok(ngaySuDungList);
        }
        [HttpGet("existing/ngay-ket-thuc/phong/{maPhong}")]
        public async Task<IActionResult> GetExistingsNgayKetThucByMaPhong(string maPhong)
        {
            var ngayKetThucList = await _context.LichThietBi
                .Where(tb => tb.MaPhong == maPhong)
                .Select(tb => new { tb.NgayKetThuc })
                .ToListAsync();
            return Ok(ngayKetThucList);
        }



    }
}
