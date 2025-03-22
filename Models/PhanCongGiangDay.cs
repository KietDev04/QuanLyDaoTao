using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTaoWeb.Models
{
    public class PhanCongGiangDay
    {
        [Key]
        [StringLength(10)]
        public string MaPCGD { get; set; }

        [StringLength(10)]
        public string MaGV { get; set; }

        [StringLength(10)]
        public string MaMH { get; set; }

        [StringLength(10)]
        public string MaLop { get; set; }

        [Range(1, 3)]
        public int HocKy { get; set; }

        [Range(2000, 2026)] // Điều chỉnh năm tối đa
        public int NamHoc { get; set; }

        // Navigation properties
        public GiangVien GiangVien { get; set; }
        public MonHoc MonHoc { get; set; }
        public LopHoc LopHoc { get; set; }
    }
}