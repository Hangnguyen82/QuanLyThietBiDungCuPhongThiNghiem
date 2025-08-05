using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietPhieuThanhLyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChiTietPhieuThanhLyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{maPhieuTL}")]
        public async Task<IActionResult> GetByPhieuThanhLyId(string maPhieuTL)
        {
            var details = await _context.ChiTietPhieuThanhLy
                                        .Where(ct => ct.MaPhieuTL == maPhieuTL)
                                        .ToListAsync();
            return Ok(details);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChiTietPhieuThanhLy chiTietPhieuThanhLy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ChiTietPhieuThanhLy.Add(chiTietPhieuThanhLy);
            await _context.SaveChangesAsync();
            return Ok(chiTietPhieuThanhLy);
        }

        [HttpDelete("{maPhieuTL}/{maThietBi}")]
        public async Task<IActionResult> Delete(string maPhieuTL, string maThietBi)
        {
            var detail = await _context.ChiTietPhieuThanhLy.FindAsync(maPhieuTL, maThietBi);
            if (detail == null)
            {
                return NotFound();
            }

            _context.ChiTietPhieuThanhLy.Remove(detail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{maPhieuTL}/{maThietBi}")]
        public async Task<IActionResult> UpdateOrCreate(string maPhieuTL, string maThietBi, [FromBody] ChiTietPhieuThanhLy chiTietPhieuThanhLy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the existing detail by MaPhieuTL and MaThietBi
            var existingDetail = await _context.ChiTietPhieuThanhLy
                                                .FirstOrDefaultAsync(ct => ct.MaPhieuTL == maPhieuTL && ct.MaThietBi == maThietBi);

            if (existingDetail != null)
            {
                // If the detail exists, update it
                existingDetail.GiaTL = chiTietPhieuThanhLy.GiaTL;
                existingDetail.LyDo = chiTietPhieuThanhLy.LyDo;

                _context.ChiTietPhieuThanhLy.Update(existingDetail);
                await _context.SaveChangesAsync();

                return Ok(existingDetail);
            }
            else
            {
                // If the detail doesn't exist, create a new one
                chiTietPhieuThanhLy.MaPhieuTL = maPhieuTL;
                _context.ChiTietPhieuThanhLy.Add(chiTietPhieuThanhLy);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetByPhieuThanhLyId), new { maPhieuTL = maPhieuTL }, chiTietPhieuThanhLy);
            }
        }

    }
}
