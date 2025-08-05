using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class ThietBi
    {
        public string? MaThietBi { get; set; } // Mã thiết bị

        public string? TenThietBi { get; set; } // Tên thiết bị

        public string? MaLoaiThietBi { get; set; } // Mã loại thiết bị

        public string? TinhTrang { get; set; } // Tình trạng

        public DateTime? NgayCapNhat { get; set; } // Ngày cập nhật

        public DateTime? NgaySX { get; set; } // Ngày sản xuất

        public string? NhaSX { get; set; } // Nhà sản xuất

        public DateTime? NgayBaoHanh { get; set; } // Ngày bảo hành

        public string? XuatXu { get; set; } // Xuất xứ

        public string? MaNCC { get; set; }

        public string? MaPhong { get; set; }

        public string? HinhAnh { get; set; } // Đường dẫn hình ảnh
        public bool isDeleted { get; set; } = false;

        [NotMapped]
        public string? HinhAnhUrl => string.IsNullOrEmpty(HinhAnh) ? null : $"http://localhost:5123/images/ThietBi/{HinhAnh}";

        // Foreign key cho bảng LoaiThietBi
        [ForeignKey("MaLoaiThietBi")]
        [JsonIgnore]
        public LoaiThietBi? LoaiThietBi { get; set; } // Đối tượng loại thiết bị

        // Foreign key cho bảng NhaCungCap
        [ForeignKey("MaNCC")]
        [JsonIgnore]
        public NhaCungCap? NhaCungCap { get; set; } // Đối tượng nhà cung cấp
        

        [ForeignKey("MaPhong")]
        [JsonIgnore]
        public PhongThiNghiem? PhongThiNghiem { get; set; }

    }
}
