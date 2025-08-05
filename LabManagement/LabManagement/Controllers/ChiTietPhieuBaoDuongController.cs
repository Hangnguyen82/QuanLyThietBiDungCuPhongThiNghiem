using Microsoft.AspNetCore.Mvc;
using LabManagement.Model;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietPhieuBaoDuongController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChiTietPhieuBaoDuongController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChiTietPhieuBaoDuong
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietBaoDuongTB>>> GetChiTietPhieuBaoDuong()
        {
            var chiTietPhieuBaoDuong = await _context.ChiTietBaoDuongTB
                .Include(c => c.PhieuBaoDuong)
                .Include(c => c.ThietBi)
                .ToListAsync();

            return Ok(chiTietPhieuBaoDuong);
        }

        // GET: api/ChiTietPhieuBaoDuong/{maPhieuBD}/{maThietBi}
        [HttpGet("{maPhieuBD}/{maThietBi}")]
        public async Task<ActionResult<ChiTietBaoDuongTB>> GetChiTietPhieuBaoDuong(string maPhieuBD, string maThietBi)
        {
            var chiTietPhieuBaoDuong = await _context.ChiTietBaoDuongTB
                .Include(c => c.PhieuBaoDuong)
                .Include(c => c.ThietBi)
                .FirstOrDefaultAsync(c => c.MaPhieuBD == maPhieuBD && c.MaThietBi == maThietBi);

            if (chiTietPhieuBaoDuong == null)
            {
                return NotFound();
            }

            return Ok(chiTietPhieuBaoDuong);
        }

        // POST: api/ChiTietPhieuBaoDuong
        [HttpPost]
        public async Task<ActionResult<ChiTietBaoDuongTB>> PostChiTietPhieuBaoDuong(ChiTietBaoDuongTB chiTietPhieuBaoDuong)
        {
            if (chiTietPhieuBaoDuong == null)
            {
                return BadRequest();
            }

            _context.ChiTietBaoDuongTB.Add(chiTietPhieuBaoDuong);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChiTietPhieuBaoDuong), new { maPhieuBD = chiTietPhieuBaoDuong.MaPhieuBD, maThietBi = chiTietPhieuBaoDuong.MaThietBi }, chiTietPhieuBaoDuong);
        }

        private bool ChiTietPhieuBaoDuongExists(string maPhieuBD, string maThietBi)
        {
            return _context.ChiTietBaoDuongTB.Any(c => c.MaPhieuBD == maPhieuBD && c.MaThietBi == maThietBi);
        }
        // New Method: GET api/ChiTietPhieuBaoDuong/byMaPhieuBD/{maPhieuBD}
        [HttpGet("byMaPhieuBD/{maPhieuBD}")]
        public async Task<ActionResult<IEnumerable<ChiTietBaoDuongTB>>> GetChiTietPhieuBaoDuongByMaPhieuBD(string maPhieuBD)
        {
            var chiTietPhieuBaoDuongList = await _context.ChiTietBaoDuongTB
                .Include(c => c.PhieuBaoDuong)
                .Include(c => c.ThietBi)
                .Where(c => c.MaPhieuBD == maPhieuBD)
                .ToListAsync();

            if (chiTietPhieuBaoDuongList == null || !chiTietPhieuBaoDuongList.Any())
            {
                return NotFound($"No ChiTietPhieuBaoDuong found with MaPhieuBD: {maPhieuBD}");
            }

            return Ok(chiTietPhieuBaoDuongList);
        }
        // New Method: GET api/ChiTietPhieuBaoDuong/byMaThietBi/{maThietBi}
        [HttpGet("byMaThietBi/{maThietBi}")]
        public async Task<ActionResult<IEnumerable<ChiTietBaoDuongTB>>> GetChiTietPhieuBaoDuongByMaThietBi(string maThietBi)
        {
            // Fetching the ChiTietPhieuBaoDuong details for the given maThietBi
            var chiTietPhieuBaoDuongList = await _context.ChiTietBaoDuongTB
                .Include(c => c.PhieuBaoDuong)
                .Include(c => c.ThietBi)
                .Where(c => c.MaThietBi == maThietBi)
                .ToListAsync();

            if (chiTietPhieuBaoDuongList == null || !chiTietPhieuBaoDuongList.Any())
            {
                return NotFound($"No ChiTietPhieuBaoDuong found with MaThietBi: {maThietBi}");
            }

            return Ok(chiTietPhieuBaoDuongList);
        }

    }
}
