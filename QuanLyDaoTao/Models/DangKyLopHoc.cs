using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDaoTaoWeb.Models
{
    public class DangKyLopHoc
    {
        [Required]
        [StringLength(10)]
        [Display(Name = "Mã sinh viên")]
        public string MaSV { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã lớp học")]
        public string MaLopHoc { get; set; }

        [Required]
        [Display(Name = "Ngày đăng ký")]
        [DataType(DataType.DateTime)]
        public DateTime NgayDangKy { get; set; } = DateTime.Now;

        [ForeignKey("MaSV")]
        public virtual SinhVien SinhVien { get; set; }

        [ForeignKey("MaLopHoc")]
        public virtual LopHoc LopHoc { get; set; }
    }
}