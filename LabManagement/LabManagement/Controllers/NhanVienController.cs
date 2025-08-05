using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabManagement.Data;
using LabManagement.Model;
using LabManagement.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BCrypt.Net;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public NhanVienController(ApplicationDbContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        // GET: api/NhanVien
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetNhanViens()
        {
            return await _context.NhanVien
                .Include(nv => nv.ChucVu)       // Include ChucVu navigation property
                .Include(nv => nv.NhomQuyen)    // Include NhomQuyen navigation property
                .ToListAsync();
        }

        //[HttpPost]
        //public async Task<ActionResult<NhanVien>> PostNhanVien(NhanVien nhanVien)
        //{
        //    // Băm mật khẩu trước khi lưu
        //    nhanVien.MatKhau = BCrypt.Net.BCrypt.HashPassword(nhanVien.MatKhau);

        //    _context.NhanVien.Add(nhanVien);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetNhanViens), new { id = nhanVien.MaNV }, nhanVien);
        //}

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var nhanVien = await _context.NhanVien
                .Include(nv => nv.NhomQuyen)
                .FirstOrDefaultAsync(nv => nv.Email == loginRequest.Email);

            if (nhanVien == null)
            {
                return Unauthorized(new { message = "Email hoặc mật khẩu không đúng" });
            }

            try
            {
                if (BCrypt.Net.BCrypt.Verify(loginRequest.MatKhau, nhanVien.MatKhau))
                {
                    var token = _jwtTokenService.GenerateJwtToken(nhanVien);
                    Console.WriteLine($"Generated Token: {token}"); // Log the token

                    return Ok(new
                    {
                        Token = token,
                        Role = nhanVien.NhomQuyen?.TenNhom,
                        EmployeeName = nhanVien.TenNV,
                        maNV = nhanVien.MaNV
                    });
                }
                else
                {
                    return Unauthorized(new { message = "Email hoặc mật khẩu không đúng" });
                }
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // Nếu salt không tương thích, băm lại mật khẩu và cập nhật trong DB
                Console.WriteLine("Salt không tương thích, tiến hành cập nhật mật khẩu");

                // Băm lại mật khẩu
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(loginRequest.MatKhau);

                // Cập nhật mật khẩu mới vào cơ sở dữ liệu
                nhanVien.MatKhau = hashedPassword;
                _context.NhanVien.Update(nhanVien);
                await _context.SaveChangesAsync();

                var token = _jwtTokenService.GenerateJwtToken(nhanVien);
                return Ok(new
                {
                    Token = token,
                    Role = nhanVien.NhomQuyen?.TenNhom,
                    EmployeeName = nhanVien.TenNV
                });
            }
        }






        // Secure endpoints based on roles
        [Authorize(Policy = "GiamDocTrungTam")] // Giám đốc trung tâm
        [HttpGet("giam-doc-trung-tam")]
        public IActionResult ForQuanLyPTN()
        {
            return Ok("Giám đốc trung tâm");
        }

        [Authorize(Policy = "ChuyenVien")] // Chuyên viên phòng thí nghiệm
        [HttpGet("chuyen-vien")]
        public IActionResult ForChuyenVien()
        {
            return Ok("Chuyên viên phòng thí nghiệm");
        }

        [Authorize(Policy = "NguoiDung")] // Người dùng (Học sinh/Giáo viên)
        [HttpGet("nguoi-dung")]
        public IActionResult ForNguoiDung()
        {
            return Ok("Người dùng - Học sinh/Giáo viên");
        }
        // GET: api/NhanVien/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<NhanVien>> GetNhanVienById(string id)
        {
            // Tìm nhân viên theo mã với các thuộc tính liên quan
            var nhanVien = await _context.NhanVien
                .Include(nv => nv.ChucVu)       // Include ChucVu navigation property
                .Include(nv => nv.NhomQuyen)   // Include NhomQuyen navigation property
                .FirstOrDefaultAsync(nv => nv.MaNV == id);

            // Nếu không tìm thấy nhân viên
            if (nhanVien == null)
            {
                return NotFound(new { message = "Không tìm thấy nhân viên với mã này" });
            }

            // Trả về thông tin nhân viên
            return Ok(nhanVien);
        }
        // POST: api/NhanVien
        [HttpPost]
        public async Task<ActionResult<NhanVien>> PostNhanVien(NhanVien nhanVien)
        {
            // Hash the password before saving
            nhanVien.MatKhau = BCrypt.Net.BCrypt.HashPassword(nhanVien.MatKhau);

            _context.NhanVien.Add(nhanVien);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNhanVienById), new { id = nhanVien.MaNV }, nhanVien);
        }

        // PUT: api/NhanVien/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhanVien(string id, NhanVien nhanVien)
        {
            if (id != nhanVien.MaNV)
            {
                return BadRequest(new { message = "Mã nhân viên không khớp" });
            }

            // Hash the password if it has been updated
            if (!string.IsNullOrEmpty(nhanVien.MatKhau))
            {
                nhanVien.MatKhau = BCrypt.Net.BCrypt.HashPassword(nhanVien.MatKhau);
            }

            _context.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.NhanVien.Any(e => e.MaNV == id))
                {
                    return NotFound(new { message = "Không tìm thấy nhân viên với mã này" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("reactivate/{id}")]
        public async Task<IActionResult> ReactivateNhanVien(string id)
        {
            var nhanVien = await _context.NhanVien.FirstOrDefaultAsync(nv => nv.MaNV == id);

            if (nhanVien == null)
            {
                return NotFound(new { message = "Không tìm thấy nhân viên với mã này" });
            }

            nhanVien.isDeleted = true; 
            _context.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

    }
}