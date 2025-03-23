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
        public async Task<IActionResult> CreateKhoa(Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem MaKhoa đã tồn tại chưa
                if (_context.Khoa.Any(k => k.MaKhoa == khoa.MaKhoa))
                {
                    ModelState.AddModelError("MaKhoa", "Mã khoa này đã tồn tại");
                    return View("Khoa/Create", khoa);
                }

                try
                {
                    _context.Khoa.Add(khoa);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Thêm mới khoa thành công!";
                    return RedirectToAction("KhoaIndex");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
                }
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
            var baiGiangList = _context.BaiGiang
            .Include(bg => bg.MonHoc) // Lấy thông tin môn học
            .ToList();
            return View("BaiGiang/Index", baiGiangList);
        }

        public IActionResult CreateBaiGiang()
        {
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH");
            return View("BaiGiang/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateBaiGiang(BaiGiang baiGiang)
        {
            if (_context.BaiGiang.Any(b => b.MaBG == baiGiang.MaBG))
            {
                ModelState.AddModelError("MaSV", "Mã sinh viên này đã tồn tại");
                return View("GiangVien/Create", baiGiang);
            }
            try
            {
                _context.BaiGiang.Add(baiGiang);
                _context.SaveChanges();
                TempData["Success"] = "Thêm mới bài giảng thành công!";
                return RedirectToAction("BaiGiangIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH");
            return View("BaiGiang/Create", baiGiang);
        }

        public IActionResult EditBaiGiang(string id)
        {
            var baiGiang = _context.BaiGiang.Find(id);
            if (baiGiang == null) return NotFound();
            
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH");
            return View("BaiGiang/Edit", baiGiang);
        }

        [HttpPost]
        public async Task<IActionResult> EditBaiGiang([Bind("MaBG,TieuDe,MaMH,NoiDung")] BaiGiang baiGiang)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("MonHoc");

            // Kiểm tra xem MaMH có tồn tại trong bảng MonHoc không
            if (!string.IsNullOrEmpty(baiGiang.MaMH) && !_context.MonHoc.Any(m => m.MaMH == baiGiang.MaMH))
            {
                ModelState.AddModelError("MaMH", "Mã môn học không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load bài giảng từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingBaiGiang = await _context.BaiGiang
                        .Include(bg => bg.MonHoc)
                        .FirstOrDefaultAsync(bg => bg.MaBG == baiGiang.MaBG);

                    if (existingBaiGiang == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy bài giảng với MaBG = {baiGiang.MaBG}");
                        ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", baiGiang.MaMH);
                        return View("BaiGiang/Edit", baiGiang);
                    }

                    // Cập nhật các trường cần thiết
                    existingBaiGiang.TieuDe = baiGiang.TieuDe;
                    existingBaiGiang.MaMH = baiGiang.MaMH;
                    existingBaiGiang.NoiDung = baiGiang.NoiDung;

                    // Không cần cập nhật navigation properties (MonHoc) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingBaiGiang).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Cập nhật bài giảng thành công!";
                    return RedirectToAction("BaiGiangIndex");
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
                }
            }

            // Nếu có lỗi, trả lại view với danh sách môn học
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", baiGiang.MaMH);
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
            var sinhVienList = _context.SinhVien
            .Include(s => s.Khoa) // Lấy thông tin khoa
            .ToList();
            return View("SinhVien/Index", sinhVienList);
        }

        public IActionResult CreateSinhVien()
        {
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa");
            return View("SinhVien/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSinhVien(SinhVien sinhVien)
        {
            if (_context.SinhVien.Any(s => s.MaSV == sinhVien.MaSV))
            {
                ModelState.AddModelError("MaSV", "Mã sinh viên này đã tồn tại");
                return View("SinhVien/Create", sinhVien);
            }
            try
            {
                _context.SinhVien.Add(sinhVien);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm mới sinh viên thành công!";
                return RedirectToAction("SinhVienIndex");
            }   
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa");
            return View("SinhVien/Create", sinhVien);                    
        }

        public IActionResult EditSinhVien(string id)
        {
            var sinhVien = _context.SinhVien.Find(id);
            if (sinhVien == null) return NotFound();
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", sinhVien.MaKhoa);
            return View("SinhVien/Edit", sinhVien);
        }

        [HttpPost]
        public async Task<IActionResult> EditSinhVien([Bind("MaSV,HoTen,NgaySinh,Email,MaKhoa")] SinhVien sinhVien)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("Khoa");
            ModelState.Remove("DangKyLopHocs");
            ModelState.Remove("DanhGias");

            // Kiểm tra xem MaKhoa có tồn tại trong bảng Khoa không
            if (!string.IsNullOrEmpty(sinhVien.MaKhoa) && !_context.Khoa.Any(k => k.MaKhoa == sinhVien.MaKhoa))
            {
                ModelState.AddModelError("MaKhoa", "Mã khoa không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load sinh viên từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingSinhVien = await _context.SinhVien
                        .Include(s => s.Khoa)
                        .Include(s => s.DangKyLopHocs)
                        .Include(s => s.DanhGias)
                        .FirstOrDefaultAsync(s => s.MaSV == sinhVien.MaSV);

                    if (existingSinhVien == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy sinh viên với MaSV = {sinhVien.MaSV}");
                        ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", sinhVien.MaKhoa);
                        return View("SinhVien/Edit", sinhVien);
                    }

                    // Cập nhật các trường cần thiết
                    existingSinhVien.HoTen = sinhVien.HoTen;
                    existingSinhVien.NgaySinh = sinhVien.NgaySinh;
                    existingSinhVien.Email = sinhVien.Email;
                    existingSinhVien.MaKhoa = sinhVien.MaKhoa;

                    // Không cần cập nhật navigation properties (Khoa, DangKyLopHocs, DanhGias) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingSinhVien).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật sinh viên thành công!";
                    return RedirectToAction("SinhVienIndex");
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
                }
            }

            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", sinhVien.MaKhoa);
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
            var monHocList = _context.MonHoc
            .Include(m => m.Khoa) // Lấy thông tin khoa
            .ToList();
            return View("MonHoc/Index", monHocList);
        }

        public IActionResult CreateMonHoc()
        {
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa");
            return View("MonHoc/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonHoc(MonHoc monHoc)
        {
            if (_context.MonHoc.Any(m => m.MaMH == monHoc.MaMH))
            {
                ModelState.AddModelError("MaMH", "Mã môn học này đã tồn tại");
                return View("MonHoc/Create", monHoc);
            }
            try
            {
                _context.MonHoc.Add(monHoc);
                _context.SaveChanges();
                TempData["Success"] = "Thêm mới môn học thành công!";
                return RedirectToAction("MonHocIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", monHoc.MaKhoa);
            return View("MonHoc/Create",monHoc);
        }

        public IActionResult EditMonHoc(string id)
        {
            var monHoc = _context.MonHoc.Find(id);
            if (monHoc == null) return NotFound();
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", monHoc.MaKhoa);
            return View("MonHoc/Edit",monHoc);
        }

        [HttpPost]
        public async Task<IActionResult> EditMonHoc([Bind("MaMH,TenMH,SoTinChi,MaKhoa")] MonHoc monHoc)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("Khoa");
            ModelState.Remove("BaiGiangs");
            ModelState.Remove("PhanCongGiangDays");
            ModelState.Remove("DanhGias");

            // Kiểm tra xem MaKhoa có tồn tại trong bảng Khoa không
            if (!string.IsNullOrEmpty(monHoc.MaKhoa) && !_context.Khoa.Any(k => k.MaKhoa == monHoc.MaKhoa))
            {
                ModelState.AddModelError("MaKhoa", "Mã khoa không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load môn học từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingMonHoc = await _context.MonHoc
                        .Include(mh => mh.Khoa)
                        .Include(mh => mh.BaiGiangs)
                        .Include(mh => mh.PhanCongGiangDays)
                        .Include(mh => mh.DanhGias)
                        .FirstOrDefaultAsync(mh => mh.MaMH == monHoc.MaMH);

                    if (existingMonHoc == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy môn học với MaMH = {monHoc.MaMH}");
                        ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", monHoc.MaKhoa);
                        return View("MonHoc/Edit", monHoc);
                    }

                    // Cập nhật các trường cần thiết
                    existingMonHoc.TenMH = monHoc.TenMH;
                    existingMonHoc.SoTinChi = monHoc.SoTinChi;
                    existingMonHoc.MaKhoa = monHoc.MaKhoa;

                    // Không cần cập nhật navigation properties (Khoa, BaiGiangs, PhanCongGiangDays, DanhGias) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingMonHoc).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật môn học thành công!";
                    return RedirectToAction("MonHocIndex");
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
                }
            }

            // Nếu có lỗi, trả lại view với danh sách khoa
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", monHoc.MaKhoa);
            return View("MonHoc/Edit", monHoc);
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
            var lopHocList = _context.LopHoc
            .Include(l => l.ChuongTrinhDaoTao) // Include ChuongTrinhDaoTao
            .Include(l => l.DangKyLopHocs) // Include DangKyLopHocs
            .ToList();
            return View("LopHoc/Index", lopHocList);
        }

        public IActionResult CreateLopHoc()
        {
            ViewBag.chuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCTDT", "TenCTDT");
            return View("LopHoc/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateLopHoc(LopHoc lopHoc)
        {
            if (_context.LopHoc.Any(l => l.MaLop == lopHoc.MaLop))
            {
                ModelState.AddModelError("MaLop", "Mã lớp này đã tồn tại");
                ViewBag.chuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCTDT", "TenCTDT");
                return View("LopHoc/Create", lopHoc);
            }
            try
            {
                _context.LopHoc.Add(lopHoc);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm mới lớp học thành công!";
                return RedirectToAction("LopHocIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            ViewBag.chuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCTDT", "TenCTDT");
            return View("LopHoc/Create", lopHoc);
        }

        public IActionResult EditLopHoc(string id)
        {
            var lopHoc = _context.LopHoc.Find(id);
            if (lopHoc == null) return NotFound();
            ViewBag.chuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCTDT", "TenCTDT", lopHoc.MaCTDT);
            return View("LopHoc/Edit", lopHoc);
        }

        [HttpPost]
        public async Task<IActionResult> EditLopHoc([Bind("MaLop,TenLop,MaCTDT")] LopHoc lopHoc)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("ChuongTrinhDaoTao");
            ModelState.Remove("DangKyLopHocs");
            ModelState.Remove("PhanCongGiangDays");

            // Kiểm tra xem MaCTDT có tồn tại trong bảng ChuongTrinhDaoTao không
            if (!string.IsNullOrEmpty(lopHoc.MaCTDT) && !_context.ChuongTrinhDaoTao.Any(ct => ct.MaCTDT == lopHoc.MaCTDT))
            {
                ModelState.AddModelError("MaCTDT", "Mã chương trình đào tạo không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load lớp học từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingLopHoc = await _context.LopHoc
                        .Include(lh => lh.ChuongTrinhDaoTao)
                        .Include(lh => lh.DangKyLopHocs)
                        .Include(lh => lh.PhanCongGiangDays)
                        .FirstOrDefaultAsync(lh => lh.MaLop == lopHoc.MaLop);

                    if (existingLopHoc == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy lớp học với MaLop = {lopHoc.MaLop}");
                        ViewBag.ChuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCTDT", "TenCTDT", lopHoc.MaCTDT);
                        return View("LopHoc/Edit", lopHoc);
                    }

                    // Cập nhật các trường cần thiết
                    existingLopHoc.TenLop = lopHoc.TenLop;
                    existingLopHoc.MaCTDT = lopHoc.MaCTDT;

                    // Không cần cập nhật navigation properties (ChuongTrinhDaoTao, DangKyLopHocs, PhanCongGiangDays) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingLopHoc).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật lớp học thành công!";
                    return RedirectToAction("LopHocIndex");
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
                }
            }

            // Nếu có lỗi, trả lại view với danh sách chương trình đào tạo
            ViewBag.ChuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCTDT", "TenCTDT", lopHoc.MaCTDT);
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
            var chuongTrinhDaoTaoList = _context.ChuongTrinhDaoTao
            .Include(ct => ct.Khoa) // Lấy thông tin Khoa
            .ToList();
            return View("ChuongTrinhDaoTao/Index", chuongTrinhDaoTaoList);
        }

        public IActionResult CreateChuongTrinhDaoTao()
        {
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa");
            return View("ChuongTrinhDaoTao/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateChuongTrinhDaoTao(ChuongTrinhDaoTao chuongTrinhDaoTao)
        {
            if (_context.ChuongTrinhDaoTao.Any(ct => ct.MaCTDT == chuongTrinhDaoTao.MaCTDT))
            {
                ModelState.AddModelError("MaCTDT", "Mã chương trình đào tạo này đã tồn tại");
                return View("ChuongTrinhDaoTao/Create", chuongTrinhDaoTao);
            }
            try
            {
                _context.ChuongTrinhDaoTao.Add(chuongTrinhDaoTao);
                _context.SaveChanges();
                return RedirectToAction("ChuongTrinhDaoTaoIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", chuongTrinhDaoTao.MaKhoa);
            return View("ChuongTrinhDaoTao/Create", chuongTrinhDaoTao);
        }

        public IActionResult EditChuongTrinhDaoTao(string id)
        {
            var chuongTrinhDaoTao = _context.ChuongTrinhDaoTao.Find(id);
            if (chuongTrinhDaoTao == null) return NotFound();
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", chuongTrinhDaoTao.MaKhoa);
            return View("ChuongTrinhDaoTao/Edit", chuongTrinhDaoTao);
        }

        [HttpPost]
        public async Task<IActionResult> EditChuongTrinhDaoTao([Bind("MaCTDT,TenCTDT,NamBatDau,MaKhoa,TrangThai")] ChuongTrinhDaoTao chuongTrinhDaoTao)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("Khoa");
            ModelState.Remove("LopHocs");

            // Kiểm tra xem MaKhoa có tồn tại trong bảng Khoa không
            if (!string.IsNullOrEmpty(chuongTrinhDaoTao.MaKhoa) && !_context.Khoa.Any(k => k.MaKhoa == chuongTrinhDaoTao.MaKhoa))
            {
                ModelState.AddModelError("MaKhoa", "Mã khoa không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load chương trình đào tạo từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingChuongTrinhDaoTao = await _context.ChuongTrinhDaoTao
                        .Include(ct => ct.Khoa)
                        .Include(ct => ct.LopHocs)
                        .FirstOrDefaultAsync(ct => ct.MaCTDT == chuongTrinhDaoTao.MaCTDT);

                    if (existingChuongTrinhDaoTao == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy chương trình đào tạo với MaCTDT = {chuongTrinhDaoTao.MaCTDT}");
                        ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", chuongTrinhDaoTao.MaKhoa);
                        return View("ChuongTrinhDaoTao/Edit", chuongTrinhDaoTao);
                    }

                    // Cập nhật các trường cần thiết
                    existingChuongTrinhDaoTao.TenCTDT = chuongTrinhDaoTao.TenCTDT;
                    existingChuongTrinhDaoTao.NamBatDau = chuongTrinhDaoTao.NamBatDau;
                    existingChuongTrinhDaoTao.MaKhoa = chuongTrinhDaoTao.MaKhoa;
                    existingChuongTrinhDaoTao.TrangThai = chuongTrinhDaoTao.TrangThai;

                    // Không cần cập nhật navigation properties (Khoa, LopHocs) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingChuongTrinhDaoTao).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật chương trình đào tạo thành công!";
                    return RedirectToAction("ChuongTrinhDaoTaoIndex");
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
                }
            }

            // Nếu có lỗi, trả lại view với danh sách khoa
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", chuongTrinhDaoTao.MaKhoa);
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
            var danhGiaList = _context.DanhGia
            .Include(d => d.SinhVien) // Lấy thông tin sinh viên
            .Include(d => d.MonHoc) // Lấy thông tin môn học
            .ToList();
            return View("DanhGia/Index", danhGiaList);
        }

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
            var deCuongList = _context.DeCuong
            .Include(d => d.MonHoc) // Lấy thông tin môn học
            .ToList();
            return View("DeCuong/Index", deCuongList);
        }

        public IActionResult CreateDeCuong()
        {
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH");
            return View("DeCuong/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeCuong(DeCuong deCuong)
        {
            if (_context.DeCuong.Any(d => d.MaDC == deCuong.MaDC))
            {
                ModelState.AddModelError("MaDC", "Mã đề cương này đã tồn tại");
                return View("DeCuong/Create", deCuong);
            }
            try
            {
                _context.DeCuong.Add(deCuong);
                _context.SaveChanges();
                return RedirectToAction("DeCuongIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", deCuong.MaMH);
            return View("DeCuong/Create", deCuong);
        }

        public IActionResult EditDeCuong(string id)
        {
            var deCuong = _context.DeCuong.Find(id);
            if (deCuong == null) return NotFound();
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", deCuong.MaMH);
            return View("DeCuong/Edit", deCuong);
        }

        [HttpPost]
        public async Task<IActionResult> EditDeCuong([Bind("MaDC,MaMH,MoTa,MucTieu,NgayCapNhat")] DeCuong deCuong)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("MonHoc");

            // Kiểm tra xem MaMH có tồn tại trong bảng MonHoc không
            if (!string.IsNullOrEmpty(deCuong.MaMH) && !_context.MonHoc.Any(mh => mh.MaMH == deCuong.MaMH))
            {
                ModelState.AddModelError("MaMH", "Mã môn học không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load đề cương từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingDeCuong = await _context.DeCuong
                        .Include(dc => dc.MonHoc)
                        .FirstOrDefaultAsync(dc => dc.MaDC == deCuong.MaDC);

                    if (existingDeCuong == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy đề cương với MaDC = {deCuong.MaDC}");
                        ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", deCuong.MaMH);
                        return View("DeCuong/Edit", deCuong);
                    }

                    // Cập nhật các trường cần thiết
                    existingDeCuong.MaMH = deCuong.MaMH;
                    existingDeCuong.MoTa = deCuong.MoTa;
                    existingDeCuong.MucTieu = deCuong.MucTieu;
                    existingDeCuong.NgayCapNhat = deCuong.NgayCapNhat;

                    // Không cần cập nhật navigation properties (MonHoc) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingDeCuong).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật đề cương thành công!";
                    return RedirectToAction("DeCuongIndex");
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
                }
            }

            // Nếu có lỗi, trả lại view với danh sách môn học
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", deCuong.MaMH);
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
            ViewBag.GiangVienList = new SelectList(_context.GiangVien, "MaGV", "HoTen");
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH");
            ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop");
            return View("PhanCongGiangDay/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhanCongGiangDay(PhanCongGiangDay phanCongGiangDay)
        {
            if (_context.PhanCongGiangDay.Any(p => p.MaPCGD == phanCongGiangDay.MaPCGD))
            {
                ModelState.AddModelError("MaPC", "Mã phân công này đã tồn tại");
                return View("PhanCongGiangDay/Create", phanCongGiangDay);
            }
            try
            {
                _context.PhanCongGiangDay.Add(phanCongGiangDay);
                _context.SaveChanges();
                return RedirectToAction("PhanCongGiangDayIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            ViewBag.GiangVienList = new SelectList(_context.GiangVien, "MaGV", "HoTen", phanCongGiangDay.MaGV);
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", phanCongGiangDay.MaMH);
            ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop", phanCongGiangDay.MaLop);
            return View("PhanCongGiangDay/Create", phanCongGiangDay);
        }

        public IActionResult EditPhanCongGiangDay(string id)
        {
            var phanCongGiangDay = _context.PhanCongGiangDay
            .Include(p => p.GiangVien)
            .Include(p => p.MonHoc)
            .Include(p => p.LopHoc)
            .FirstOrDefault(p => p.MaPCGD == id);
            if (phanCongGiangDay == null) return NotFound();

            ViewBag.GiangVienList = new SelectList(_context.GiangVien, "MaGV", "HoTen", phanCongGiangDay.MaGV);
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", phanCongGiangDay.MaMH);
            ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop", phanCongGiangDay.MaLop);
            return View("PhanCongGiangDay/Edit", phanCongGiangDay);
        }
        
        [HttpPost]
        public async Task<IActionResult> EditPhanCongGiangDay([Bind("MaPCGD,MaGV,MaMH,MaLop,HocKy,NamHoc")] PhanCongGiangDay phanCongGiangDay)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("GiangVien");
            ModelState.Remove("MonHoc");
            ModelState.Remove("LopHoc");

            // Kiểm tra xem MaGV có tồn tại trong bảng GiangVien không
            if (!string.IsNullOrEmpty(phanCongGiangDay.MaGV) && !_context.GiangVien.Any(gv => gv.MaGV == phanCongGiangDay.MaGV))
            {
                ModelState.AddModelError("MaGV", "Mã giảng viên không tồn tại.");
            }

            // Kiểm tra xem MaMH có tồn tại trong bảng MonHoc không
            if (!string.IsNullOrEmpty(phanCongGiangDay.MaMH) && !_context.MonHoc.Any(mh => mh.MaMH == phanCongGiangDay.MaMH))
            {
                ModelState.AddModelError("MaMH", "Mã môn học không tồn tại.");
            }

            // Kiểm tra xem MaLop có tồn tại trong bảng LopHoc không
            if (!string.IsNullOrEmpty(phanCongGiangDay.MaLop) && !_context.LopHoc.Any(lh => lh.MaLop == phanCongGiangDay.MaLop))
            {
                ModelState.AddModelError("MaLop", "Mã lớp học không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load phân công giảng dạy từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingPhanCongGiangDay = await _context.PhanCongGiangDay
                        .Include(pcgd => pcgd.GiangVien)
                        .Include(pcgd => pcgd.MonHoc)
                        .Include(pcgd => pcgd.LopHoc)
                        .FirstOrDefaultAsync(pcgd => pcgd.MaPCGD == phanCongGiangDay.MaPCGD);

                    if (existingPhanCongGiangDay == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy phân công giảng dạy với MaPCGD = {phanCongGiangDay.MaPCGD}");
                        ViewBag.GiangVienList = new SelectList(_context.GiangVien, "MaGV", "HoTen", phanCongGiangDay.MaGV);
                        ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", phanCongGiangDay.MaMH);
                        ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop", phanCongGiangDay.MaLop);
                        return View("PhanCongGiangDay/Edit", phanCongGiangDay);
                    }

                    // Cập nhật các trường cần thiết
                    existingPhanCongGiangDay.MaGV = phanCongGiangDay.MaGV;
                    existingPhanCongGiangDay.MaMH = phanCongGiangDay.MaMH;
                    existingPhanCongGiangDay.MaLop = phanCongGiangDay.MaLop;
                    existingPhanCongGiangDay.HocKy = phanCongGiangDay.HocKy;
                    existingPhanCongGiangDay.NamHoc = phanCongGiangDay.NamHoc;

                    // Không cần cập nhật navigation properties (GiangVien, MonHoc, LopHoc) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingPhanCongGiangDay).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật phân công giảng dạy thành công!";
                    return RedirectToAction("PhanCongGiangDayIndex");
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

            // Nếu có lỗi, trả lại view với danh sách giảng viên, môn học và lớp học
            ViewBag.GiangVienList = new SelectList(_context.GiangVien, "MaGV", "HoTen", phanCongGiangDay.MaGV);
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", phanCongGiangDay.MaMH);
            ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop", phanCongGiangDay.MaLop);
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
            var taiLieuList = _context.TaiLieu
            .Include(t => t.BaiGiang) // Lấy thông tin bài giảng
            .ToList();
            return View("TaiLieu/Index", taiLieuList);
        }

        public IActionResult CreateTaiLieu()
        {  
            ViewBag.BaiGiangList = new SelectList(_context.BaiGiang, "MaBG", "TieuDe");
            return View("TaiLieu/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaiLieu(TaiLieu taiLieu)
        {   
            if (_context.TaiLieu.Any(t => t.MaTL == taiLieu.MaTL))
            {
                ModelState.AddModelError("MaTL", "Mã tài liệu này đã tồn tại");
                return View("TaiLieu/Create", taiLieu);
            }
            try
            {
                _context.TaiLieu.Add(taiLieu);
                _context.SaveChanges();
                return RedirectToAction("TaiLieuIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            ViewBag.BaiGiangList = new SelectList(_context.BaiGiang, "MaBG", "TieuDe", taiLieu.MaBG);
            return View("TaiLieu/Create", taiLieu);
        }

        public IActionResult EditTaiLieu(string id)
        {
            var taiLieu = _context.TaiLieu.Find(id);
            if (taiLieu == null) return NotFound();
            ViewBag.BaiGiangList = new SelectList(_context.BaiGiang, "MaBG", "TieuDe", taiLieu.MaBG);
            return View("TaiLieu/Edit", taiLieu);
        }

        [HttpPost]
        public async Task<IActionResult> EditTaiLieu([Bind("MaTL,TenTL,MaBG,DuongDan")] TaiLieu taiLieu)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("BaiGiang");

            // Kiểm tra xem MaBG có tồn tại trong bảng BaiGiang không
            if (!string.IsNullOrEmpty(taiLieu.MaBG) && !_context.BaiGiang.Any(bg => bg.MaBG == taiLieu.MaBG))
            {
                ModelState.AddModelError("MaBG", "Mã bài giảng không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load tài liệu từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingTaiLieu = await _context.TaiLieu
                        .Include(tl => tl.BaiGiang)
                        .FirstOrDefaultAsync(tl => tl.MaTL == taiLieu.MaTL);

                    if (existingTaiLieu == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy tài liệu với MaTL = {taiLieu.MaTL}");
                        ViewBag.BaiGiangList = new SelectList(_context.BaiGiang, "MaBG", "TieuDe", taiLieu.MaBG);
                        return View("TaiLieu/Edit", taiLieu);
                    }

                    // Cập nhật các trường cần thiết
                    existingTaiLieu.TenTL = taiLieu.TenTL;
                    existingTaiLieu.MaBG = taiLieu.MaBG;
                    existingTaiLieu.DuongDan = taiLieu.DuongDan;

                    // Không cần cập nhật navigation properties (BaiGiang) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingTaiLieu).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật tài liệu thành công!";
                    return RedirectToAction("TaiLieuIndex");
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
                }
            }

            // Nếu có lỗi, trả lại view với danh sách bài giảng
            ViewBag.BaiGiangList = new SelectList(_context.BaiGiang, "MaBG", "TieuDe", taiLieu.MaBG);
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
            var dangKyLopHocList = _context.DangKyLopHoc
            .Include(d => d.SinhVien) // Lấy thông tin sinh viên
            .Include(d => d.LopHoc) // Lấy thông tin lớp học
            .ToList();
            return View("DangKyLopHoc/Index", dangKyLopHocList);
        }

        public IActionResult CreateDangKyLopHoc()
        {   
            ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen");
            ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop");
            return View("DangKyLopHoc/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDangKyLopHoc(DangKyLopHoc dangKyLopHoc)
        {
            try
            {
                _context.DangKyLopHoc.Add(dangKyLopHoc);
                _context.SaveChanges();
                return RedirectToAction("DangKyLopHocIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", dangKyLopHoc.MaSV);
            ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop", dangKyLopHoc.MaLopHoc);
            return View("DangKyLopHoc/Create", dangKyLopHoc);
        }

        public IActionResult EditDangKyLopHoc(string maSV, string maLop)
        {
            var dangKyLopHoc = _context.DangKyLopHoc
                .Include(dk => dk.SinhVien)
                .Include(dk => dk.LopHoc)
                .FirstOrDefault(dk => dk.MaSV == maSV && dk.MaLopHoc == maLop);

            if (dangKyLopHoc == null) return NotFound();

            ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", dangKyLopHoc.MaSV);
            ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop", dangKyLopHoc.MaLopHoc);
            return View("DangKyLopHoc/Edit", dangKyLopHoc);
        }

        [HttpPost]
        public async Task<IActionResult> EditDangKyLopHoc([Bind("MaSV,MaLopHoc,NgayDangKy")] DangKyLopHoc dangKyLopHoc)
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

            if (ModelState.IsValid)
            {
                try
                {
                    // Load đăng ký lớp học từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingDangKyLopHoc = await _context.DangKyLopHoc
                        .Include(dk => dk.SinhVien)
                        .Include(dk => dk.LopHoc)
                        .FirstOrDefaultAsync(dk => dk.MaSV == dangKyLopHoc.MaSV && dk.MaLopHoc == dangKyLopHoc.MaLopHoc);

                    if (existingDangKyLopHoc == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy đăng ký lớp học với MaSV = {dangKyLopHoc.MaSV} và MaLopHoc = {dangKyLopHoc.MaLopHoc}");
                        ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", dangKyLopHoc.MaSV);
                        ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop", dangKyLopHoc.MaLopHoc);
                        return View("DangKyLopHoc/Edit", dangKyLopHoc);
                    }

                    // Cập nhật các trường cần thiết
                    existingDangKyLopHoc.NgayDangKy = dangKyLopHoc.NgayDangKy;

                    // Không cần cập nhật MaSV và MaLopHoc vì chúng là khóa chính và không nên thay đổi
                    // Không cần cập nhật navigation properties (SinhVien, LopHoc) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingDangKyLopHoc).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật đăng ký lớp học thành công!";
                    return RedirectToAction("DangKyLopHocIndex");
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
                }
            }

            // Nếu có lỗi, trả lại view với danh sách sinh viên và lớp học
            ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", dangKyLopHoc.MaSV);
            ViewBag.LopHocList = new SelectList(_context.LopHoc, "MaLop", "TenLop", dangKyLopHoc.MaLopHoc);
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
            var giangVienList = _context.GiangVien
            .Include(g => g.Khoa) // Lấy thông tin khoa
            .ToList();
            return View("GiangVien/Index", giangVienList);
        }

        public IActionResult CreateGiangVien()
        {
            // Chuyển đổi List<Khoa> thành IEnumerable<SelectListItem>
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa");
            return View("GiangVien/Create");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGiangVien(GiangVien giangVien)
        {
            if (_context.GiangVien.Any(g => g.MaGV == giangVien.MaGV))
            {
                ModelState.AddModelError("MaGV", "Mã giảng viên này đã tồn tại");
                return View("GiangVien/Create", giangVien);
            }
            try
            {
                _context.GiangVien.Add(giangVien);
                _context.SaveChanges();
                TempData["Success"] = "Thêm mới giảng viên thành công!";
                return RedirectToAction("GiangVienIndex");
            }   
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
            // Chuyển đổi List<Khoa> thành IEnumerable<SelectListItem>
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa");
            return View("GiangVien/Create", giangVien);
        }

        public IActionResult EditGiangVien(string id)
        {
            var giangVien = _context.GiangVien.Find(id);
            if (giangVien == null) return NotFound();
            // Chuyển đổi List<Khoa> thành IEnumerable<SelectListItem>
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", giangVien.MaKhoa);
            return View("GiangVien/Edit", giangVien);
        }

        [HttpPost]
        public async Task<IActionResult> EditGiangVien([Bind("MaGV,HoTen,Email,MaKhoa,NgayNhanViec")] GiangVien giangVien)
        {
            // Xóa các lỗi validation liên quan đến navigation properties
            ModelState.Remove("Khoa");
            ModelState.Remove("PhanCongGiangDays");

            // Kiểm tra xem MaKhoa có tồn tại trong bảng Khoa không
            if (!string.IsNullOrEmpty(giangVien.MaKhoa) && !_context.Khoa.Any(k => k.MaKhoa == giangVien.MaKhoa))
            {
                ModelState.AddModelError("MaKhoa", "Mã khoa không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load giảng viên từ cơ sở dữ liệu, bao gồm navigation properties
                    var existingGiangVien = await _context.GiangVien
                        .Include(g => g.Khoa)
                        .Include(g => g.PhanCongGiangDays)
                        .FirstOrDefaultAsync(g => g.MaGV == giangVien.MaGV);

                    if (existingGiangVien == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy giảng viên với MaGV = {giangVien.MaGV}");
                        ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", giangVien.MaKhoa);
                        return View("GiangVien/Edit", giangVien);
                    }

                    // Cập nhật các trường cần thiết
                    existingGiangVien.HoTen = giangVien.HoTen;
                    existingGiangVien.Email = giangVien.Email;
                    existingGiangVien.MaKhoa = giangVien.MaKhoa;
                    existingGiangVien.NgayNhanViec = giangVien.NgayNhanViec;

                    // Không cần cập nhật navigation properties (Khoa, PhanCongGiangDays) vì chúng đã được load từ cơ sở dữ liệu

                    _context.Entry(existingGiangVien).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật giảng viên thành công!";
                    return RedirectToAction("GiangVienIndex");
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
                }
            }

            // Nếu có lỗi, trả lại view với danh sách khoa
            ViewBag.KhoaList = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", giangVien.MaKhoa);
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