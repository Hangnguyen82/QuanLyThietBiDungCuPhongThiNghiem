using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class ChiTietNhapDC
    {
        [Key, Column(Order = 0)]
        public string? MaPhieuNhap { get; set; }  

        [Key, Column(Order = 1)]
        public string? MaDungCu { get; set; }  

        public decimal GiaNhap { get; set; }  

        public int SoLuongNhap { get; set; } 

        [ForeignKey("MaPhieuNhap")]
        [JsonIgnore]
        public PhieuNhap? PhieuNhap { get; set; } 

        // Foreign key cho bảng DungCu
        [ForeignKey("MaDungCu")]
        [JsonIgnore]
        public DungCu? DungCu { get; set; } 
    }
}
