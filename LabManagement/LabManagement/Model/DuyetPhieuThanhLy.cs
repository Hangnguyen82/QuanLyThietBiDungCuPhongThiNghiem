using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class DuyetPhieuThanhLy
    {
        [Key, Column(Order = 0)]
        public string? MaPhieuTL { get; set; }

        [Key, Column(Order = 1)]
        public string? MaNV { get; set; }

        public DateTime? NgayDuyet { get; set; }

        public string? TrangThai { get; set; }

        public string? LyDoTuChoi { get; set; }

        public string? GhiChu { get; set; }

        [ForeignKey("MaPhieuTL")]
        [JsonIgnore]
        public virtual PhieuThanhLy? PhieuThanhLy { get; set; }

        [ForeignKey("MaNV")]
        [JsonIgnore]
        public virtual NhanVien? NhanVien { get; set; }
    }
}
