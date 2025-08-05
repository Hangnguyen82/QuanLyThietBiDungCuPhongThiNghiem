using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietDeXuatDungCuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChiTietDeXuatDungCuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChiTietDeXuatDungCu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietDeXuatDungCu>>> GetChiTietDeXuatDungCus()
        {
            return await _context.ChiTietDeXuatDungCu.ToListAsync();
        }

        // GET: api/ChiTietDeXuatDungCu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChiTietDeXuatDungCu>> GetChiTietDeXuatDungCu(int id)
        {
            var chiTietDeXuatDungCu = await _context.ChiTietDeXuatDungCu.FindAsync(id);

            if (chiTietDeXuatDungCu == null)
            {
                return NotFound();
            }

            return chiTietDeXuatDungCu;
        }
        [HttpGet("byphieu/{maPhieu}")]
        public async Task<ActionResult<ChiTietDeXuatDungCu>> GetChiTietDeXuatDungCuByPhieu(string maPhieu)
        {
            var details = await _context.ChiTietDeXuatDungCu
                                        .Where(ct => ct.MaPhieu == maPhieu)
                                        .ToListAsync();
            return Ok(details);
        }
        // POST: api/ChiTietDeXuatDungCu
        [HttpPost]
        public async Task<ActionResult<ChiTietDeXuatDungCu>> PostChiTietDeXuatDungCu(ChiTietDeXuatDungCu chiTietDeXuatDungCu)
        {
            _context.ChiTietDeXuatDungCu.Add(chiTietDeXuatDungCu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChiTietDeXuatDungCu), new { id = chiTietDeXuatDungCu.MaCTDeXuatDC }, chiTietDeXuatDungCu);
        }

        // PUT: api/ChiTietDeXuatDungCu/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChiTietDeXuatDungCu(int id, ChiTietDeXuatDungCu chiTietDeXuatDungCu)
        {
            if (id != chiTietDeXuatDungCu.MaCTDeXuatDC)
            {
                return BadRequest();
            }

            _context.Entry(chiTietDeXuatDungCu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietDeXuatDungCuExists(id))
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

        // DELETE: api/ChiTietDeXuatDungCu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChiTietDeXuatDungCu(int id)
        {
            var chiTietDeXuatDungCu = await _context.ChiTietDeXuatDungCu.FindAsync(id);
            if (chiTietDeXuatDungCu == null)
            {
                return NotFound();
            }

            _context.ChiTietDeXuatDungCu.Remove(chiTietDeXuatDungCu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChiTietDeXuatDungCuExists(int id)
        {
            return _context.ChiTietDeXuatDungCu.Any(e => e.MaCTDeXuatDC == id);
        }
        [HttpPut("{maPhieu}/{maLoaiDC}")]
        public async Task<IActionResult> UpdateOrCreate(string maPhieu, string maLoaiDC, [FromBody] ChiTietDeXuatDungCu chiTietDeXuatDungCu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tìm kiếm chi tiết đã tồn tại theo MaPhieu và MaLoaiDC
            var existingDetail = await _context.ChiTietDeXuatDungCu
                                               .FirstOrDefaultAsync(ct => ct.MaPhieu == maPhieu && ct.MaLoaiDC == maLoaiDC);

            if (existingDetail != null)
            {
                // Nếu chi tiết đã tồn tại, cập nhật nó
                existingDetail.TenDungCu = chiTietDeXuatDungCu.TenDungCu;
                existingDetail.SoLuongDeXuat = chiTietDeXuatDungCu.SoLuongDeXuat;
                existingDetail.MoTa = chiTietDeXuatDungCu.MoTa;

                _context.ChiTietDeXuatDungCu.Update(existingDetail);
                await _context.SaveChangesAsync();

                return Ok(existingDetail);
            }
            else
            {
                // Nếu chi tiết không tồn tại, tạo mới một chi tiết đề xuất dụng cụ
                chiTietDeXuatDungCu.MaPhieu = maPhieu;
                // Do not manually set MaCTDeXuatDC, let it auto-generate
                _context.ChiTietDeXuatDungCu.Add(chiTietDeXuatDungCu);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetChiTietDeXuatDungCu), new { id = chiTietDeXuatDungCu.MaCTDeXuatDC }, chiTietDeXuatDungCu);
            }
        }


    }
}
