using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDaoTaoWeb.Models
{
    public class BaiGiang
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "Mã bài giảng")]
        public string MaBG { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã môn học")]
        public string MaMH { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }

        [Required]
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        [ForeignKey("MaMH")]
        public virtual MonHoc MonHoc { get; set; }
    }
}