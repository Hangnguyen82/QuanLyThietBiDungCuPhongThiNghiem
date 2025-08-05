using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;
using LabManagement.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThietBiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private static readonly string ImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "ThietBi");
        private const string ImageBaseUrl = "http://localhost:5123/images/ThietBi";

        public ThietBiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/thietbi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThietBi>>> GetAllThietBi()
        {
            var thietBis = await _context.ThietBi
                .Include(tb => tb.LoaiThietBi)
                .Include(tb => tb.NhaCungCap)
                .Include(tb => tb.PhongThiNghiem)
                .ToListAsync();

            return thietBis;
        }

        // GET: api/thietbi/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ThietBi>> GetThietBiById(string id)
        {
            var thietBi = await _context.ThietBi
                .Include(tb => tb.LoaiThietBi)
                .Include(tb => tb.NhaCungCap)
                .Include(tb => tb.PhongThiNghiem)
                .FirstOrDefaultAsync(tb => tb.MaThietBi == id);

            if (thietBi == null)
            {
                return NotFound();
            }

            return thietBi;
        }

        // POST: api/thietbi
        [HttpPost]
        public async Task<ActionResult<ThietBi>> CreateThietBi(ThietBi thietBi)
        {
            if (!string.IsNullOrEmpty(thietBi.HinhAnh))
            {
                string fullImagePath = Path.Combine(ImageDirectory, thietBi.HinhAnh);
                if (!System.IO.File.Exists(fullImagePath))
                {
                    return BadRequest("Hình ảnh không tồn tại.");
                }
            }

            _context.ThietBi.Add(thietBi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetThietBiById), new { id = thietBi.MaThietBi }, thietBi);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateThietBi(string id, ThietBi updatedData)
        {
            // Lấy thiết bị hiện có từ database
            var existingThietBi = await _context.ThietBi.FindAsync(id);
            if (existingThietBi == null)
            {
                return NotFound("Thiết bị không tồn tại.");
            }

            // Cập nhật các trường cụ thể
            existingThietBi.NgayCapNhat = updatedData.NgayCapNhat;
            existingThietBi.TinhTrang = updatedData.TinhTrang;

            // Đảm bảo không thay đổi các trường khác
            _context.Entry(existingThietBi).Property(x => x.NgayCapNhat).IsModified = true;
            _context.Entry(existingThietBi).Property(x => x.TinhTrang).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThietBiExists(id))
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


        // DELETE: api/thietbi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThietBi(string id)
        {
            var thietBi = await _context.ThietBi.FindAsync(id);
            if (thietBi == null)
            {
                return NotFound();
            }

            _context.ThietBi.Remove(thietBi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ThietBiExists(string id)
        {
            return _context.ThietBi.Any(e => e.MaThietBi == id);
        }
        // GET: api/thietbi/loaithethicbi/{maLoaiThietBi}
        [HttpGet("LoaiThietBi/{maLoaiThietBi}")]
        public async Task<ActionResult<IEnumerable<ThietBi>>> GetThietBisByLoaiThietBi(string maLoaiThietBi)
        {
            try
            {
                var thietBis = await _context.ThietBi
                    .Where(tb => tb.MaLoaiThietBi == maLoaiThietBi)
                    .Include(tb => tb.LoaiThietBi)
                    .Include(tb => tb.NhaCungCap)
                    .Include(tb => tb.PhongThiNghiem)
                    .ToListAsync();

                if (!thietBis.Any())
                {
                    return NotFound("Không tìm thấy thiết bị nào thuộc loại này.");
                }

                return Ok(thietBis); // Trả về danh sách thiết bị
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
        // GET: api/thietbi/CheckExistence?ma={maItem}
        [HttpGet("CheckExistence")]
        public async Task<IActionResult> CheckExistence([FromQuery] string ma)
        {
            if (string.IsNullOrEmpty(ma))
            {
                return BadRequest("Mã không được để trống.");
            }

            var exists = await _context.ThietBi.AnyAsync(tb => tb.MaThietBi == ma);

            if (exists)
            {
                return Ok(new { Exists = true, Message = "Thiết bị tồn tại trong hệ thống." });
            }

            return NotFound(new { Exists = false, Message = "Không tìm thấy thiết bị trong hệ thống." });
        }
        // In ThietBiController.cs
        [HttpPost("BatchUpdateIsDeleted")]
        public async Task<IActionResult> BatchUpdateIsDeleted([FromBody] List<string> maThietBiList)
        {
            if (maThietBiList == null || !maThietBiList.Any())
            {
                return BadRequest("Danh sách mã thiết bị không được để trống.");
            }

            var thietBis = await _context.ThietBi.Where(tb => maThietBiList.Contains(tb.MaThietBi)).ToListAsync();
            if (!thietBis.Any())
            {
                return NotFound("Không tìm thấy thiết bị nào tương ứng.");
            }

            foreach (var thietBi in thietBis)
            {
                thietBi.isDeleted = true; 
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Cập nhật thành công." });
        }
          // GET: api/thietbi/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ThietBi>>> SearchThietBi(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Query không được để trống.");
            }

            var results = await _context.ThietBi
                .Where(tb => tb.MaThietBi.Contains(query) || tb.TenThietBi.Contains(query))
                .ToListAsync();

            if (!results.Any())
            {
                return NotFound("Không tìm thấy thiết bị phù hợp.");
            }

            return Ok(results);
        }
        [HttpPut("{maThietBi}/UpdateMaPhong")]
        public async Task<IActionResult> UpdateMaPhong(string maThietBi, [FromBody] UpdateMaPhongRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.MaPhong))
            {
                return BadRequest("Mã phòng không được để trống.");
            }

            // Tìm thiết bị cần cập nhật
            var thietBi = await _context.ThietBi.FirstOrDefaultAsync(tb => tb.MaThietBi == maThietBi);
            if (thietBi == null)
            {
                return NotFound($"Không tìm thấy thiết bị với mã {maThietBi}.");
            }

            // Kiểm tra mã phòng mới có tồn tại trong hệ thống hay không
            var phongThiNghiem = await _context.PhongThiNghiem.FirstOrDefaultAsync(ptn => ptn.MaPhong == request.MaPhong);
            if (phongThiNghiem == null)
            {
                return NotFound($"Không tìm thấy phòng thí nghiệm với mã {request.MaPhong}.");
            }

            // Cập nhật mã phòng
            thietBi.MaPhong = request.MaPhong;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Cập nhật mã phòng thành công.",
                    ThietBi = thietBi
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ThietBi.Any(tb => tb.MaThietBi == maThietBi))
                {
                    return NotFound($"Thiết bị với mã {maThietBi} không tồn tại.");
                }
                else
                {
                    throw;
                }
            }
        }

        // Định nghĩa lớp yêu cầu
        public class UpdateMaPhongRequest
        {
            public string? MaPhong { get; set; }
        }



    }
}
