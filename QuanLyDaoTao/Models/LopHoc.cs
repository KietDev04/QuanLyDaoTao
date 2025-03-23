using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDaoTaoWeb.Models
{
    public class LopHoc
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "Mã lớp")]
        public string MaLop { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên lớp")]
        public string TenLop { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã chương trình đào tạo")]
        public string MaCTDT { get; set; }

        [ForeignKey("MaCTDT")]
        public virtual ChuongTrinhDaoTao ChuongTrinhDaoTao { get; set; }

        public virtual ICollection<DangKyLopHoc> DangKyLopHocs { get; set; }
        public virtual ICollection<PhanCongGiangDay> PhanCongGiangDays { get; set; }
    }
}