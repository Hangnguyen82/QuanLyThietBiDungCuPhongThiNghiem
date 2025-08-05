using Microsoft.AspNetCore.Mvc;
using LabManagement.Data;
using LabManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangKyThietBiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DangKyThietBiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DangKiDungCu
        [HttpGet]
        public async Task<IActionResult> GetChiTietDangKiThietBi()
        {
            var chiTietDangKiDungCus = await _context.DangKiThietBi
                .Include(c => c.PhieuDangKi) // Include PhieuDeXuat for detailed info
                .Include(c => c.ThietBi)      // Include DungCu for detailed info
                .ToListAsync();

            return Ok(chiTietDangKiDungCus);
        }

        // POST: api/DangKiDungCu
        [HttpPost]
        public async Task<IActionResult> CreateDangKiThietBi([FromBody] DangKiThietBi newChiTiet)
        {
            try
            {
                if (newChiTiet == null)
                {
                    return BadRequest("Dang Ki Thiet Bi cannot be null.");
                }

                // Validate that both MaPhieu and MaDungCu are provided
                if (string.IsNullOrEmpty(newChiTiet.MaPhieuDK) || string.IsNullOrEmpty(newChiTiet.MaThietBi))
                {
                    return BadRequest("Both MaPhieu and MaDungCu are required.");
                }

                // Ensure the PhieuDanGKi exists
                var phieuDangKi = await _context.PhieuDangKi.FindAsync(newChiTiet.MaPhieuDK);
                if (phieuDangKi == null)
                {
                    return NotFound("DangKiThietBi not found.");
                }

                // Ensure the DungCu exists
                var dungCu = await _context.ThietBi.FindAsync(newChiTiet.MaThietBi);
                if (dungCu == null)
                {
                    return NotFound("ThietBi not found.");
                }

                _context.DangKiThietBi.Add(newChiTiet);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetChiTietDangKiThietBi), new { maPhieu = newChiTiet.MaPhieuDK, maDungCu = newChiTiet.MaThietBi }, newChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        // GET: api/ChiTietDeXuatDungCu/{maPhieu}
        [HttpGet("{maPhieu}")]
        public async Task<IActionResult> GetDangKiThietBiByMaPhieu(string maPhieu)
        {
            // Retrieve the details of ChiTietDeXuatDungCu for the specific MaPhieu
            var chiTietDangKiDungCus = await _context.DangKiThietBi
                .Include(c => c.PhieuDangKi)  // Include PhieuDeXuat for detailed info
                .Include(c => c.ThietBi)       // Include DungCu for detailed info
                .Where(c => c.MaPhieuDK == maPhieu) // Filter by MaPhieu
                .ToListAsync();

            if (chiTietDangKiDungCus == null || chiTietDangKiDungCus.Count == 0)
            {
                return NotFound("No details found for the given MaPhieu.");
            }

            return Ok(chiTietDangKiDungCus);
        }

        // Phương thức cập nhật trạng thái thiết bị sử dụng PUT
        [HttpPut("update-device-status/{maPhieuDK}")]
        public async Task<IActionResult> UpdateDeviceStatus(string maPhieuDK, [FromBody] DeviceStatusUpdateRequest request)
        {
            try
            {
                // Kiểm tra phiếu đăng ký
                var phieuDangKi = await _context.PhieuDangKi.FindAsync(maPhieuDK);
                if (phieuDangKi == null)
                {
                    // Trả về lỗi dưới dạng JSON
                    return NotFound(new { message = "Phiếu đăng ký không tồn tại." });
                }

                // Cập nhật trạng thái trong bảng DangKiThietBi
                var dangKiThietBi = await _context.DangKiThietBi
                    .Where(d => d.MaPhieuDK == maPhieuDK)
                    .ToListAsync();

                if (dangKiThietBi.Count == 0)
                {
                    // Trả về lỗi dưới dạng JSON
                    return NotFound(new { message = "Không tìm thấy thông tin thiết bị." });
                }

                // Lặp qua tất cả thiết bị và cập nhật trạng thái
                foreach (var item in dangKiThietBi)
                {
                    // Trạng thái đang sử dụng
                    if (request.TrangThaiSuDung == "Đang sử dụng" && item.TrangThaiSuDung != "Đang sử dụng")
                    {
                        item.TrangThaiSuDung = "Đang sử dụng";
                        item.NgayBatDauThucTe = DateTime.Now;  // Ghi nhận thời gian bắt đầu
                    }
                    // Trạng thái hoàn thành sử dụng
                    else if (request.TrangThaiSuDung == "Hoàn thành sử dụng" && item.TrangThaiSuDung == "Đang sử dụng")
                    {
                        item.TrangThaiSuDung = "Hoàn thành sử dụng";
                        item.NgayKetThucThucTe = DateTime.Now;  // Ghi nhận thời gian kết thúc
                    }
                    // Trạng thái quá hạn sử dụng
                    else if (request.TrangThaiSuDung == "Quá hạn sử dụng" && item.TrangThaiSuDung != "Quá hạn sử dụng")
                    {
                        if (item.NgayKetThucThucTe.HasValue && item.NgayKetThucThucTe < DateTime.Now)
                        {
                            item.TrangThaiSuDung = "Quá hạn sử dụng";
                        }
                        else
                        {
                            return BadRequest(new { message = "Chưa đến thời điểm quá hạn sử dụng." });
                        }
                        
                    }
                    item.TinhTrangSuDung = request.TinhTrangSuDung;

                    _context.DangKiThietBi.Update(item);
                }

                // Lưu lại các thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();

                // Trả về thông báo thành công dưới dạng JSON
                return Ok(new { message = "Cập nhật trạng thái thành công." });
            }
            catch (Exception ex)
            {
                // Trả về lỗi nội bộ dưới dạng JSON
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }


        // Đối tượng nhận vào yêu cầu cập nhật trạng thái thiết bị
        public class DeviceStatusUpdateRequest
        {
            public string? TrangThaiSuDung { get; set; }  // Trạng thái mới
            public string? TinhTrangSuDung { get; set; }
        }


    }
}
