using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class DuyetPhieu
    {
        [Required]
        [Key]
        public string MaPhieu { get; set; } // Mã phiếu

        public string MaNV { get; set; } // Mã nhân viên duyệt phiếu

        public DateTime? NgayDuyet { get; set; } // Ngày duyệt phiếu

        public string TrangThai { get; set; } // Trạng thái duyệt phiếu

        public string LyDoTuChoi { get; set; } // Lý do từ chối phê duyệt, nếu có

        // Quan hệ với bảng PhieuDeXuat
        [ForeignKey("MaPhieu")]
        [JsonIgnore]
        public PhieuDeXuat? PhieuDeXuat { get; set; }

        // Quan hệ với bảng NhanVien
        [ForeignKey("MaNV")]
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; }
    }
}

