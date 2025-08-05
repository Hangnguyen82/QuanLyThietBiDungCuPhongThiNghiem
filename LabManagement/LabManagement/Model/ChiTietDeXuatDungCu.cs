using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class ChiTietDeXuatDungCu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCTDeXuatDC { get; set; } // Primary Key with Auto-Increment
        public string? MaPhieu { get; set; } // Mã phiếu
        public string? MaLoaiDC { get; set; } // Mã loại dụng cụ
        public string? TenDungCu { get; set; } // Tên dụng cụ
        public int SoLuongDeXuat { get; set; } // Số lượng đề xuất
        public string? MoTa { get; set; } // Mô tả

        // Quan hệ với bảng PhieuDeXuat
        [ForeignKey("MaPhieu")]
        [JsonIgnore]
        public PhieuDeXuat? PhieuDeXuat { get; set; }

        // Quan hệ với bảng LoaiDungCu
        [ForeignKey("MaLoaiDC")]
        [JsonIgnore]
        public LoaiDungCu? LoaiDungCu { get; set; }
    }
}
