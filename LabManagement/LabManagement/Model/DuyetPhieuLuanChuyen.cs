using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class DuyetPhieuLuanChuyen
    {
        [Key]
        public string? MaPhieuLC { get; set; } // Mã phiếu luân chuyển

        public string? MaNV { get; set; } // Mã nhân viên

        public DateTime? NgayDuyet { get; set; } // Ngày duyệt

        public string? TrangThai { get; set; } // Trạng thái phê duyệt

        public string? LyDoTuChoi { get; set; } // Lý do từ chối

        [ForeignKey("MaPhieuLC")]
        [JsonIgnore]
        public virtual PhieuDeXuatLuanChuyen? PhieuDeXuatLuanChuyen { get; set; }

        [ForeignKey("MaNV")]
        [JsonIgnore]
        public virtual NhanVien? NhanVien { get; set; }
    }
}
