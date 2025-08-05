using Microsoft.AspNetCore.Mvc;
using LabManagement.Data;
using LabManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuDangKiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhieuDangKiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhieuDKi()
        {
            var phieuDeXuats = await _context.PhieuDangKi
                .Include(p => p.NhanVien)
                .Include(p => p.PhongThiNghiem)
                .ToListAsync();

            return Ok(phieuDeXuats);
        }
        // POST: api/PhieuDKi
        [HttpPost]
        public async Task<IActionResult> CreatePhieuDKi([FromBody] PhieuDangKi newPhieuDKi)
        {


            try
            {
                if (newPhieuDKi == null)
                {
                    return BadRequest("Phieu Dang Ki cannot be null.");
                }

                if (string.IsNullOrEmpty(newPhieuDKi.MaNV))
                {
                    return BadRequest("Both MaThietBi and MaNV are required.");
                }

                _context.PhieuDangKi.Add(newPhieuDKi);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPhieuDKi), new { id = newPhieuDKi.MaPhieuDK }, newPhieuDKi);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("existing")]
        public async Task<IActionResult> GetExistingPhieuDKi()
        {
            var phieuDKis = await _context.PhieuDangKi.Select(p => new { p.MaPhieuDK }).ToListAsync();
            return Ok(phieuDKis);
        }
        // GET: api/PhieuDKi/{maPhieu}
        [HttpGet("{maPhieu}")]
        public async Task<IActionResult> GetPhieuDKiByMaPhieu(string maPhieu)
        {
            // Find the proposal with the given MaPhieu
            var phieuDKi = await _context.PhieuDangKi
                .Include(p => p.NhanVien) // Include NhanVien details
                .Include(p => p.PhongThiNghiem)
                .FirstOrDefaultAsync(p => p.MaPhieuDK == maPhieu); // Search by MaPhieu

            // If the proposal is not found, return a 404 Not Found
            if (phieuDKi == null)
            {
                return NotFound($"PhieuDangKi with MaPhieu '{maPhieu}' not found.");
            }

            // Return the found proposal details
            return Ok(phieuDKi);
        }

        //[HttpPost("approve/{maPhieuDK}")]
        //public async Task<IActionResult> ApprovePhieuDangKi(string maPhieuDK)
        //{
        //    // Tìm phiếu đăng ký theo mã
        //    var phieuDangKi = await _context.PhieuDangKi
        //        .FirstOrDefaultAsync(p => p.MaPhieuDK == maPhieuDK);

        //    if (phieuDangKi == null)
        //    {
        //        return NotFound(new { message = "Phiếu đăng ký không tồn tại." });
        //    }

        //    // Kiểm tra nếu phiếu đã được phê duyệt
        //    if (phieuDangKi.TrangThai == "Đã phê duyệt")
        //    {
        //        return BadRequest(new { message = "Đơn đăng ký đã được phê duyệt." });
        //    }

        //    // Cập nhật trạng thái của phiếu đăng ký thành "Đã phê duyệt"
        //    phieuDangKi.TrangThai = "Đã phê duyệt";
        //    phieuDangKi.NgayHoanTat = DateTime.Now;

        //    // Lưu cập nhật vào bảng PhieuDangKi
        //    _context.PhieuDangKi.Update(phieuDangKi);
        //    await _context.SaveChangesAsync();

        //    // Lấy thông tin đăng ký thiết bị từ bảng DangKiThietBi
        //    var dangKiThietBis = await _context.DangKiThietBi
        //        .Where(d => d.MaPhieuDK == maPhieuDK)
        //        .ToListAsync();

        //    // Cập nhật thông tin vào bảng LichThietBi
        //    foreach (var thietBi in dangKiThietBis)
        //    {
        //        // Tạo mới một bản ghi trong bảng LichThietBi
        //        var lichThietBi = new LichThietBi
        //        {
        //            MaThietBi = thietBi.MaThietBi,
        //            MaPhong = phieuDangKi.MaPhong,
        //            NgaySuDung = thietBi.NgaySuDung ?? DateTime.Now,  // Nếu ngày sử dụng không có thì mặc định là ngày hiện tại
        //            NgayKetThuc = thietBi.NgayKetThuc ?? DateTime.Now.AddDays(1) // Nếu ngày kết thúc không có thì mặc định là ngày hiện tại cộng 1 ngày
        //        };

        //        // Thêm bản ghi vào bảng LichThietBi
        //        _context.LichThietBi.Add(lichThietBi);
        //        await _context.SaveChangesAsync();
        //    }

        //    // Lấy thông tin đăng ký dụng cụ từ bảng DangKiDungCu
        //    var dangKiDungCus = await _context.DangKiDungCu
        //        .Where(d => d.MaPhieuDK == maPhieuDK)
        //        .ToListAsync();

        //    // Cập nhật thông tin vào bảng LichDungCu
        //    foreach (var dungCu in dangKiDungCus)
        //    {
        //        // Tạo mới một bản ghi trong bảng LichDungCu
        //        var lichDungCu = new LichDungCu
        //        {
        //            MaDungCu = dungCu.MaDungCu,
        //            MaPhong = phieuDangKi.MaPhong,
        //            NgaySuDung = dungCu.NgaySuDung ?? DateTime.Now,  // Nếu ngày sử dụng không có thì mặc định là ngày hiện tại
        //            NgayKetThuc = dungCu.NgayKetThuc ?? DateTime.Now.AddDays(1), // Nếu ngày kết thúc không có thì mặc định là ngày hiện tại cộng 1 ngày
        //            SoLuong = dungCu.SoLuong
        //        };

        //        // Thêm bản ghi vào bảng LichDungCu
        //        _context.LichDungCu.Add(lichDungCu);
        //        await _context.SaveChangesAsync();
        //    }

        //    // Lưu thay đổi vào cơ sở dữ liệu
        //    await _context.SaveChangesAsync();

        //    // Trả về phản hồi cho người dùng
        //    return Ok(new { message = "Đơn đăng ký đã được phê duyệt và lịch thiết bị, dụng cụ đã được cập nhật." });
        //}
        // Method to update the LichThietBi (equipment schedule)
        private async Task UpdateLichThietBi(string maPhieuDK, string maPhong)
        {
            var dangKiThietBis = await _context.DangKiThietBi
                .Where(d => d.MaPhieuDK == maPhieuDK)
                .ToListAsync();

            foreach (var thietBi in dangKiThietBis)
            {
                var lichThietBi = new LichThietBi
                {
                    MaPhieuDK = maPhieuDK,
                    MaThietBi = thietBi.MaThietBi,
                    MaPhong = maPhong,
                    NgaySuDung = thietBi.NgaySuDung ?? DateTime.Now,
                    NgayKetThuc = thietBi.NgayKetThuc ?? DateTime.Now.AddDays(1)
                };

                _context.LichThietBi.Add(lichThietBi);
            }

            await _context.SaveChangesAsync();
        }

        // Method to update the LichDungCu (tool schedule)
        private async Task UpdateLichDungCu(string maPhieuDK, string maPhong)
        {
            var dangKiDungCus = await _context.DangKiDungCu
                .Where(d => d.MaPhieuDK == maPhieuDK)
                .ToListAsync();

            foreach (var dungCu in dangKiDungCus)
            {
                var lichDungCu = new LichDungCu
                {
                    MaPhieuDK = maPhieuDK,
                    MaDungCu = dungCu.MaDungCu,
                    MaPhong = maPhong,
                    NgaySuDung = dungCu.NgaySuDung ?? DateTime.Now,
                    NgayKetThuc = dungCu.NgayKetThuc ?? DateTime.Now.AddDays(1),
                    SoLuong = dungCu.SoLuong
                };

                _context.LichDungCu.Add(lichDungCu);
            }

            await _context.SaveChangesAsync();
        }

        // POST: api/PhieuDKi/approve/{maPhieuDK}
        [HttpPost("approve/{maPhieuDK}")]
        public async Task<IActionResult> ApprovePhieuDangKi(string maPhieuDK)
        {
            var phieuDangKi = await _context.PhieuDangKi
                .FirstOrDefaultAsync(p => p.MaPhieuDK == maPhieuDK);

            if (phieuDangKi == null)
            {
                return NotFound(new { message = "Phiếu đăng ký không tồn tại." });
            }

            // Check if the proposal is already approved
            if (phieuDangKi.TrangThai == "Đã phê duyệt")
            {
                return BadRequest(new { message = "Đơn đăng ký đã được phê duyệt." });
            }

            // Update the status of the proposal to "Approved"
            phieuDangKi.TrangThai = "Đã phê duyệt";
            phieuDangKi.NgayHoanTat = DateTime.Now;

            // Save changes to the PhieuDangKi table
            _context.PhieuDangKi.Update(phieuDangKi);
            await _context.SaveChangesAsync();

            // Update LichThietBi (equipment schedule)
            await UpdateLichThietBi(maPhieuDK, phieuDangKi.MaPhong);

            // Update LichDungCu (tool schedule)
            await UpdateLichDungCu(maPhieuDK, phieuDangKi.MaPhong);

            return Ok(new { message = "Đơn đăng ký đã được phê duyệt và lịch thiết bị, dụng cụ đã được cập nhật." });
        }

        

        [HttpGet("get-dung-cu-in-lab")]
        public async Task<IActionResult> GetDungCuInLab()
        {
            try
            {
                // Fetch the list of registration forms with required details, and join with the equipment table
                var phieuDangKiList = await _context.PhieuDangKi
                    .Where(p => p.PhongThiNghiem != null) // Ensure there is a laboratory linked
                    .Select(p => new
                    {
                        p.PhongThiNghiem.MaPhong,  // Room/Laboratory ID
                       
                        DungCuList = _context.DangKiDungCu // Join with the table containing the equipment
                            .Where(d => d.MaPhieuDK == p.MaPhieuDK) // Filter based on the registration ID
                            .Select(d => new
                            {
                                d.MaDungCu,        // Equipment ID
                                d.SoLuong,         // Quantity
                                d.NgaySuDung,      // Usage date of equipment
                                d.NgayKetThuc      // End date of equipment usage
                            })
                            .ToList()
                    })
                    .ToListAsync();

                // Return the list of required details
                return Ok(phieuDangKiList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("get-thiet-bi-in-lab")]
        public async Task<IActionResult> GetThietBiInLab()
        {
            try
            {
                // Fetch the list of registration forms with required details, and join with the equipment table
                var phieuDangKiList = await _context.PhieuDangKi
                    .Where(p => p.PhongThiNghiem != null) // Ensure there is a laboratory linked
                    .Select(p => new
                    {
                        p.PhongThiNghiem.MaPhong,  // Room/Laboratory ID

                        DungCuList = _context.DangKiThietBi // Join with the table containing the equipment
                            .Where(d => d.MaPhieuDK == p.MaPhieuDK) // Filter based on the registration ID
                            .Select(d => new
                            {
                                d.MaThietBi,        // Equipment ID
                               
                                d.NgaySuDung,      // Usage date of equipment
                                d.NgayKetThuc      // End date of equipment usage
                            })
                            .ToList()
                    })
                    .ToListAsync();

                // Return the list of required details
                return Ok(phieuDangKiList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PhieuDangKi updatedPhieuDangKi)
        {
            if (id != updatedPhieuDangKi.MaPhieuDK)
            {
                return BadRequest();
            }

            _context.Entry(updatedPhieuDangKi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PhieuDangKi.Any(e => e.MaPhieuDK == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }


    }
}
