using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class ChiTietLuanChuyenTB
    {
        [Key, Column(Order = 0)]
        public string? MaPhieuLC { get; set; } // Mã phiếu đề xuất

        [Key, Column(Order = 1)]
        public string? MaThietBi { get; set; } // Mã thiết bị

        public string? MaPhongTu { get; set; } // Phòng hiện tại

        public string? MaPhongDen { get; set; } // Phòng chuyển đến

        [ForeignKey("MaPhieuLC")]
        [JsonIgnore]
        public virtual PhieuDeXuatLuanChuyen? PhieuDeXuatLuanChuyen { get; set; }

        [ForeignKey("MaThietBi")]
        [JsonIgnore]
        public virtual ThietBi? ThietBi { get; set; }

        [ForeignKey("MaPhongTu")]
        [JsonIgnore]
        public virtual PhongThiNghiem? PhongTu { get; set; }

        [ForeignKey("MaPhongDen")]
        [JsonIgnore]
        public virtual PhongThiNghiem? PhongDen { get; set; }
    }
}
