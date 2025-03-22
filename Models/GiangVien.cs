using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTaoWeb.Models
{
    public class GiangVien
    {
        [Key]
        [StringLength(10)]
        public string MaGV { get; set; }

        [Required]
        [StringLength(100)]
        public string? HoTen { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(10)]
        public string MaKhoa { get; set; }

        public DateTime NgayNhanViec { get; set; } = new DateTime(2023, 1, 1);

        // Navigation property
        public Khoa Khoa { get; set; }
    }
}