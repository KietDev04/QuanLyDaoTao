using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        // Xem Khoa
        public IActionResult KhoaIndex()
        {
            var khoaList = _context.Khoa.ToList();
            if (khoaList == null)
            {
                return Content("Khoa list is null");
            }
            return View("Khoa/Index", khoaList);
        }

        // Xem danh sách BaiGiang
        public IActionResult BaiGiangIndex()
        {
            var baiGiangList = _context.BaiGiang
            .Include(bg => bg.MonHoc) // Lấy thông tin môn học
            .ToList();
            return View("BaiGiang/Index", baiGiangList);
        }

        // Xem danh sách môn học
        public IActionResult MonHocIndex()
        {
            var monHocList = _context.MonHoc
            .Include(m => m.Khoa) // Lấy thông tin khoa
            .ToList();
            return View("MonHoc/Index", monHocList);
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

        // Thêm DanhGia
        public IActionResult CreateDanhGia()
        {   
            ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen");
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH");
            return View("DanhGia/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDanhGia(DanhGia danhGia)
        {   
            if (_context.DanhGia.Any(d => d.MaDG == danhGia.MaDG))
            {
                ModelState.AddModelError("MaDG", "Mã đánh giá này đã tồn tại");
                return View("DanhGia/Create", danhGia);
            }
            try
            {
                _context.DanhGia.Add(danhGia);
                _context.SaveChanges();
                return RedirectToAction("DanhGiaIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", danhGia.MaSV);
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", danhGia.MaMH);
            return View("DanhGia/Create", danhGia);
        }

        // Sửa DanhGia
        public IActionResult EditDanhGia(string id)
        {
            var danhGia = _context.DanhGia
                .Include(d => d.SinhVien)
                .Include(d => d.MonHoc)
                .FirstOrDefault(d => d.MaDG == id);

            if (danhGia == null) return NotFound();

            ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", danhGia.MaSV);
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", danhGia.MaMH);
            return View("DanhGia/Edit", danhGia);
        }

        [HttpPost]
        public async Task<IActionResult> EditDanhGia([Bind("MaDG,MaSV,MaMH,DiemDanhGia,NhanXet,NgayDanhGia")] DanhGia danhGia)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("SinhVien");
            ModelState.Remove("MonHoc");

            // Kiểm tra xem MaSV có tồn tại trong bảng SinhVien không
            if (!string.IsNullOrEmpty(danhGia.MaSV) && !_context.SinhVien.Any(sv => sv.MaSV == danhGia.MaSV))
            {
                ModelState.AddModelError("MaSV", "Mã sinh viên không tồn tại.");
            }

            // Kiểm tra xem MaMH có tồn tại trong bảng MonHoc không
            if (!string.IsNullOrEmpty(danhGia.MaMH) && !_context.MonHoc.Any(mh => mh.MaMH == danhGia.MaMH))
            {
                ModelState.AddModelError("MaMH", "Mã môn học không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load đánh giá từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingDanhGia = await _context.DanhGia
                        .Include(dg => dg.SinhVien)
                        .Include(dg => dg.MonHoc)
                        .FirstOrDefaultAsync(dg => dg.MaDG == danhGia.MaDG);

                    if (existingDanhGia == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy đánh giá với MaDG = {danhGia.MaDG}");
                        ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", danhGia.MaSV);
                        ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", danhGia.MaMH);
                        return View("DanhGia/Edit", danhGia);
                    }

                    // Cập nhật các trường cần thiết
                    existingDanhGia.MaSV = danhGia.MaSV;
                    existingDanhGia.MaMH = danhGia.MaMH;
                    existingDanhGia.DiemDanhGia = danhGia.DiemDanhGia;
                    existingDanhGia.NhanXet = danhGia.NhanXet;
                    existingDanhGia.NgayDanhGia = danhGia.NgayDanhGia;

                    // Không cần cập nhật navigation properties (SinhVien, MonHoc) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingDanhGia).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật đánh giá thành công!";
                    return RedirectToAction("DanhGiaIndex");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
                }
            }
            else
            {
                // Ghi log để debug lỗi validation
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                    ModelState.AddModelError("", error.ErrorMessage); // Thêm lỗi vào ModelState để hiển thị trên giao diện
                }
            }

            // Nếu có lỗi, trả lại view với danh sách sinh viên và môn học
            ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", danhGia.MaSV);
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", danhGia.MaMH);
            return View("DanhGia/Edit", danhGia);
        }

        // Xem danh sách ChuongTrinhDaoTao
        public IActionResult ChuongTrinhDaoTaoIndex()
        {
            var chuongTrinhDaoTaoList = _context.ChuongTrinhDaoTao
            .Include(ct => ct.Khoa) // Lấy thông tin Khoa
            .ToList();
            return View("ChuongTrinhDaoTao/Index", chuongTrinhDaoTaoList);
        }

        // Xem danh sách PhanCongGiangDay
        public IActionResult PhanCongGiangDayIndex()
        {
            var phanCongGiangDayList = _context.PhanCongGiangDay.ToList();
            return View("PhanCongGiangDay/Index", phanCongGiangDayList);
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

        // Xem danh sách TaiLieu
        public IActionResult TaiLieuIndex()
        {
            var taiLieuList = _context.TaiLieu
            .Include(t => t.BaiGiang) // Lấy thông tin bài giảng
            .ToList();
            return View("TaiLieu/Index", taiLieuList);
        }
    }
}