using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    
    public class LichThietBi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLichTB { get; set; }

        public string? MaPhieuDK { get; set; }
        public string? MaThietBi { get; set; }
        public string? MaPhong { get; set; }
        public DateTime NgaySuDung { get; set; }
        public DateTime NgayKetThuc { get; set; }

        [ForeignKey("MaPhieuDK")]
        [JsonIgnore]
        public PhieuDangKi? PhieuDangKi { get; set; }

        [ForeignKey("MaPhong")]
        [JsonIgnore]
        public PhongThiNghiem? PhongThiNghiem { get; set; }
        [ForeignKey("MaThietBi")]
        [JsonIgnore]
        public ThietBi? ThietBi { get; set; }

    }
}
