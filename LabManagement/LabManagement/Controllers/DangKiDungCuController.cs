using Microsoft.AspNetCore.Mvc;
using LabManagement.Data;
using LabManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangKiDungCuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DangKiDungCuController(ApplicationDbContext context)
        {
             _context = context;
        }

        // GET: api/DangKiDungCu
        [HttpGet]
        public async Task<IActionResult> GetChiTietDangKiDungCu()
        {
            var chiTietDangKiDungCus = await _context.DangKiDungCu
                .Include(c => c.PhieuDangKi) // Include PhieuDeXuat for detailed info
                .Include(c => c.DungCu)      // Include DungCu for detailed info
                .ToListAsync();

            return Ok(chiTietDangKiDungCus);
        }

        // POST: api/DangKiDungCu
        [HttpPost]
        public async Task<IActionResult> CreateDangKiDungCu([FromBody] DangKiDungCu newChiTiet)
        {
            try
            {
                if (newChiTiet == null)
                {
                    return BadRequest("Dang Ki Dung Cu cannot be null.");
                }

                // Validate that both MaPhieu and MaDungCu are provided
                if (string.IsNullOrEmpty(newChiTiet.MaPhieuDK) || string.IsNullOrEmpty(newChiTiet.MaDungCu))
                {
                    return BadRequest("Both MaPhieu and MaDungCu are required.");
                }

                // Ensure the PhieuDanGKi exists
                var phieuDangKi = await _context.PhieuDangKi.FindAsync(newChiTiet.MaPhieuDK);
                if (phieuDangKi == null)
                {
                    return NotFound("DangKiDungCu not found.");
                }

                // Ensure the DungCu exists
                var dungCu = await _context.DungCu.FindAsync(newChiTiet.MaDungCu);
                if (dungCu == null)
                {
                    return NotFound("DungCu not found.");
                }

                _context.DangKiDungCu.Add(newChiTiet);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetChiTietDangKiDungCu), new { maPhieu = newChiTiet.MaPhieuDK, maDungCu = newChiTiet.MaDungCu }, newChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        // GET: api/ChiTietDeXuatDungCu/{maPhieu}
        [HttpGet("{maPhieu}")]
        public async Task<IActionResult> GetDangKiDungCuByMaPhieu(string maPhieu)
        {
            // Retrieve the details of ChiTietDeXuatDungCu for the specific MaPhieu
            var chiTietDangKiDungCus = await _context.DangKiDungCu
                .Include(c => c.PhieuDangKi)  // Include PhieuDeXuat for detailed info
                .Include(c => c.DungCu)       // Include DungCu for detailed info
                .Where(c => c.MaPhieuDK == maPhieu) // Filter by MaPhieu
                .ToListAsync();

            if (chiTietDangKiDungCus == null || chiTietDangKiDungCus.Count == 0)
            {
                return NotFound("No details found for the given MaPhieu.");
            }

            return Ok(chiTietDangKiDungCus);
        }
        // Phương thức cập nhật trạng thái thiết bị sử dụng PUT
        [HttpPut("update-tool-status/{maPhieuDK}")]
        public async Task<IActionResult> UpdateToolStatus(string maPhieuDK, [FromBody] ToolStatusUpdateRequest request)
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
                var dangKiDungCu = await _context.DangKiDungCu
                    .Where(d => d.MaPhieuDK == maPhieuDK)
                    .ToListAsync();

                if (dangKiDungCu.Count == 0)
                {
                    // Trả về lỗi dưới dạng JSON
                    return NotFound(new { message = "Không tìm thấy thông tin dụng cụ." });
                }

                // Lặp qua tất cả thiết bị và cập nhật trạng thái
                foreach (var item in dangKiDungCu)
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
                    _context.DangKiDungCu.Update(item);
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
        public class ToolStatusUpdateRequest
        {
            public string? TrangThaiSuDung { get; set; }  // Trạng thái mới
            public string? TinhTrangSuDung { get; set; }
        }


    }
}
