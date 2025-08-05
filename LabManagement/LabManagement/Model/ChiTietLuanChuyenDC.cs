using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class ChiTietLuanChuyenDC
    {
        [Key, Column(Order = 0)]
        public string? MaPhieuLC { get; set; } // Mã phiếu đề xuất

        [Key, Column(Order = 1)]
        public string? MaDungCu { get; set; } // Mã dụng cụ

        public string? MaPhongTu { get; set; } // Phòng hiện tại

        public string? MaPhongDen { get; set; } // Phòng chuyển đến

        public int SoLuong { get; set; } // Số lượng dụng cụ

        [ForeignKey("MaPhieuLC")]
        [JsonIgnore]
        public virtual PhieuDeXuatLuanChuyen? PhieuDeXuatLuanChuyen { get; set; }

        [ForeignKey("MaDungCu")]
        [JsonIgnore]
        public virtual DungCu? DungCu { get; set; }

        [ForeignKey("MaPhongTu")]
        [JsonIgnore]
        public virtual PhongThiNghiem? PhongTu { get; set; }

        [ForeignKey("MaPhongDen")]
        [JsonIgnore]
        public virtual PhongThiNghiem? PhongDen { get; set; }
    }
}
