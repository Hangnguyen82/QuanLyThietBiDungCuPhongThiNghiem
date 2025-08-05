using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietDeXuatThietBiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChiTietDeXuatThietBiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChiTietDeXuatThietBi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietDeXuatThietBi>>> GetChiTietDeXuatThietBis()
        {
            return await _context.ChiTietDeXuatThietBi.ToListAsync();
        }

        // GET: api/ChiTietDeXuatThietBi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChiTietDeXuatThietBi>> GetChiTietDeXuatThietBi(int id)
        {
            var chiTietDeXuatThietBi = await _context.ChiTietDeXuatThietBi.FindAsync(id);

            if (chiTietDeXuatThietBi == null)
            {
                return NotFound();
            }

            return chiTietDeXuatThietBi;
        }

        // POST: api/ChiTietDeXuatThietBi
        [HttpPost]
        public async Task<ActionResult<ChiTietDeXuatThietBi>> PostChiTietDeXuatThietBi(ChiTietDeXuatThietBi chiTietDeXuatThietBi)
        {
            _context.ChiTietDeXuatThietBi.Add(chiTietDeXuatThietBi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChiTietDeXuatThietBi), new { id = chiTietDeXuatThietBi.MaCTDeXuatTB }, chiTietDeXuatThietBi);
        }

        // PUT: api/ChiTietDeXuatThietBi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChiTietDeXuatThietBi(int id, ChiTietDeXuatThietBi chiTietDeXuatThietBi)
        {
            if (id != chiTietDeXuatThietBi.MaCTDeXuatTB)
            {
                return BadRequest();
            }

            _context.Entry(chiTietDeXuatThietBi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietDeXuatThietBiExists(id))
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

        // DELETE: api/ChiTietDeXuatThietBi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChiTietDeXuatThietBi(int id)
        {
            var chiTietDeXuatThietBi = await _context.ChiTietDeXuatThietBi.FindAsync(id);
            if (chiTietDeXuatThietBi == null)
            {
                return NotFound();
            }

            _context.ChiTietDeXuatThietBi.Remove(chiTietDeXuatThietBi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChiTietDeXuatThietBiExists(int id)
        {
            return _context.ChiTietDeXuatThietBi.Any(e => e.MaCTDeXuatTB == id);
        }
        // GET: api/ChiTietDeXuatThietBi/maPhieu
        [HttpGet("byphieu/{maPhieu}")]
        public async Task<ActionResult<IEnumerable<ChiTietDeXuatThietBi>>> GetChiTietDeXuatThietBiByMaPhieu(string maPhieu)
        {
            // Lọc các chi tiết đề xuất thiết bị theo maPhieu
            var chiTietDeXuatThietBis = await _context.ChiTietDeXuatThietBi
                                                      .Where(ct => ct.MaPhieu == maPhieu)
                                                      .ToListAsync();

            if (chiTietDeXuatThietBis == null || !chiTietDeXuatThietBis.Any())
            {
                return NotFound();  // Nếu không tìm thấy chi tiết nào, trả về NotFound
            }

            return chiTietDeXuatThietBis;  // Trả về danh sách chi tiết đề xuất thiết bị
        }
        [HttpPut("{maPhieu}/{maLoaiThietBi}")]
        public async Task<IActionResult> UpdateOrCreate(string maPhieu, string maLoaiThietBi, [FromBody] ChiTietDeXuatThietBi chiTietDeXuatThietBi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tìm kiếm chi tiết đã tồn tại theo MaPhieu và MaLoaiThietBi
            var existingDetail = await _context.ChiTietDeXuatThietBi
                                               .FirstOrDefaultAsync(ct => ct.MaPhieu == maPhieu && ct.MaLoaiThietBi == maLoaiThietBi);

            if (existingDetail != null)
            {
                // Nếu chi tiết đã tồn tại, cập nhật nó
                existingDetail.TenThietBi = chiTietDeXuatThietBi.TenThietBi;
                existingDetail.SoLuongDeXuat = chiTietDeXuatThietBi.SoLuongDeXuat;
                existingDetail.MoTa = chiTietDeXuatThietBi.MoTa;

                _context.ChiTietDeXuatThietBi.Update(existingDetail);
                await _context.SaveChangesAsync();

                return Ok(existingDetail);
            }
            else
            {
                // Nếu chi tiết không tồn tại, tạo mới một chi tiết đề xuất thiết bị
                chiTietDeXuatThietBi.MaPhieu = maPhieu;
                _context.ChiTietDeXuatThietBi.Add(chiTietDeXuatThietBi);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetChiTietDeXuatThietBi), new { id = chiTietDeXuatThietBi.MaCTDeXuatTB }, chiTietDeXuatThietBi);
            }
       
        }
    }
}
