using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class ChiTietNhapTB
    {
        [Key, Column(Order = 0)]
        public string? MaPhieuNhap { get; set; }

        [Key, Column(Order = 1)]
        public string? MaThietBi { get; set; }

        public decimal GiaNhap { get; set; }

        [ForeignKey("MaPhieuNhap")]
        [JsonIgnore]
        public PhieuNhap? PhieuNhap { get; set; }

        [ForeignKey("MaThietBi")]
        [JsonIgnore]
        public ThietBi? ThietBi { get; set; }
    }
}
