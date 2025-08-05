using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagement.Model
{
    public class NhanVien
    {
        public string? MaNV { get; set; }
        public string? TenNV { get; set; }
        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDT { get; set; }

        // Thêm Email và Mật khẩu
        public string? Email { get; set; }
        public string? MatKhau { get; set; }
        public bool isDeleted { get; set; } = false;

        // Foreign key for ChucVu
        public string? MaCV { get; set; }
        [ForeignKey("MaCV")]
        [JsonIgnore]
        public ChucVu? ChucVu { get; set; }

        // Foreign key for NhomQuyen
        public string? MaNhom { get; set; }
        [ForeignKey("MaNhom")]
        [JsonIgnore]
        public NhomQuyen? NhomQuyen { get; set; }

    }
}