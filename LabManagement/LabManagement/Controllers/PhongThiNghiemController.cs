using LabManagement.Data;
using LabManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongThiNghiemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhongThiNghiemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PhongThiNghiem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhongThiNghiem>>> GetAllPhongThiNghiem()
        {
            try
            {
                var danhSach = await _context.PhongThiNghiem.ToListAsync();
                if (danhSach == null || !danhSach.Any())
                {
                    return NotFound(new { message = "Không tìm thấy danh sách phòng thí nghiệm." });
                }

                return Ok(danhSach);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi server.", error = ex.Message });
            }
        }

        // GET: api/PhongThiNghiem/{maPhong}
        [HttpGet("{maPhong}")]
        public async Task<ActionResult<PhongThiNghiem>> GetPhongThiNghiemByMaPhong(string maPhong)
        {
            try
            {
                var phongThiNghiem = await _context.PhongThiNghiem
                    .FirstOrDefaultAsync(p => p.MaPhong == maPhong);

                if (phongThiNghiem == null)
                {
                    return NotFound(new { message = $"Không tìm thấy phòng thí nghiệm với mã {maPhong}." });
                }

                return Ok(phongThiNghiem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi server.", error = ex.Message });
            }
        }
        // GET: api/PhongThiNghiem/{maPhong}/DungCu
        //[HttpGet("{maPhong}/DungCu")]
        //public async Task<ActionResult<IEnumerable<ThietBi>>> GetDungCuInPhongThiNghiem(string maPhong)
        //{
        //    if (string.IsNullOrEmpty(maPhong))
        //    {
        //        return BadRequest(new { message = "Mã phòng không được để trống." });
        //    }

        //    try
        //    {
        //        // Tìm các thiết bị trong phòng thí nghiệm
        //        var thietBiList = await _context.DungCu
        //            .Where(tb => tb.MaPhong == maPhong)
        //            .Include(tb => tb.LoaiDungCu)
        //            .Include(tb => tb.NhaCungCap)
        //            .Include(tb => tb.PhongThiNghiem)
        //            .ToListAsync();

        //        if (!thietBiList.Any())
        //        {
        //            return NotFound(new { message = $"Không có dụng cụ nào trong phòng thí nghiệm với mã {maPhong}." });
        //        }

        //        return Ok(thietBiList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Đã xảy ra lỗi server.", error = ex.Message });
        //    }
        //}

        //// GET: api/PhongThiNghiem/{maPhong}/ThietBi
        //[HttpGet("{maPhong}/ThietBi")]
        //public async Task<ActionResult<IEnumerable<ThietBi>>> GetThietBiInPhongThiNghiem(string maPhong)
        //{
        //    if (string.IsNullOrEmpty(maPhong))
        //    {
        //        return BadRequest(new { message = "Mã phòng không được để trống." });
        //    }

        //    try
        //    {
        //        // Tìm các thiết bị trong phòng thí nghiệm
        //        var thietBiList = await _context.ThietBi
        //            .Where(tb => tb.MaPhong == maPhong)
        //            .Include(tb => tb.LoaiThietBi)
        //            .Include(tb => tb.NhaCungCap)
        //            .Include(tb => tb.PhongThiNghiem)
        //            .ToListAsync();

        //        if (!thietBiList.Any())
        //        {
        //            return NotFound(new { message = $"Không có thiết bị nào trong phòng thí nghiệm với mã {maPhong}." });
        //        }

        //        return Ok(thietBiList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Đã xảy ra lỗi server.", error = ex.Message });
        //    }
        //}
        // GET: api/PhongThiNghiem/ThietBi/{maThietBi}
        [HttpGet("ThietBi/{maThietBi}")]
        public async Task<ActionResult<PhongThiNghiem>> GetPhongThiNghiemByDeviceId(string maThietBi)
        {
            if (string.IsNullOrEmpty(maThietBi))
            {
                return BadRequest(new { message = "Mã thiết bị không được để trống." });
            }

            try
            {
                // Tìm thiết bị với mã đã cho
                var thietBi = await _context.ThietBi
                    .Where(tb => tb.MaThietBi == maThietBi)
                    //.Include(tb => tb.PhongThiNghiem) // Lấy thông tin phòng thí nghiệm của thiết bị
                    .Include(tb => tb.LoaiThietBi)
                    .Include(tb => tb.NhaCungCap)
                    .FirstOrDefaultAsync();

                if (thietBi == null)
                {
                    return NotFound(new { message = $"Không tìm thấy thiết bị với mã {maThietBi}." });
                }

                return Ok(thietBi); // Trả về thông tin phòng thí nghiệm của thiết bị
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi server.", error = ex.Message });
            }
        }

        // GET: api/PhongThiNghiem/ThietBi/{maThietBi}
        [HttpGet("DungCu/{maDungCu}")]
        public async Task<ActionResult<PhongThiNghiem>> GetPhongThiNghiemByToolId(string maDungCu)
        {
            if (string.IsNullOrEmpty(maDungCu))
            {
                return BadRequest(new { message = "Mã dụng cụ không được để trống." });
            }

            try
            {
                // Tìm thiết bị với mã đã cho
                var dungCu = await _context.DungCu
                    .Where(tb => tb.MaDungCu == maDungCu)
                    .Include(tb => tb.LoaiDungCu)
                    .Include(tb => tb.NhaCungCap)
                    //.Include(tb => tb.PhongThiNghiem) // Lấy thông tin phòng thí nghiệm của thiết bị
                    .FirstOrDefaultAsync();

                if (dungCu == null)
                {
                    return NotFound(new { message = $"Không tìm thấy dụng cụ với mã {maDungCu}." });
                }

                return Ok(dungCu); // Trả về thông tin phòng thí nghiệm của thiết bị
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi server.", error = ex.Message });
            }
        }

    }
}
