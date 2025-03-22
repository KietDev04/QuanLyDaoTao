using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTaoWeb.Models
{
    public class DanhGia
    {
        [Key]
        [StringLength(10)]
        public string MaDG { get; set; }

        [StringLength(10)]
        public string MaSV { get; set; }

        [StringLength(10)]
        public string MaMH { get; set; }

        [Range(1, 10)]
        public int DiemDanhGia { get; set; }

        [StringLength(500)]
        public string NhanXet { get; set; }

        public DateTime NgayDanhGia { get; set; } = DateTime.Now;

        // Navigation properties
        public SinhVien SinhVien { get; set; }
        public MonHoc MonHoc { get; set; }
    }
}