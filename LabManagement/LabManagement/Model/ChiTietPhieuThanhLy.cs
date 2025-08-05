using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class ChiTietPhieuThanhLy
    {
        [Key, Column(Order = 0)]
        public string? MaPhieuTL { get; set; }

        [Key, Column(Order = 1)]
        public string? MaThietBi { get; set; }

        public decimal GiaTL { get; set; }

        public string? LyDo { get; set; }

        [ForeignKey("MaPhieuTL")]
        [JsonIgnore]
        public virtual PhieuThanhLy? PhieuThanhLy { get; set; }

        [ForeignKey("MaThietBi")]
        [JsonIgnore]
        public virtual ThietBi? ThietBi { get; set; }
    }
}
