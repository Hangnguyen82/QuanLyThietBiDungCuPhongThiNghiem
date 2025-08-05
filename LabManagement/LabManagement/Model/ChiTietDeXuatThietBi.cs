using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class ChiTietDeXuatThietBi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCTDeXuatTB { get; set; } // Primary Key with Auto-Increment]
        public string? MaPhieu { get; set; } // Mã phiếu
        public string? MaLoaiThietBi { get; set; } // Mã loại thiết bị
        public string? TenThietBi { get; set; } // Tên loại thiết bị được đề xuất
        public int SoLuongDeXuat { get; set; } // Số lượng thiết bị đề xuất
        public string? MoTa { get; set; } // Mô tả loại thiết bị

        // Quan hệ với bảng PhieuDeXuat
        [ForeignKey("MaPhieu")]
        [JsonIgnore]
        public PhieuDeXuat? PhieuDeXuat { get; set; }

        // Quan hệ với bảng LoaiThietBi
        [ForeignKey("MaLoaiThietBi")]
        [JsonIgnore]
        public LoaiThietBi? LoaiThietBi { get; set; }
    }
}
