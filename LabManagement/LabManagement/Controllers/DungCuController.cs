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
    public class DungCuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        //private const string ImageDirectory = @"D:\Project\LabManagement\LabManagement\LabManagement\Image\DungCu";
        private const string ImageBaseUrl = "http://localhost:5123/images/DungCu";

        private static readonly string ImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "DungCu");
        //private const string ImageBaseUrl = "http://localhost/images/DungCu";
        public DungCuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/dungcu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DungCu>>> GetAllDungCu()
        {
            var dungCus = await _context.DungCu
                .Include(dc => dc.LoaiDungCu)
                .Include(dc => dc.NhaCungCap)
                .ToListAsync();

            return dungCus;
        }

        // GET: api/dungcu/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DungCu>> GetDungCuById(string id)
        {
            var dungCu = await _context.DungCu
                .Include(dc => dc.LoaiDungCu)
                .Include(dc => dc.NhaCungCap)
                .FirstOrDefaultAsync(dc => dc.MaDungCu == id);

            if (dungCu == null)
            {
                return NotFound();
            }

            return dungCu;
        }

        // POST: api/dungcu
        [HttpPost]
        public async Task<ActionResult<DungCu>> CreateDungCu(DungCu dungCu)
        {
            if (!string.IsNullOrEmpty(dungCu.HinhAnh))
            {
                string fullImagePath = Path.Combine(ImageDirectory, dungCu.HinhAnh);
                if (!System.IO.File.Exists(fullImagePath))
                {
                    return BadRequest("Hình ảnh không tồn tại.");
                }
            }

            _context.DungCu.Add(dungCu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDungCuById), new { id = dungCu.MaDungCu }, dungCu);
        }

        // PUT: api/dungcu/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDungCu(string id, DungCu dungCu)
        {
            if (id != dungCu.MaDungCu)
            {
                return BadRequest("Mã dụng cụ không khớp.");
            }

            if (!string.IsNullOrEmpty(dungCu.HinhAnh))
            {
                string fullImagePath = Path.Combine(ImageDirectory, dungCu.HinhAnh);
                if (!System.IO.File.Exists(fullImagePath))
                {
                    return BadRequest("Hình ảnh không tồn tại.");
                }
            }

            _context.Entry(dungCu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DungCuExists(id))
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

        // DELETE: api/dungcu/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDungCu(string id)
        {
            var dungCu = await _context.DungCu.FindAsync(id);
            if (dungCu == null)
            {
                return NotFound();
            }

            _context.DungCu.Remove(dungCu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("LoaiDungCu/{maLoaiDC}")]
        public async Task<ActionResult<IEnumerable<DungCu>>> GetDungCusByLoaiDungCu(string maLoaiDC)
        {
            try
            {
                // Lọc danh sách dụng cụ theo mã loại dụng cụ
                var dungCus = await _context.DungCu
                    .Where(dc => dc.MaLoaiDC == maLoaiDC)
                    .Include(dc => dc.LoaiDungCu)  // Bao gồm thông tin loại dụng cụ
                    .Include(dc => dc.NhaCungCap) // Bao gồm thông tin nhà cung cấp
                    .ToListAsync();

                // Nếu không tìm thấy dụng cụ nào
                if (!dungCus.Any())
                {
                    return NotFound("Không tìm thấy dụng cụ nào thuộc loại này.");
                }

                return Ok(dungCus); // Trả về danh sách dụng cụ đã lọc
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }



        private bool DungCuExists(string id)
        {
            return _context.DungCu.Any(e => e.MaDungCu == id);
        }

        // GET: api/dungcu/CheckExistence?ma={maItem}
[HttpGet("CheckExistence")]
public async Task<IActionResult> CheckExistence([FromQuery] string ma)
{
    try
    {
        // Check if the item exists in the database
        bool exists = await _context.DungCu.AnyAsync(dc => dc.MaDungCu == ma);

        // Return a response indicating the existence of the item
        if (exists)
        {
            return Ok(new { exists = true, message = "Dụng cụ tồn tại." });
        }
        else
        {
            return NotFound(new { exists = false, message = "Không tìm thấy dụng cụ." });
        }
    }
    catch (Exception ex)
    {
        // Handle unexpected errors
        return StatusCode(500, new { exists = false, message = $"Lỗi: {ex.Message}" });
    }
    }
        // GET: api/dungcu/quantities/{maDungCu}
        [HttpGet("quantities/{maDungCu}")]
        public async Task<ActionResult<int>> GetQuantitiesDungCu(string maDungCu)
        {
            try
            {
                // Tìm dụng cụ theo mã dụng cụ
                var dungCu = await _context.DungCu
                    .FirstOrDefaultAsync(dc => dc.MaDungCu == maDungCu);

                if (dungCu == null)
                {
                    return NotFound(new { message = "Dụng cụ không tồn tại." });
                }

                // Trả về số lượng của dụng cụ
                return Ok(dungCu.SoLuong);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, new { message = $"Lỗi: {ex.Message}" });
            }
        }


    }
}
