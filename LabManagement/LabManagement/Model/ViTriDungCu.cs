using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace LabManagement.Model
{
    public class ViTriDungCu
    {
        public string? MaDungCu { get; set; }
        public string? MaPhong { get; set; }

        public int? SoLuong { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("MaDungCu")]
        [JsonIgnore]
        public DungCu? DungCu { get; set; }

        [ForeignKey("MaPhong")]
        [JsonIgnore]
        public PhongThiNghiem? PhongThiNghiem { get; set; }
    }
}
