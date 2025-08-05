using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class ThoiGianSuDung
    {
        [Key, Column(Order = 0)]
        public string? MaPhieuDK { get; set; }

        [Key, Column(Order = 1)]
        public string? MaThietBi {  get; set; }

        public string? MaNV { get; set; }  // Mã nhân viên phê duyệt

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public int SoGio {  get; set; }

        [ForeignKey("MaPhieuDK")]
        [JsonIgnore]
        public PhieuDangKi? PhieuDangKi { get; set; }  // Đối tượng phiếu đăng ký

        // Foreign key cho bảng DungCu
        [ForeignKey("MaThietBi")]
        [JsonIgnore]
        public ThietBi? ThietBi { get; set; }  // Đối tượng dụng cụ

        [ForeignKey("MaNV")]
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; }

    }
}
