using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class PhieuBaoDuong
    {
        [Key]
        public string? MaPhieuBD { get; set; }  // Mã phiếu bảo dưỡng

        public string? MaNV { get; set; }  // Mã nhân viên
        public string? MaNCC { get; set; }
        public string? NoiDung { get; set; }  // Nội dung bảo dưỡng

        public DateTime? NgayBaoDuong { get; set; }  // Ngày bảo dưỡng

        public decimal TongTien { get; set; }  // Tổng tiền bảo dưỡng

        [ForeignKey("MaNV")]
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; }  // Tham chiếu đến nhân viên
        [ForeignKey("MaNCC")]
        [JsonIgnore]
        public NhaCungCap? NhaCungCap { get; set; }  // Tham chiếu đến nhân viên
    }
}
