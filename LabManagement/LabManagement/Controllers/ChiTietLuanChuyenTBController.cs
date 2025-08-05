using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietLuanChuyenTBController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChiTietLuanChuyenTBController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChiTietLuanChuyenTB
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietLuanChuyenTB>>> GetChiTietLuanChuyenTB()
        {
            return await _context.ChiTietLuanChuyenTB.ToListAsync();
        }

        // GET: api/ChiTietLuanChuyenTB/5/5
        [HttpGet("{maPhieuLC}/{maThietBi}")]
        public async Task<ActionResult<ChiTietLuanChuyenTB>> GetChiTietLuanChuyenTB(string maPhieuLC, string maThietBi)
        {
            var chiTietLuanChuyenTB = await _context.ChiTietLuanChuyenTB.FindAsync(maPhieuLC, maThietBi);

            if (chiTietLuanChuyenTB == null)
            {
                return NotFound();
            }

            return chiTietLuanChuyenTB;
        }

        // POST: api/ChiTietLuanChuyenTB
        [HttpPost]
        public async Task<ActionResult<ChiTietLuanChuyenTB>> PostChiTietLuanChuyenTB(ChiTietLuanChuyenTB chiTietLuanChuyenTB)
        {
            _context.ChiTietLuanChuyenTB.Add(chiTietLuanChuyenTB);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChiTietLuanChuyenTB", new { maPhieuLC = chiTietLuanChuyenTB.MaPhieuLC, maThietBi = chiTietLuanChuyenTB.MaThietBi }, chiTietLuanChuyenTB);
        }

        // DELETE: api/ChiTietLuanChuyenTB/5/5
        [HttpDelete("{maPhieuLC}/{maThietBi}")]
        public async Task<IActionResult> DeleteChiTietLuanChuyenTB(string maPhieuLC, string maThietBi)
        {
            var chiTietLuanChuyenTB = await _context.ChiTietLuanChuyenTB.FindAsync(maPhieuLC, maThietBi);
            if (chiTietLuanChuyenTB == null)
            {
                return NotFound();
            }

            _context.ChiTietLuanChuyenTB.Remove(chiTietLuanChuyenTB);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/ChiTietLuanChuyenTB/maPhieuLC
        [HttpGet("{maPhieuLC}")]
        public async Task<ActionResult<IEnumerable<ChiTietLuanChuyenTB>>> GetChiTietLuanChuyenTBByMaPhieu(string maPhieuLC)
        {
            var chiTietLuanChuyenTB = await _context.ChiTietLuanChuyenTB
                .Where(c => c.MaPhieuLC == maPhieuLC)
                .ToListAsync();

            if (chiTietLuanChuyenTB == null || !chiTietLuanChuyenTB.Any())
            {
                return NotFound();
            }

            return chiTietLuanChuyenTB;
        }
        // PUT: api/ChiTietLuanChuyenTB/maPhieuLC/maThietBi
        [HttpPut("{maPhieuLC}/{maThietBi}")]
        public async Task<IActionResult> PutChiTietLuanChuyenTB(string maPhieuLC, string maThietBi, ChiTietLuanChuyenTB chiTietLuanChuyenTB)
        {
            // Check if the request body is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify that the IDs in the URL match the ID in the body
            if (maPhieuLC != chiTietLuanChuyenTB.MaPhieuLC || maThietBi != chiTietLuanChuyenTB.MaThietBi)
            {
                return BadRequest("ID mismatch.");
            }

            // Try to find the existing record
            var existingRecord = await _context.ChiTietLuanChuyenTB.FindAsync(maPhieuLC, maThietBi);
            if (existingRecord == null)
            {
                return NotFound();
            }

            // Update the existing record with new data
            existingRecord.MaPhongDen = chiTietLuanChuyenTB.MaPhongDen;
            existingRecord.MaPhongTu = chiTietLuanChuyenTB.MaPhongTu;

            // Mark the record as modified
            _context.Entry(existingRecord).State = EntityState.Modified;

            try
            {
                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietLuanChuyenTBExists(maPhieuLC, maThietBi))
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

        private bool ChiTietLuanChuyenTBExists(string maPhieuLC, string maThietBi)
        {
            return _context.ChiTietLuanChuyenTB.Any(e => e.MaPhieuLC == maPhieuLC && e.MaThietBi == maThietBi);
        }


    }
}
