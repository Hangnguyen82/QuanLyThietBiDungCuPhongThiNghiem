using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LabManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanLyGioDCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuanLyGioDCController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả các bản ghi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuanLyGioDC>>> GetAll()
        {
            return await _context.QuanLyGioDC
                .Include(q => q.PhieuDangKi)
                .Include(q => q.PhongThiNghiem)
                .Include(q => q.DungCu)
                .ToListAsync();
        }

        // Lấy chi tiết một bản ghi theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<QuanLyGioDC>> GetById(int id)
        {
            var quanLyGioDC = await _context.QuanLyGioDC
                .Include(q => q.PhieuDangKi)
                .Include(q => q.PhongThiNghiem)
                .Include(q => q.DungCu)
                .FirstOrDefaultAsync(q => q.MaQuanLyDC == id);

            if (quanLyGioDC == null)
            {
                return NotFound();
            }

            return quanLyGioDC;
        }

        // Thêm mới một bản ghi
        [HttpPost]
        public async Task<ActionResult<QuanLyGioDC>> AddQuanLyGioDC(QuanLyGioDC quanLyGioDC)
        {
            _context.QuanLyGioDC.Add(quanLyGioDC);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = quanLyGioDC.MaQuanLyDC }, quanLyGioDC);
        }

        // Cập nhật thông tin một bản ghi theo ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuanLyGioDC(int id, QuanLyGioDC quanLyGioDC)
        {
            if (id != quanLyGioDC.MaQuanLyDC)
            {
                return BadRequest();
            }

            _context.Entry(quanLyGioDC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuanLyGioDCExists(id))
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

        // Xóa một bản ghi theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuanLyGioDC(int id)
        {
            var quanLyGioDC = await _context.QuanLyGioDC.FindAsync(id);
            if (quanLyGioDC == null)
            {
                return NotFound();
            }

            _context.QuanLyGioDC.Remove(quanLyGioDC);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Kiểm tra xem một bản ghi có tồn tại hay không
        private bool QuanLyGioDCExists(int id)
        {
            return _context.QuanLyGioDC.Any(e => e.MaQuanLyDC == id);
        }
    }
}
