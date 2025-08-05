using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class DuyetPhieuDK
    {
        [Required]
        [Key]
        public string? MaPhieuDK { get; set; }  // Mã phiếu đăng ký

        public string? MaNV { get; set; }  // Mã nhân viên phê duyệt

        public DateTime? NgayDuyet { get; set; }  // Ngày phê duyệt

        public string? TrangThai { get; set; }  // Trạng thái phê duyệt

        public string? LyDoTuChoi { get; set; }  // Lý do từ chối hoặc lý do phê duyệt

        // Foreign key cho bảng PhieuDangKi
        [ForeignKey("MaPhieuDK")]
        [JsonIgnore]
        public PhieuDangKi? PhieuDangKi { get; set; }  // Đối tượng phiếu đăng ký

        // Foreign key cho bảng NhanVien
        [ForeignKey("MaNV")]
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; }  // Đối tượng nhân viên phê duyệt
    }
}
