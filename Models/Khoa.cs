using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyDaoTao.Models
{
    public class Khoa
    {
        public int Id { get; set; }
        public string TenKhoa { get; set; }
        public string MoTa { get; set; }
        public List<DeCuong> DeCuongs { get; set; }
    }
}