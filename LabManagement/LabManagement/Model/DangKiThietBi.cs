using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class DangKiThietBi
    {
        [Key, Column(Order = 0)]
        public string? MaPhieuDK { get; set; }

        [Key, Column(Order = 1)]
        public string? MaThietBi { get; set; }
        public DateTime? NgayDangKi { get; set; }

        public DateTime? NgaySuDung { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public string? TrangThaiSuDung {  get; set; }

        public string? TinhTrangSuDung { get; set; }
        public DateTime? NgayBatDauThucTe {  get; set; }
        public DateTime? NgayKetThucThucTe { get; set; }

        [ForeignKey("MaPhieuDK")]
        [JsonIgnore]
        public PhieuDangKi? PhieuDangKi { get; set; }  // Đối tượng phiếu đăng ký

        // Foreign key cho bảng DungCu
        [ForeignKey("MaThietBi")]
        [JsonIgnore]
        public ThietBi? ThietBi { get; set; }  // Đối tượng dụng cụ
    }
}
