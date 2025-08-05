using Microsoft.AspNetCore.Mvc;
using LabManagement.Data;
using LabManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietNhapTBController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChiTietNhapTBController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChiTietNhapTB
        [HttpGet]
        public async Task<IActionResult> GetChiTietNhapTB()
        {
            var chiTietNhapTBs = await _context.ChiTietNhapTB
                .Include(c => c.PhieuNhap) 
                .Include(c => c.ThietBi)   
                .ToListAsync();

            return Ok(chiTietNhapTBs);
        }

        // POST: api/ChiTietNhapTB
        [HttpPost]
        public async Task<IActionResult> CreateChiTietNhapTB([FromBody] ChiTietNhapTB newChiTiet)
        {
            try
            {
                if (newChiTiet == null)
                {
                    return BadRequest("ChiTietNhapTB cannot be null.");
                }

                if (string.IsNullOrEmpty(newChiTiet.MaPhieuNhap) || string.IsNullOrEmpty(newChiTiet.MaThietBi))
                {
                    return BadRequest("Both MaPhieuNhap and MaThietBi are required.");
                }

                var phieuNhap = await _context.PhieuNhap.FindAsync(newChiTiet.MaPhieuNhap);
                if (phieuNhap == null)
                {
                    return NotFound("PhieuNhap not found.");
                }

                var thietBi = await _context.ThietBi.FindAsync(newChiTiet.MaThietBi);
                if (thietBi == null)
                {
                    return NotFound("ThietBi not found.");
                }

                _context.ChiTietNhapTB.Add(newChiTiet);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetChiTietNhapTB), new { maPhieuNhap = newChiTiet.MaPhieuNhap, maThietBi = newChiTiet.MaThietBi }, newChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/ChiTietNhapTB/{maPhieuNhap}
        [HttpGet("{maPhieuNhap}")]
        public async Task<IActionResult> GetChiTietNhapTBByMaPhieuNhap(string maPhieuNhap)
        {
  
            var chiTietNhapTBs = await _context.ChiTietNhapTB
                .Include(c => c.PhieuNhap) 
                .Include(c => c.ThietBi)    
                .Where(c => c.MaPhieuNhap == maPhieuNhap) 
                .ToListAsync();

            if (chiTietNhapTBs == null || chiTietNhapTBs.Count == 0)
            {
                return NotFound("No details found for the given MaPhieuNhap.");
            }

            return Ok(chiTietNhapTBs);
        }
    }
}
