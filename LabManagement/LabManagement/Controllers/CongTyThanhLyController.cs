using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongTyThanhLyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CongTyThanhLyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CongTyThanhLy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CongTyThanhLy>>> GetAll()
        {
            return await _context.CongTyThanhLy.ToListAsync();
        }

        // GET: api/CongTyThanhLy/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CongTyThanhLy>> GetById(string id)
        {
            var congTy = await _context.CongTyThanhLy.FindAsync(id);

            if (congTy == null)
            {
                return NotFound();
            }

            return congTy;
        }

        // POST: api/CongTyThanhLy
        [HttpPost]
        public async Task<ActionResult<CongTyThanhLy>> Create(CongTyThanhLy congTyThanhLy)
        {
            if (_context.CongTyThanhLy.Any(e => e.MaCty == congTyThanhLy.MaCty))
            {
                return BadRequest("Công ty với mã này đã tồn tại.");
            }

            _context.CongTyThanhLy.Add(congTyThanhLy);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = congTyThanhLy.MaCty }, congTyThanhLy);
        }

        // PUT: api/CongTyThanhLy/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, CongTyThanhLy congTyThanhLy)
        {
            if (id != congTyThanhLy.MaCty)
            {
                return BadRequest("Mã công ty không khớp.");
            }

            _context.Entry(congTyThanhLy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CongTyThanhLyExists(id))
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

        // DELETE: api/CongTyThanhLy/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var congTy = await _context.CongTyThanhLy.FindAsync(id);

            if (congTy == null)
            {
                return NotFound();
            }

            _context.CongTyThanhLy.Remove(congTy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CongTyThanhLyExists(string id)
        {
            return _context.CongTyThanhLy.Any(e => e.MaCty == id);
        }
    }
}
