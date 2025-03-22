using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTaoWeb.Models
{
    public class LopHoc
    {
        [Key]
        [StringLength(10)]
        public string MaLop { get; set; }

        [Required]
        [StringLength(50)]
        public string? TenLop { get; set; }

        [StringLength(10)]
        public string MaCTDT { get; set; }

        // Navigation property
        public ChuongTrinhDaoTao ChuongTrinhDaoTao { get; set; }
    }
}