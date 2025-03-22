using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTaoWeb.Models
{
    public class MonHoc
    {
        [Key]
        [StringLength(10)]
        public string MaMH { get; set; }

        [Required]
        [StringLength(100)]
        public string TenMH { get; set; }

        [Range(1, 6)]
        public int SoTinChi { get; set; }

        [StringLength(10)]
        public string MaKhoa { get; set; }

        // Navigation property
        public Khoa Khoa { get; set; }
    }
}