using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace LabManagement.Model
{
    public class QuanLyGioTB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaQuanLyTB { get; set; }

        public string? MaPhieuDK { get; set; }
        public string? MaThietBi { get; set; }
        public string? MaPhong { get; set; }
        public string? soGioSuDungThucTe { get; set; }

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
