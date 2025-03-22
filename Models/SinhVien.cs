using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTaoWeb.Models
{
    public class SinhVien
    {
        [Key]
        [StringLength(10)]
        public string MaSV { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(10)]
        public string MaKhoa { get; set; }

        // Navigation property
        public Khoa Khoa { get; set; }
    }
}