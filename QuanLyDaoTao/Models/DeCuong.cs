using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDaoTaoWeb.Models
{
    public class DeCuong
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "Mã đề cương")]
        public string MaDC { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã môn học")]
        public string MaMH { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Mục tiêu")]
        public string MucTieu { get; set; }

        [Required]
        [Display(Name = "Ngày cập nhật")]
        [DataType(DataType.Date)]
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;

        [ForeignKey("MaMH")]
        public virtual MonHoc MonHoc { get; set; }
    }
}