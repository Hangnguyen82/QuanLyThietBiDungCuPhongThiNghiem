using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class PhieuThanhLy
    {
        [Key]
        public string? MaPhieuTL { get; set; }

        public string? MaCty { get; set; }

        public string? MaNV { get; set; }

        public DateTime? NgayLapPhieu { get; set; }

        public string? TrangThai { get; set; }

        public string? LyDoChung { get; set; }

        public decimal? TongTien { get; set; }

        public DateTime? NgayHoanTat { get; set; }

        public string? TrangThaiThanhLy { get; set; } = "Chưa hoàn thành";

        [ForeignKey("MaCty")]
        [JsonIgnore]
        public virtual CongTyThanhLy? CongTyThanhLy { get; set; }

        [ForeignKey("MaNV")]
        [JsonIgnore]
        public virtual NhanVien? NhanVien { get; set; }
    }
}
