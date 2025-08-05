using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class PhieuDeXuat
    {
        [Key]
        public string? MaPhieu { get; set; }  // Mã phiếu đề xuất

        public string? MaNV { get; set; }  // Mã nhân viên tạo phiếu

        public DateTime? NgayTao { get; set; }  // Ngày tạo phiếu

        public string? LyDoDeXuat { get; set; }  // Lý do đề xuất (nếu có)

        public string? GhiChu { get; set; }  // Thông tin bổ sung
        public DateTime? NgayHoanTat { get; set; }  // Ngày hoàn thành/phê duyệt phiếu
        
        public string? TrangThai { get; set; }  // Trạng thái phiếu (mặc định là 'Chưa phê duyệt')


        // Foreign key cho bảng NhanVien
        [ForeignKey("MaNV")]
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; }  // Đối tượng nhân viên

    }
}
