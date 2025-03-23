using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDaoTaoWeb.Models
{
    public class DanhGia
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "Mã đánh giá")]
        public string MaDG { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã sinh viên")]
        public string MaSV { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã môn học")]
        public string MaMH { get; set; }

        [Required]
        [Range(1, 10)]
        [Display(Name = "Điểm đánh giá")]
        public int DiemDanhGia { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Nhận xét")]
        public string NhanXet { get; set; }

        [Required]
        [Display(Name = "Ngày đánh giá")]
        [DataType(DataType.Date)]
        public DateTime NgayDanhGia { get; set; } = DateTime.Now;

        [ForeignKey("MaSV")]
        public virtual SinhVien SinhVien { get; set; }
        
        [ForeignKey("MaMH")]
        public virtual MonHoc MonHoc { get; set; }
    }
}