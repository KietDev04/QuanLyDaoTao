using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTaoWeb.Models
{
    public class ChuongTrinhDaoTao
    {
        [Key]
        [StringLength(10)]
        public string MaCTDT { get; set; }

        [Required]
        [StringLength(100)]
        public string TenCTDT { get; set; }

        [Range(2000, 2025)] // Điều chỉnh năm tối đa theo thời điểm hiện tại
        public int NamBatDau { get; set; }

        [StringLength(10)]
        public string MaKhoa { get; set; }

        // Navigation property
        public Khoa Khoa { get; set; }
    }
}