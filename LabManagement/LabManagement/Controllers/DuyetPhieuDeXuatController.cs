using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuyetPhieuDeXuatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DuyetPhieuDeXuatController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DuyetPhieuDeXuat
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var duyetPhieuList = await _context.DuyetPhieu
                .Include(d => d.PhieuDeXuat)
                .Include(d => d.NhanVien)
                .ToListAsync();

            return Ok(duyetPhieuList);
        }

     
        // GET: api/DuyetPhieuDeXuat/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var duyetPhieu = await _context.DuyetPhieu
                .Include(d => d.PhieuDeXuat)
                .Include(d => d.NhanVien)
                .FirstOrDefaultAsync(d => d.MaPhieu == id);

            if (duyetPhieu == null)
            {
                return NotFound("Phieu de xuat not found.");
            }

            return Ok(duyetPhieu);
        }
        //POST: api/DuyetPhieuDeXuat
        [HttpPost]
        public async Task<IActionResult> Create(DuyetPhieu duyetPhieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the MaPhieu already exists in the database
            var existingPhieu = await _context.DuyetPhieu.FindAsync(duyetPhieu.MaPhieu);
            if (existingPhieu != null)
            {
                return Conflict(new { message = "Phiếu đề xuất này đã được duyệt." });
            }

            _context.DuyetPhieu.Add(duyetPhieu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = duyetPhieu.MaPhieu }, duyetPhieu);
        }
                                  
    }
}
