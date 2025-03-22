using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDaoTaoWeb.Models;

namespace QuanLyDaoTaoWeb.Controllers
{
    [Authorize(Roles = "SinhVien")]
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinhVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult IndexSinhVien()
        {
            return View();
        }

        // Xem danh sách BaiGiang
        public IActionResult BaiGiangIndex()
        {
            var baiGiangList = _context.BaiGiang.ToList();
            return View("BaiGiangIndex", baiGiangList);
        }

        // Xem danh sách DangKyLopHoc
        public IActionResult DangKyLopHocIndex()
        {
            var dangKyLopHocList = _context.DangKyLopHoc.ToList();
            return View("DangKyLopHocIndex", dangKyLopHocList);
        }

        // Sửa DangKyLopHoc
        public IActionResult EditDangKyLopHoc(string maSV, string maLopHoc)
        {
            var dangKyLopHoc = _context.DangKyLopHoc
                .FirstOrDefault(d => d.MaSV == maSV && d.MaLopHoc == maLopHoc);
            if (dangKyLopHoc == null) return NotFound();
            return View("EditDangKyLopHoc", dangKyLopHoc);
        }

        [HttpPost]
        public IActionResult EditDangKyLopHoc(DangKyLopHoc dangKyLopHoc)
        {
            if (ModelState.IsValid)
            {
                // Lấy bản ghi hiện tại từ cơ sở dữ liệu
                var existingDangKy = _context.DangKyLopHoc
                    .FirstOrDefault(d => d.MaSV == dangKyLopHoc.MaSV && d.MaLopHoc == dangKyLopHoc.MaLopHoc);

                if (existingDangKy == null)
                {
                    return NotFound();
                }

                // Cập nhật giá trị từ form
                existingDangKy.NgayDangKy = dangKyLopHoc.NgayDangKy;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.DangKyLopHoc.Update(existingDangKy);
                _context.SaveChanges();

                return RedirectToAction("DangKyLopHocIndex");
            }
            return View("EditDangKyLopHoc", dangKyLopHoc);
        }

        // Xem danh sách DanhGia
        public IActionResult DanhGiaIndex()
        {
            var danhGiaList = _context.DanhGia.ToList();
            return View("DanhGiaIndex", danhGiaList);
        }

        // Xem danh sách LopHoc
        public IActionResult LopHocIndex()
        {
            var lopHocList = _context.LopHoc.ToList();
            return View("LopHocIndex", lopHocList);
        }

        // Xem danh sách MonHoc
        public IActionResult MonHocIndex()
        {
            var monHocList = _context.MonHoc.ToList();
            return View("MonHocIndex", monHocList);
        }

        // Xem danh sách DeCuong
        public IActionResult DeCuongIndex()
        {
            var deCuongList = _context.DeCuong.ToList();
            return View("DeCuongIndex", deCuongList);
        }

        // Xem danh sách GiangVien
        public IActionResult GiangVienIndex()
        {
            var giangVienList = _context.GiangVien.ToList();
            return View("GiangVienIndex", giangVienList);
        }

        // Xem danh sách Khoa
        public IActionResult KhoaIndex()
        {
            var khoaList = _context.Khoa.ToList();
            return View("KhoaIndex", khoaList);
        }

        // Xem danh sách TaiLieu
        public IActionResult TaiLieuIndex()
        {
            var taiLieuList = _context.TaiLieu.ToList();
            return View("TaiLieuIndex", taiLieuList);
        }
    }
}