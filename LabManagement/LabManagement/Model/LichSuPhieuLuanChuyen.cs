using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class LichSuPhieuLuanChuyen
    {
        [Key]
        public int MaLichSu { get; set; } // Mã lịch sử (tự động tăng)

        public string? MaPhieuLC { get; set; } // Mã phiếu luân chuyển

        public string? TrangThaiTruoc { get; set; } // Trạng thái trước khi thay đổi

        public string? TrangThaiSau { get; set; } // Trạng thái sau khi thay đổi

        public DateTime NgayThayDoi { get; set; } = DateTime.Now; // Thời gian thay đổi

        public string? MaNV { get; set; } // Mã nhân viên thực hiện thay đổi

        [ForeignKey("MaPhieuLC")]
        [JsonIgnore]
        public virtual PhieuDeXuatLuanChuyen? PhieuDeXuatLuanChuyen { get; set; }

        [ForeignKey("MaNV")]
        [JsonIgnore]
        public virtual NhanVien? NhanVien { get; set; }
    }
}
