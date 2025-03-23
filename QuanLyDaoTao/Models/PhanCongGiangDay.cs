using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDaoTaoWeb.Models
{
    public class PhanCongGiangDay
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "Mã phân công")]
        public string MaPCGD { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã giảng viên")]
        public string MaGV { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã môn học")]
        public string MaMH { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã lớp")]
        public string MaLop { get; set; }

        [Required]
        [Range(1, 3)]
        [Display(Name = "Học kỳ")]
        public int HocKy { get; set; }

        [Required]
        [Range(2000, 2026)]
        [Display(Name = "Năm học")]
        public int NamHoc { get; set; }

        [ForeignKey("MaGV")]
        public virtual GiangVien GiangVien { get; set; }

        [ForeignKey("MaMH")]
        public virtual MonHoc MonHoc { get; set; }

        [ForeignKey("MaLop")]
        public virtual LopHoc LopHoc { get; set; }
    }
}