using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDaoTaoWeb.Models
{
    public class DangKyLopHoc
    {
        public string MaSV { get; set; }
        public string MaLopHoc { get; set; }

        public DateTime NgayDangKy { get; set; } = DateTime.Now;

        // Navigation properties
        public SinhVien SinhVien { get; set; }
        public LopHoc LopHoc { get; set; }
    }
}