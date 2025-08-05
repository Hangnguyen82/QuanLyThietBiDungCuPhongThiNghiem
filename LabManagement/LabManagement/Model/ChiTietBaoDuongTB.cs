using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class ChiTietBaoDuongTB
    {
        public string MaPhieuBD { get; set; }  

        public string MaThietBi { get; set; } 

        public decimal DonGia { get; set; }  

        [ForeignKey("MaPhieuBD")]
        [JsonIgnore]
        public PhieuBaoDuong? PhieuBaoDuong { get; set; }  

        [ForeignKey("MaThietBi")]
        [JsonIgnore]
        public ThietBi? ThietBi { get; set; } 
    }
}
