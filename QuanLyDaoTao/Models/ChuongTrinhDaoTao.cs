using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDaoTaoWeb.Models
{
    public class ChuongTrinhDaoTao
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "Mã chương trình")]
        public string MaCTDT { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên chương trình")]
        public string TenCTDT { get; set; }

        [Required]
        [Range(2000, 2030)]
        [Display(Name = "Năm bắt đầu")]
        public int NamBatDau { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã khoa")]
        public string MaKhoa { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; } = true;

        [ForeignKey("MaKhoa")]
        public virtual Khoa Khoa { get; set; }

        public virtual ICollection<LopHoc> LopHocs { get; set; }
    }
}