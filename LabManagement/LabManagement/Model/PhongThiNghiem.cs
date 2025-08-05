using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManagement.Model
{
    public class PhongThiNghiem
    {
        [Key]
        [MaxLength(20)]
        public string? MaPhong { get; set; }

        [MaxLength(50)]
        public string? LoaiPhong { get; set; }

        [MaxLength(50)]
        public string? ChucNang { get; set; }
    }
}
