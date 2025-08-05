using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class PhieuDeXuatLuanChuyen
    {
        [Key]
        public string? MaPhieuLC { get; set; } // Mã phiếu đề xuất

        public DateTime NgayTao { get; set; } // Ngày tạo phiếu

        public string TrangThai { get; set; } = "Đang xử lý"; // Trạng thái mặc định

        public string? MaNV { get; set; } // Người đề xuất luân chuyển

        public string? GhiChu { get; set; } // Ghi chú thêm

        public DateTime? NgayLuanChuyen { get; set; } // Ngày dự kiến luân chuyển

        public DateTime? NgayHoanTat { get; set; } // Ngày hoàn tất luân chuyển

        [ForeignKey("MaNV")]
        [JsonIgnore]
        public virtual NhanVien? NhanVien { get; set; }

    }
}
