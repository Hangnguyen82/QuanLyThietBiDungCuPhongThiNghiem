using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class DungCu
    {
        
        public string? MaDungCu { get; set; } // Mã dụng cụ

        
        public string? TenDungCu { get; set; } // Tên dụng cụ

       
        public string? MaLoaiDC { get; set; } // Mã loại dụng cụ

        
        public int SoLuong { get; set; } // Số lượng

        
        public string? TinhTrang { get; set; } // Tình trạng

       
        public DateTime? NgayCapNhat { get; set; } // Ngày cập nhật

        public DateTime? NgaySX { get; set; } // Ngày sản xuất

        
        public string? NhaSX { get; set; } // Nhà sản xuất

        public DateTime? NgayBaoHanh { get; set; } // Ngày bảo hành

        public string? XuatXu { get; set; } // Xuất xứ

        
        public string? MaNCC { get; set; } // Mã nhà cung cấp


        // Thuộc tính hình ảnh

        public string? HinhAnh { get; set; } // Đường dẫn hình ảnh
        public bool IsDeleted { get; set; } = false;
        // Thuộc tính ảo để lấy URL hình ảnh
        [NotMapped]
        public string? HinhAnhUrl => string.IsNullOrEmpty(HinhAnh) ? null : $"http://localhost:5123/images/DungCu/{HinhAnh}";
        // Foreign key cho bảng LoaiDungCu
        [ForeignKey("MaLoaiDC")]
        [JsonIgnore]
        public LoaiDungCu? LoaiDungCu { get; set; } // Đối tượng loại dụng cụ

        // Foreign key cho bảng NhaCungCap
        [ForeignKey("MaNCC")]
        [JsonIgnore]
        public NhaCungCap? NhaCungCap { get; set; } // Đối tượng nhà cung cấp

    }
}
