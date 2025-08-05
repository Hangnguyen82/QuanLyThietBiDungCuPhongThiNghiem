using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class PhieuDangKi // đề xuất thiết bị mới
    {
        [Key]
        public string? MaPhieuDK { get; set; } // Mã phiếu đăng ký

        public string? MaNV { get; set; } // Mã nhân viên

        public string? MaPhong { get; set; }

        public DateTime? NgayLap { get; set; }  // Ngày sử dụng thiết bị

        public string? GhiChu { get; set; }  // Thông tin bổ sung

        public DateTime? NgayHoanTat { get; set; }  // Ngày hoàn thành/phê duyệt phiếu

        public string? TrangThai { get; set; } 

        public string? LyDoDK { get; set; }  // Lý do đề xuất (nếu có)


        // Foreign key cho bảng NhanVien
        [ForeignKey("MaNV")]
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; } // Đối tượng nhân viên

        [ForeignKey("MaPhong")]
        [JsonIgnore]
        public PhongThiNghiem? PhongThiNghiem { get; set; }


    }
}
