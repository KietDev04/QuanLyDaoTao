using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyDaoTao.Models
{
    public class DeCuong
    {
        public int Id { get; set; }
        public string TenDeCuong { get; set; }
        public int KhoaId { get; set; }
        public Khoa Khoa { get; set; }
        public List<BaiGiang> BaiGiangs { get; set; }
    }
}