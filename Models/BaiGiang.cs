using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyDaoTao.Models
{
    public class BaiGiang
    {
        public int Id { get; set; }
        public string TenBaiGiang { get; set; }
        public string NoiDung { get; set; }
        public string FilePDF { get; set; } // Đường dẫn file PDF
        public string VideoUrl { get; set; } // Đường dẫn video
        public int DeCuongId { get; set; }
        public DeCuong DeCuong { get; set; }
    }
}