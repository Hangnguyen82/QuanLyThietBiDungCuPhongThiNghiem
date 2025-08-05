using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class LichSuPhieuDeXuat
    {
        [Key]
        public int MaLichSu { get; set; } // Mã lịch sử (tự tăng)

        [Required]
        [StringLength(20)]
        public string? MaPhieu { get; set; } // Mã phiếu

        [StringLength(50)]
        public string? TrangThaiTruoc { get; set; } // Trạng thái trước khi thay đổi

        [Required]
        [StringLength(50)]
        public string? TrangThaiSau { get; set; } // Trạng thái sau khi thay đổi

        [Required]
        [DataType(DataType.DateTime)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? NgayThayDoi { get; set; } // Thời gian thay đổi

        [StringLength(20)]
        public string? MaNV { get; set; } // Mã nhân viên thực hiện thay đổi

        // Navigation Properties
        [ForeignKey("MaPhieu")]
        [JsonIgnore]
        public virtual PhieuDeXuat? PhieuDeXuat { get; set; }

        [ForeignKey("MaNV")]
        [JsonIgnore]
        public virtual NhanVien? NhanVien { get; set; }
    }
}
