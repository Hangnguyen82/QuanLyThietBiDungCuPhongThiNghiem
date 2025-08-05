using Microsoft.AspNetCore.Mvc;
using LabManagement.Model;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuBaoDuongController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhieuBaoDuongController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PhieuBaoDuong
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhieuBaoDuong>>> GetPhieuBaoDuong()
        {
            return await _context.PhieuBaoDuong.Include(p => p.NhanVien).ToListAsync();
        }

        // GET: api/PhieuBaoDuong/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PhieuBaoDuong>> GetPhieuBaoDuong(string id)
        {
            var phieuBaoDuong = await _context.PhieuBaoDuong
                                               .Include(p => p.NhanVien)
                                               .FirstOrDefaultAsync(p => p.MaPhieuBD == id);

            if (phieuBaoDuong == null)
            {
                return NotFound();
            }

            return phieuBaoDuong;
        }

        // POST: api/PhieuBaoDuong
        [HttpPost]
        public async Task<ActionResult<PhieuBaoDuong>> PostPhieuBaoDuong(PhieuBaoDuong phieuBaoDuong)
        {
            if (phieuBaoDuong == null)
            {
                return BadRequest();
            }

            _context.PhieuBaoDuong.Add(phieuBaoDuong);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPhieuBaoDuong), new { id = phieuBaoDuong.MaPhieuBD }, phieuBaoDuong);
        }
    }
}
