using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // Xem danh sách Khoa
        public IActionResult KhoaIndex()
        {
            var khoaList = _context.Khoa.ToList();
            return View("Khoa/Index", khoaList);
        }

        // Xem danh sách BaiGiang
        public IActionResult BaiGiangIndex()
        {
            var baiGiangList = _context.BaiGiang
            .Include(b => b.MonHoc) // Lấy thông tin môn học
            .ToList();
            return View("BaiGiang/Index", baiGiangList);
        }

        // Xem danh sách MonHoc
        public IActionResult MonHocIndex()
        {
            var monHocList = _context.MonHoc
            .Include(m => m.Khoa)
            .ToList();
            return View("MonHoc/Index", monHocList);
        }

        // Xem danh sách DangKyLopHoc
        public IActionResult DangKyLopHocIndex()
        {
            var dangKyLopHocList = _context.DangKyLopHoc
            .Include(d => d.SinhVien) // Lấy thông tin sinh viên
            .Include(d => d.LopHoc) // Lấy thông tin lớp học
            .ToList();
            return View("DangKyLopHoc/Index", dangKyLopHocList);
        }

        [HttpGet]
        public IActionResult CreateDangKyLopHoc()
        {
            ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen");
            ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop");
            return View("DangKyLopHoc/Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDangKyLopHoc([Bind("MaSV,MaLopHoc")] DangKyLopHoc dangKyLopHoc)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("SinhVien");
            ModelState.Remove("LopHoc");

            // Kiểm tra xem MaSV có tồn tại trong bảng SinhVien không
            if (!string.IsNullOrEmpty(dangKyLopHoc.MaSV) && !_context.SinhVien.Any(sv => sv.MaSV == dangKyLopHoc.MaSV))
            {
                ModelState.AddModelError("MaSV", "Mã sinh viên không tồn tại.");
            }

            // Kiểm tra xem MaLopHoc có tồn tại trong bảng LopHoc không
            if (!string.IsNullOrEmpty(dangKyLopHoc.MaLopHoc) && !_context.LopHoc.Any(lh => lh.MaLop == dangKyLopHoc.MaLopHoc))
            {
                ModelState.AddModelError("MaLopHoc", "Mã lớp học không tồn tại.");
            }

            // Kiểm tra xem đã tồn tại đăng ký với MaSV và MaLopHoc này chưa
            if (_context.DangKyLopHoc.Any(dk => dk.MaSV == dangKyLopHoc.MaSV && dk.MaLopHoc == dangKyLopHoc.MaLopHoc))
            {
                ModelState.AddModelError("", "Đăng ký lớp học với mã sinh viên và mã lớp học này đã tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Gán ngày đăng ký là ngày hiện tại
                    dangKyLopHoc.NgayDangKy = DateTime.Now;

                    // Thêm bản ghi mới vào bảng DangKyLopHoc
                    _context.DangKyLopHoc.Add(dangKyLopHoc);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Thêm đăng ký lớp học thành công!";
                    return RedirectToAction("DangKyLopHocIndex");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
                }
            }

            // Nếu có lỗi, trả lại view với danh sách sinh viên và lớp học
            ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", dangKyLopHoc.MaSV);
            ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop", dangKyLopHoc.MaLopHoc);
            return View("DangKyLopHoc/Create", dangKyLopHoc);
        }

        // Xem danh sách DanhGia
        public IActionResult DanhGiaIndex()
        {
            var danhGiaList = _context.DanhGia
            .Include(d => d.SinhVien) // Lấy thông tin sinh viên
            .Include(d => d.MonHoc) // Lấy thông tin môn học
            .ToList();
            return View("DanhGia/Index", danhGiaList);
        }

        // Xem danh sách LopHoc
        public IActionResult LopHocIndex()
        {
            var lopHocList = _context.LopHoc
            .Include(l => l.ChuongTrinhDaoTao) // Include ChuongTrinhDaoTao
            .Include(l => l.DangKyLopHocs) // Include DangKyLopHocs
            .ToList();
            return View("LopHoc/Index", lopHocList);
        }

        // Xem danh sách DeCuong
        public IActionResult DeCuongIndex()
        {
            var deCuongList = _context.DeCuong
            .Include(d => d.MonHoc) // Lấy thông tin môn học
            .ToList();
            return View("DeCuong/Index", deCuongList);
        }

        // Xem danh sách GiangVien
        public IActionResult GiangVienIndex()
        {
            var giangVienList = _context.GiangVien.ToList();
            return View("GiangVienIndex", giangVienList);
        }

        // Xem danh sách TaiLieu
        public IActionResult TaiLieuIndex()
        {
            var taiLieuList = _context.TaiLieu
            .Include(t => t.BaiGiang) // Lấy thông tin bài giảng
            .ToList();
            return View("TaiLieu/Index", taiLieuList);
        }

        // Xem chương trình đào tạo
        public IActionResult ChuongTrinhDaoTaoIndex()
        {
            var chuongTrinhDaoTaoList = _context.ChuongTrinhDaoTao
            .Include(ct => ct.Khoa) // Lấy thông tin Khoa
            .ToList();
            return View("ChuongTrinhDaoTao/Index", chuongTrinhDaoTaoList);
        }
    }
}