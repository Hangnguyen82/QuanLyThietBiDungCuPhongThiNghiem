using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ImageUploadController : ControllerBase
{
    private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image");

    public ImageUploadController()
    {
        // Đảm bảo các thư mục đã tồn tại
        Directory.CreateDirectory(Path.Combine(_basePath, "DungCu"));
        Directory.CreateDirectory(Path.Combine(_basePath, "ThietBi"));
    }

    // Phương thức chung để upload file
    private async Task<IActionResult> UploadFile(IFormFile file, string folder)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Không có tệp hình ảnh nào được chọn.");
        }

        // Xác định đường dẫn lưu file
        var folderPath = Path.Combine(_basePath, folder);
        var filePath = Path.Combine(folderPath, file.FileName);

        // Lưu file vào thư mục tương ứng
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Trả về đường dẫn URL mà người dùng có thể sử dụng để truy cập file
        return Ok(new { FilePath = $"/images/{folder}/{file.FileName}" });
    }

    // Endpoint upload DungCu
    [HttpPost("uploadDungCu")]
    public Task<IActionResult> UploadDungCu(IFormFile file)
    {
        return UploadFile(file, "DungCu");
    }

    // Endpoint upload ThietBi
    [HttpPost("uploadThietBi")]
    public Task<IActionResult> UploadThietBi(IFormFile file)
    {
        return UploadFile(file, "ThietBi");
    }
}
