using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDaoTaoWeb.Models;

namespace QuanLyDaoTaoWeb.Controllers
{
    [Authorize(Roles = "GiangVien")]
    public class GiangVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GiangVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult IndexGiangVien()
        {
            return View();
        }

        // Xem danh sách BaiGiang
        public IActionResult BaiGiangIndex()
        {
            var baiGiangList = _context.BaiGiang.ToList();
            return View("BaiGiangIndex", baiGiangList);
        }

        // Xem danh sách DanhGia
        public IActionResult DanhGiaIndex()
        {
            var danhGiaList = _context.DanhGia.ToList();
            return View("DanhGiaIndex", danhGiaList);
        }

        // Sửa DanhGia
        public IActionResult EditDanhGia(string id)
        {
            var danhGia = _context.DanhGia.Find(id);
            if (danhGia == null) return NotFound();
            return View("EditDanhGia", danhGia);
        }

        [HttpPost]
        public IActionResult EditDanhGia(DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                _context.DanhGia.Update(danhGia);
                _context.SaveChanges();
                return RedirectToAction("DanhGiaIndex");
            }
            return View("EditDanhGia", danhGia);
        }

        // Xem danh sách ChuongTrinhDaoTao
        public IActionResult ChuongTrinhDaoTaoIndex()
        {
            var ctdtList = _context.ChuongTrinhDaoTao.ToList();
            return View("ChuongtrinhDaoTaoIndex", ctdtList);
        }


        // Xem danh sách SinhVien
        public IActionResult SinhVienIndex()
        {
            var sinhVienList = _context.SinhVien.ToList();
            return View("SinhVienIndex", sinhVienList);
        }

        // Xem danh sách MonHoc
        public IActionResult MonHocIndex()
        {
            var monHocList = _context.MonHoc.ToList();
            return View("MonHocIndex", monHocList);
        }

        // Xem danh sách Khoa
        public IActionResult KhoaIndex()
        {
            var khoaList = _context.Khoa.ToList();
            return View("KhoaIndex", khoaList);
        }

        // Xem danh sách PhanCongGiangDay
        public IActionResult PhanCongGiangDayIndex()
        {
            var pcgdList = _context.PhanCongGiangDay.ToList();
            return View("PhanCongGiangDayIndex", pcgdList);
        }

        // Xem danh sách LopHoc
        public IActionResult LopHocIndex()
        {
            var lopHocList = _context.LopHoc.ToList();
            return View("LopHocIndex", lopHocList);
        }

        // Xem danh sách DeCuong
        public IActionResult DeCuongIndex()
        {
            var deCuongList = _context.DeCuong.ToList();
            return View("DeCuongIndex", deCuongList);
        }

        // Xem danh sách TaiLieu
        public IActionResult TaiLieuIndex()
        {
            var taiLieuList = _context.TaiLieu.ToList();
            return View("TaiLieuIndex", taiLieuList);
        }
    }
}