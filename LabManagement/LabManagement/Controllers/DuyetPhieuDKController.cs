using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuyetPhieuDKController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DuyetPhieuDKController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DuyetPhieuDKi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var duyetPhieuList = await _context.DuyetPhieuDK
                .Include(d => d.PhieuDangKi)
                .Include(d => d.NhanVien)
                .ToListAsync();

            return Ok(duyetPhieuList);
        }


        // GET: api/DuyetPhieuDKi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var duyetPhieu = await _context.DuyetPhieuDK
                .Include(d => d.PhieuDangKi)
                .Include(d => d.NhanVien)
                .FirstOrDefaultAsync(d => d.MaPhieuDK == id);

            if (duyetPhieu == null)
            {
                return NotFound("Phieu dang ki not found.");
            }

            return Ok(duyetPhieu);
        }
        //POST: api/DuyetPhieuDeXuat
        [HttpPost]
        public async Task<IActionResult> Create(DuyetPhieuDK duyetPhieu)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the MaPhieu already exists in the database
                var existingPhieu = await _context.DuyetPhieuDK.FindAsync(duyetPhieu.MaPhieuDK);
                if (existingPhieu != null)
                {
                    return Conflict(new { message = "Phiếu đăng kí này đã được duyệt." });
                }

                _context.DuyetPhieuDK.Add(duyetPhieu);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = duyetPhieu.MaPhieuDK }, duyetPhieu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
    }
