using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class PhieuPhanBoTB
    {
        [Key]
        [MaxLength(20)]
        public string? MaPhieu { get; set; }

        [Required]
        [MaxLength(20)]
        public string? MaThietBi { get; set; }

        [Required]
        [MaxLength(20)]
        public string? MaNV { get; set; }

        [MaxLength(255)]
        public string? NoiDung { get; set; }

        public DateTime? NgayLap { get; set; }

        [MaxLength(50)]
        public string? TrangThai { get; set; }

        [ForeignKey("MaNV")]
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; } // Đối tượng nhân viên

        [ForeignKey("MaThietBi")]
        [JsonIgnore]
        public ThietBi? ThietBi { get; set; } // Đối tượng nhân viên
    }
}
