using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDaoTaoWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace QuanLyDaoTaoWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; // Thêm UserManager

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Khởi tạo
        }

        public IActionResult IndexAdmin()
        {
            return View();
        }

        // Danh sách tài khoản chưa phê duyệt
        public IActionResult ApproveGiangVien()
        {
            var pendingGiangViens = _userManager.GetUsersInRoleAsync("GiangVien").Result
                .Where(u => !u.IsApproved).ToList();
            return View("ApproveGiangVien",pendingGiangViens);
        }

        // Phê duyệt tài khoản
        [HttpPost]
        public async Task<IActionResult> ApproveGiangVien(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null && await _userManager.IsInRoleAsync(user, "GiangVien"))
            {
                user.IsApproved = true;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("ApproveGiangVien");
        }

        // Quản lý Khoa
        public IActionResult KhoaIndex()
        {
            var khoaList = _context.Khoa.ToList();
            if (khoaList == null)
            {
                return Content("Khoa list is null");
            }
            return View("Khoa/Index", khoaList);
        }

        public IActionResult CreateKhoa()
        {
            return View("Khoa/Create");
        }

        [HttpPost]
        public IActionResult CreateKhoa(Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                _context.Khoa.Add(khoa);
                _context.SaveChanges();
                return RedirectToAction("KhoaIndex");
            }
            return View("Khoa/Create", khoa);
        }

        public IActionResult EditKhoa(string id)
        {
            var khoa = _context.Khoa.Find(id);
            if (khoa == null) return NotFound();
            return View("Khoa/Edit", khoa);
        }

        [HttpPost]
        public IActionResult EditKhoa(Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                _context.Khoa.Update(khoa);
                _context.SaveChanges();
                return RedirectToAction("KhoaIndex");
            }
            return View("Khoa/Edit", khoa);
        }

        public IActionResult DeleteKhoa(string id)
        {
            var khoa = _context.Khoa.Find(id);
            if (khoa == null) return NotFound();
            _context.Khoa.Remove(khoa);
            _context.SaveChanges();
            return RedirectToAction("KhoaIndex");
        }

        // Quản lý BaiGiang
        public IActionResult BaiGiangIndex()
        {
            var baiGiangList = _context.BaiGiang.ToList();
            return View("BaiGiang/Index", baiGiangList);
        }

        public IActionResult CreateBaiGiang()
        {
            ViewBag.MonHocList = _context.MonHoc.Select(m => new SelectListItem
            {
                Value = m.MaMH,
                Text = $"{m.MaMH} - {m.TenMH}"
            }).ToList();
            return View("BaiGiang/Create");
        }

        [HttpPost]
        public IActionResult CreateBaiGiang(BaiGiang baiGiang)
        {
            if (ModelState.IsValid)
            {
                if (!_context.MonHoc.Any(m => m.MaMH == baiGiang.MaMH))
                {
                    ModelState.AddModelError("MaMH", "Mã môn học không tồn tại");
                    ViewBag.MonHocList = _context.MonHoc.Select(m => new SelectListItem
                    {
                        Value = m.MaMH,
                        Text = $"{m.MaMH} - {m.TenMH}"
                    }).ToList();
                    return View("BaiGiang/Create", baiGiang);
                }

                _context.BaiGiang.Add(baiGiang);
                _context.SaveChanges();
                TempData["Message"] = "Thêm bài giảng thành công!";
                return RedirectToAction("BaiGiangIndex");
            }
            ViewBag.MonHocList = _context.MonHoc.Select(m => new SelectListItem
            {
                Value = m.MaMH,
                Text = $"{m.MaMH} - {m.TenMH}"
            }).ToList();
            return View("BaiGiang/Create", baiGiang);
        }

        public IActionResult EditBaiGiang(string id)
        {
            var baiGiang = _context.BaiGiang.Find(id);
            if (baiGiang == null) return NotFound();
            
            ViewBag.MonHocList = _context.MonHoc.Select(m => new SelectListItem
            {
                Value = m.MaMH,
                Text = $"{m.MaMH} - {m.TenMH}"
            }).ToList();
            return View("BaiGiang/Edit", baiGiang);
        }

        [HttpPost]
        public IActionResult EditBaiGiang(BaiGiang baiGiang)
        {
            if (ModelState.IsValid)
            {
                if (!_context.MonHoc.Any(m => m.MaMH == baiGiang.MaMH))
                {
                    ModelState.AddModelError("MaMH", "Mã môn học không tồn tại");
                    ViewBag.MonHocList = _context.MonHoc.Select(m => new SelectListItem
                    {
                        Value = m.MaMH,
                        Text = $"{m.MaMH} - {m.TenMH}"
                    }).ToList();
                    return View("BaiGiang/Edit", baiGiang);
                }

                _context.BaiGiang.Update(baiGiang);
                _context.SaveChanges();
                TempData["Message"] = "Cập nhật bài giảng thành công!";
                return RedirectToAction("BaiGiangIndex");
            }
            ViewBag.MonHocList = _context.MonHoc.Select(m => new SelectListItem
            {
                Value = m.MaMH,
                Text = $"{m.MaMH} - {m.TenMH}"
            }).ToList();
            return View("BaiGiang/Edit", baiGiang);
        }

        public IActionResult DeleteBaiGiang(string id)
        {
            var baiGiang = _context.BaiGiang.Find(id);
            if (baiGiang == null) return NotFound();
            _context.BaiGiang.Remove(baiGiang);
            _context.SaveChanges();
            return RedirectToAction("BaiGiangIndex");
        }

        // Quản lý SinhVien
        public IActionResult SinhVienIndex()
        {
            var sinhVienList = _context.SinhVien.ToList();
            return View("SinhVien/Index", sinhVienList);
        }

        public IActionResult CreateSinhVien()
        {
            return View("SinhVien/Create");
        }

        [HttpPost]
        public IActionResult CreateSinhVien(SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                _context.SinhVien.Add(sinhVien);
                _context.SaveChanges();
                return RedirectToAction("SinhVienIndex");
            }
            ViewBag.KhoaList = _context.Khoa.ToList();
            return View("SinhVien/Create", sinhVien);
        }

        public IActionResult EditSinhVien(string id)
        {
            var sinhVien = _context.SinhVien.Find(id);
            if (sinhVien == null) return NotFound();
            ViewBag.KhoaList = _context.Khoa.ToList();
            return View("SinhVien/Edit", sinhVien);
        }

        [HttpPost]
        public IActionResult EditSinhVien(SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                _context.SinhVien.Update(sinhVien);
                _context.SaveChanges();
                return RedirectToAction("SinhVienIndex");
            }
            ViewBag.KhoaList = _context.Khoa.ToList();
            return View("SinhVien/Edit", sinhVien);
        }

        public IActionResult DeleteSinhVien(string id)
        {
            var sinhVien = _context.SinhVien.Find(id);
            if (sinhVien == null) return NotFound();
            _context.SinhVien.Remove(sinhVien);
            _context.SaveChanges();
            return RedirectToAction("SinhVienIndex");
        }

        // Quản lý MonHoc
        public IActionResult MonHocIndex()
        {
            var monHocList = _context.MonHoc.ToList();
            return View("MonHoc/Index", monHocList);
        }

        public IActionResult CreateMonHoc()
        {
            return View("MonHoc/Create");
        }

        [HttpPost]
        public IActionResult CreateMonHoc(MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                _context.MonHoc.Add(monHoc);
                _context.SaveChanges();
                return RedirectToAction("MonHocIndex");
            }
            return View("MonHoc/Create",monHoc);
        }

        public IActionResult EditMonHoc(string id)
        {
            var monHoc = _context.MonHoc.Find(id);
            if (monHoc == null) return NotFound();
            return View("MonHoc/Edit",monHoc);
        }

        [HttpPost]
        public IActionResult EditMonHoc(MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                _context.MonHoc.Update(monHoc);
                _context.SaveChanges();
                return RedirectToAction("MonHocIndex");
            }
            return View("MonHoc/Edit",monHoc);
        }

        public IActionResult DeleteMonHoc(string id)
        {
            var monHoc = _context.MonHoc.Find(id);
            if (monHoc == null) return NotFound();
            _context.MonHoc.Remove(monHoc);
            _context.SaveChanges();
            return RedirectToAction("MonHocIndex");
        }

        // Quản lý LopHoc
        public IActionResult LopHocIndex()
        {
            var lopHocList = _context.LopHoc.ToList();
            return View("LopHoc/Index", lopHocList);
        }

        public IActionResult CreateLopHoc()
        {
            ViewBag.KhoaList = _context.Khoa.ToList(); // Để chọn khoa
            return View("LopHoc/Create");
        }

        [HttpPost]
        public IActionResult CreateLopHoc(LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                _context.LopHoc.Add(lopHoc);
                _context.SaveChanges();
                return RedirectToAction("LopHocIndex");
            }
            ViewBag.KhoaList = _context.Khoa.ToList();
            return View("LopHoc/Create", lopHoc);
        }

        public IActionResult EditLopHoc(string id)
        {
            var lopHoc = _context.LopHoc.Find(id);
            if (lopHoc == null) return NotFound();
            ViewBag.KhoaList = _context.Khoa.ToList();
            return View("LopHoc/Edit", lopHoc);
        }

        [HttpPost]
        public IActionResult EditLopHoc(LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                _context.LopHoc.Update(lopHoc);
                _context.SaveChanges();
                return RedirectToAction("LopHocIndex");
            }
            ViewBag.KhoaList = _context.Khoa.ToList();
            return View("LopHoc/Edit", lopHoc);
        }

        public IActionResult DeleteLopHoc(string id)
        {
            var lopHoc = _context.LopHoc.Find(id);
            if (lopHoc == null) return NotFound();
            _context.LopHoc.Remove(lopHoc);
            _context.SaveChanges();
            return RedirectToAction("LopHocIndex");
        }

        // Quản lý ChuongTrinhDaoTao
        public IActionResult ChuongTrinhDaoTaoIndex()
        {
            var chuongTrinhDaoTaoList = _context.ChuongTrinhDaoTao.ToList();
            return View("ChuongTrinhDaoTao/Index", chuongTrinhDaoTaoList);
        }

        public IActionResult CreateChuongTrinhDaoTao()
        {
            return View("ChuongTrinhDaoTao/Create");
        }

        [HttpPost]
        public IActionResult CreateChuongTrinhDaoTao(ChuongTrinhDaoTao chuongTrinhDaoTao)
        {
            if (ModelState.IsValid)
            {
                _context.ChuongTrinhDaoTao.Add(chuongTrinhDaoTao);
                _context.SaveChanges();
                return RedirectToAction("ChuongTrinhDaoTaoIndex");
            }
            return View("ChuongTrinhDaoTao/Create", chuongTrinhDaoTao);
        }

        public IActionResult EditChuongTrinhDaoTao(string id)
        {
            var chuongTrinhDaoTao = _context.ChuongTrinhDaoTao.Find(id);
            if (chuongTrinhDaoTao == null) return NotFound();
            return View("ChuongTrinhDaoTao/Edit", chuongTrinhDaoTao);
        }

        [HttpPost]
        public IActionResult EditChuongTrinhDaoTao(ChuongTrinhDaoTao chuongTrinhDaoTao)
        {
            if (ModelState.IsValid)
            {
                _context.ChuongTrinhDaoTao.Update(chuongTrinhDaoTao);
                _context.SaveChanges();
                return RedirectToAction("ChuongTrinhDaoTaoIndex");
            }
            return View("ChuongTrinhDaoTao/Edit", chuongTrinhDaoTao);
        }

        public IActionResult DeleteChuongTrinhDaoTao(string id)
        {
            var chuongTrinhDaoTao = _context.ChuongTrinhDaoTao.Find(id);
            if (chuongTrinhDaoTao == null) return NotFound();
            _context.ChuongTrinhDaoTao.Remove(chuongTrinhDaoTao);
            _context.SaveChanges();
            return RedirectToAction("ChuongTrinhDaoTaoIndex");
        }

        // Quản lý DanhGia
        public IActionResult DanhGiaIndex()
        {
            var danhGiaList = _context.DanhGia.ToList();
            return View("DanhGia/Index", danhGiaList);
        }

        public IActionResult CreateDanhGia()
        {
            return View("DanhGia/Create");
        }

        [HttpPost]
        public IActionResult CreateDanhGia(DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                _context.DanhGia.Add(danhGia);
                _context.SaveChanges();
                return RedirectToAction("DanhGiaIndex");
            }
            return View("DanhGia/Create", danhGia);
        }

        public IActionResult EditDanhGia(string id)
        {
            var danhGia = _context.DanhGia.Find(id);
            if (danhGia == null) return NotFound();
            return View("DanhGia/Edit", danhGia);
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
            return View("DanhGia/Edit", danhGia);
        }

        public IActionResult DeleteDanhGia(string id)
        {
            var danhGia = _context.DanhGia.Find(id);
            if (danhGia == null) return NotFound();
            _context.DanhGia.Remove(danhGia);
            _context.SaveChanges();
            return RedirectToAction("DanhGiaIndex");
        }

        // Quản lý DeCuong
        public IActionResult DeCuongIndex()
        {
            var deCuongList = _context.DeCuong.ToList();
            return View("DeCuong/Index", deCuongList);
        }

        public IActionResult CreateDeCuong()
        {
            return View("DeCuong/Create");
        }

        [HttpPost]
        public IActionResult CreateDeCuong(DeCuong deCuong)
        {
            if (ModelState.IsValid)
            {
                _context.DeCuong.Add(deCuong);
                _context.SaveChanges();
                return RedirectToAction("DeCuongIndex");
            }
            return View("DeCuong/Create", deCuong);
        }

        public IActionResult EditDeCuong(string id)
        {
            var deCuong = _context.DeCuong.Find(id);
            if (deCuong == null) return NotFound();
            return View("DeCuong/Edit", deCuong);
        }

        [HttpPost]
        public IActionResult EditDeCuong(DeCuong deCuong)
        {
            if (ModelState.IsValid)
            {
                _context.DeCuong.Update(deCuong);
                _context.SaveChanges();
                return RedirectToAction("DeCuongIndex");
            }
            return View("DeCuong/Edit", deCuong);
        }

        public IActionResult DeleteDeCuong(string id)
        {
            var deCuong = _context.DeCuong.Find(id);
            if (deCuong == null) return NotFound();
            _context.DeCuong.Remove(deCuong);
            _context.SaveChanges();
            return RedirectToAction("DeCuongIndex");
        }

        // Quản lý PhanCongGiangDay
        public IActionResult PhanCongGiangDayIndex()
        {
            var phanCongGiangDayList = _context.PhanCongGiangDay.ToList();
            return View("PhanCongGiangDay/Index", phanCongGiangDayList);
        }

        public IActionResult CreatePhanCongGiangDay()
        {
            return View("PhanCongGiangDay/Create");
        }

        [HttpPost]
        public IActionResult CreatePhanCongGiangDay(PhanCongGiangDay phanCongGiangDay)
        {
            if (ModelState.IsValid)
            {
                _context.PhanCongGiangDay.Add(phanCongGiangDay);
                _context.SaveChanges();
                return RedirectToAction("PhanCongGiangDayIndex");
            }
            return View("PhanCongGiangDay/Create", phanCongGiangDay);
        }

        public IActionResult EditPhanCongGiangDay(string id)
        {
            var phanCongGiangDay = _context.PhanCongGiangDay.Find(id);
            if (phanCongGiangDay == null) return NotFound();
            return View("PhanCongGiangDay/Edit", phanCongGiangDay);
        }

        [HttpPost]
        public IActionResult EditPhanCongGiangDay(PhanCongGiangDay phanCongGiangDay)
        {
            if (ModelState.IsValid)
            {
                _context.PhanCongGiangDay.Update(phanCongGiangDay);
                _context.SaveChanges();
                return RedirectToAction("PhanCongGiangDayIndex");
            }
            return View("PhanCongGiangDay/Edit", phanCongGiangDay);
        }

        public IActionResult DeletePhanCongGiangDay(string id)
        {
            var phanCongGiangDay = _context.PhanCongGiangDay.Find(id);
            if (phanCongGiangDay == null) return NotFound();
            _context.PhanCongGiangDay.Remove(phanCongGiangDay);
            _context.SaveChanges();
            return RedirectToAction("PhanCongGiangDayIndex");
        }

        // Quản Lý TaiLieu
        public IActionResult TaiLieuIndex()
        {
            var taiLieuList = _context.TaiLieu.ToList();
            return View("TaiLieu/Index", taiLieuList);
        }

        public IActionResult CreateTaiLieu()
        {
            return View("TaiLieu/Create");
        }

        [HttpPost]
        public IActionResult CreateTaiLieu(TaiLieu taiLieu)
        {
            if (ModelState.IsValid)
            {
                _context.TaiLieu.Add(taiLieu);
                _context.SaveChanges();
                return RedirectToAction("TaiLieuIndex");
            }
            return View("TaiLieu/Create", taiLieu);
        }

        public IActionResult EditTaiLieu(string id)
        {
            var taiLieu = _context.TaiLieu.Find(id);
            if (taiLieu == null) return NotFound();
            return View("TaiLieu/Edit", taiLieu);
        }

        [HttpPost]
        public IActionResult EditTaiLieu(TaiLieu taiLieu)
        {
            if (ModelState.IsValid)
            {
                _context.TaiLieu.Update(taiLieu);
                _context.SaveChanges();
                return RedirectToAction("TaiLieuIndex");
            }
            return View("TaiLieu/Edit", taiLieu);
        }

        public IActionResult DeleteTaiLieu(string id)
        {
            var taiLieu = _context.TaiLieu.Find(id);
            if (taiLieu == null) return NotFound();
            _context.TaiLieu.Remove(taiLieu);
            _context.SaveChanges();
            return RedirectToAction("TaiLieuIndex");
        }

        // Quản lý DangKyLopHoc
        public IActionResult DangKyLopHocIndex()
        {
            var dangKyLopHocList = _context.DangKyLopHoc.ToList();
            return View("DangKyLopHoc/Index", dangKyLopHocList);
        }

        public IActionResult CreateDangKyLopHoc()
        {
            return View("DangKyLopHoc/Create");
        }

        [HttpPost]
        public IActionResult CreateDangKyLopHoc(DangKyLopHoc dangKyLopHoc)
        {
            if (ModelState.IsValid)
            {
                _context.DangKyLopHoc.Add(dangKyLopHoc);
                _context.SaveChanges();
                return RedirectToAction("DangKyLopHocIndex");
            }
            return View("DangKyLopHoc/Create", dangKyLopHoc);
        }

        public IActionResult EditDangKyLopHoc(string id)
        {
            var dangKyLopHoc = _context.DangKyLopHoc.Find(id);
            if (dangKyLopHoc == null) return NotFound();
            return View("DangKyLopHoc/Edit", dangKyLopHoc);
        }

        [HttpPost]
        public IActionResult EditDangKyLopHoc(DangKyLopHoc dangKyLopHoc)
        {
            if (ModelState.IsValid)
            {
                _context.DangKyLopHoc.Update(dangKyLopHoc);
                _context.SaveChanges();
                return RedirectToAction("DangKyLopHocIndex");
            }
            return View("DangKyLopHoc/Edit", dangKyLopHoc);
        }

        public IActionResult DeleteDangKyLopHoc(string id)
        {
            var dangKyLopHoc = _context.DangKyLopHoc.Find(id);
            if (dangKyLopHoc == null) return NotFound();
            _context.DangKyLopHoc.Remove(dangKyLopHoc);
            _context.SaveChanges();
            return RedirectToAction("DangKyLopHocIndex");
        }

        // Quản Lý GiangVien
        public IActionResult GiangVienIndex()
        {
            var giangVienList = _context.GiangVien.ToList();
            return View("GiangVien/Index", giangVienList);
        }

        public IActionResult CreateGiangVien()
        {
            ViewBag.KhoaList = _context.Khoa.ToList(); // Để chọn khoa
            return View("GiangVien/Create");
        }

        [HttpPost]
        public IActionResult CreateGiangVien(GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                _context.GiangVien.Add(giangVien);
                _context.SaveChanges();
                return RedirectToAction("GiangVienIndex");
            }
            ViewBag.KhoaList = _context.Khoa.ToList();
            return View("GiangVien/Create", giangVien);
        }

        public IActionResult EditGiangVien(string id)
        {
            var giangVien = _context.GiangVien.Find(id);
            if (giangVien == null) return NotFound();
            ViewBag.KhoaList = _context.Khoa.ToList();
            return View("GiangVien/Edit", giangVien);
        }

        [HttpPost]
        public IActionResult EditGiangVien(GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                _context.GiangVien.Update(giangVien);
                _context.SaveChanges();
                return RedirectToAction("GiangVienIndex");
            }
            ViewBag.KhoaList = _context.Khoa.ToList();
            return View("GiangVien/Edit", giangVien);
        }

        public IActionResult DeleteGiangVien(string id)
        {
            var giangVien = _context.GiangVien.Find(id);
            if (giangVien == null) return NotFound();
            _context.GiangVien.Remove(giangVien);
            _context.SaveChanges();
            return RedirectToAction("GiangVienIndex");
        }
    }
}