using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class PhieuNhap
    {
        [Key]
        public string? MaPhieuNhap { get; set; }  

        public string? MaNV { get; set; }  

        public DateTime? NgayNhap { get; set; }  

        public decimal TongTien { get; set; }  
        public string? MaPhieu { get; set; }

        [ForeignKey("MaNV")]
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; }
        [ForeignKey("MaPhieu")]
        [JsonIgnore]
        public PhieuDeXuat? PhieuDeXuat { get; set; }
    }
}
