using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDaoTaoWeb.Models
{
    public class TaiLieu
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "Mã tài liệu")]
        public string MaTL { get; set; }

        [Required(ErrorMessage = "Tên tài liệu là bắt buộc")]
        [StringLength(200)]
        [Display(Name = "Tên tài liệu")]
        public string TenTL { get; set; }

        [Required(ErrorMessage = "Bài giảng là bắt buộc")]
        [StringLength(10)]
        [Display(Name = "Mã bài giảng")]
        public string MaBG { get; set; }

        [StringLength(500)]
        [Display(Name = "Đường dẫn")]
        [RegularExpression(@"^/files/.*", ErrorMessage = "Đường dẫn phải bắt đầu bằng /files/")]
        public string? DuongDan { get; set; }

        [ForeignKey("MaBG")]
        public BaiGiang BaiGiang { get; set; }
    }
}