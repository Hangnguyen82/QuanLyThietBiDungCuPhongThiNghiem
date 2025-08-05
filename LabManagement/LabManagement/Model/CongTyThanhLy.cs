using System.ComponentModel.DataAnnotations;

namespace LabManagement.Model
{
    public class CongTyThanhLy
    {
        [Key]
        public string? MaCty { get; set; }

        [Required]
        [MaxLength(100)]
        public string? TenCty { get; set; }

        [MaxLength(255)]
        public string? DiaChi { get; set; }
    }
}
