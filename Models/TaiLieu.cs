using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTaoWeb.Models
{
    public class TaiLieu
    {
        [Key]
        [StringLength(10)]
        public string MaTL { get; set; }

        [StringLength(10)]
        public string MaBG { get; set; }

        [Required]
        [StringLength(200)]
        public string? TenTL { get; set; }

        [StringLength(500)]
        [RegularExpression(@"^/files/.*", ErrorMessage = "Đường dẫn phải bắt đầu bằng /files/")]
        public string? DuongDan { get; set; }

        // Navigation property
        public BaiGiang BaiGiang { get; set; }
    }
}