using Microsoft.AspNetCore.Mvc;
using LabManagement.Data;
using LabManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietNhapDCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChiTietNhapDCController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChiTietNhapDC
        [HttpGet]
        public async Task<IActionResult> GetChiTietNhapDC()
        {
            var chiTietNhapDCs = await _context.ChiTietNhapDC
                .Include(c => c.PhieuNhap) 
                .Include(c => c.DungCu)    // Include DungCu for detailed info
                .ToListAsync();

            return Ok(chiTietNhapDCs);
        }

        // POST: api/ChiTietNhapDC
        [HttpPost]
        public async Task<IActionResult> CreateChiTietNhapDC([FromBody] ChiTietNhapDC newChiTiet)
        {
            try
            {
                if (newChiTiet == null)
                {
                    return BadRequest("ChiTietNhapDC cannot be null.");
                }

                if (string.IsNullOrEmpty(newChiTiet.MaPhieuNhap) || string.IsNullOrEmpty(newChiTiet.MaDungCu))
                {
                    return BadRequest("Both MaPhieuNhap and MaDungCu are required.");
                }

                var phieuNhap = await _context.PhieuNhap.FindAsync(newChiTiet.MaPhieuNhap);
                if (phieuNhap == null)
                {
                    return NotFound("PhieuNhap not found.");
                }

                var dungCu = await _context.DungCu.FindAsync(newChiTiet.MaDungCu);
                if (dungCu == null)
                {
                    return NotFound("DungCu not found.");
                }

                _context.ChiTietNhapDC.Add(newChiTiet);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetChiTietNhapDC), new { maPhieuNhap = newChiTiet.MaPhieuNhap, maDungCu = newChiTiet.MaDungCu }, newChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/ChiTietNhapDC/{maPhieuNhap}
        [HttpGet("{maPhieuNhap}")]
        public async Task<IActionResult> GetChiTietNhapDCByMaPhieuNhap(string maPhieuNhap)
        {

            var chiTietNhapDCs = await _context.ChiTietNhapDC
                .Include(c => c.PhieuNhap) 
                .Include(c => c.DungCu)    
                .Where(c => c.MaPhieuNhap == maPhieuNhap) 
                .ToListAsync();

            if (chiTietNhapDCs == null || chiTietNhapDCs.Count == 0)
            {
                return NotFound("No details found for the given MaPhieuNhap.");
            }

            return Ok(chiTietNhapDCs);
        }
       


    }
}
