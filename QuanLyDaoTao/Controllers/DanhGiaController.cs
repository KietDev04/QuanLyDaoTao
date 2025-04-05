using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTaoWeb.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

namespace QuanLyDaoTaoWeb.Controllers
{
    [Authorize(Roles = "Admin,SinhVien")]
    public class DanhGiaController : AdminController
    {
        public DanhGiaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
            : base(context, userManager)
        {
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                var danhGiaList = _context.DanhGia
                    .Include(d => d.SinhVien)
                    .Include(d => d.MonHoc)
                    .ToList();
                return View(danhGiaList);
            }
            else
            {
                var sinhVien = _context.SinhVien.FirstOrDefault(s => s.Email == user.Email);
                if (sinhVien == null) return NotFound();

                var danhGiaList = _context.DanhGia
                    .Include(d => d.SinhVien)
                    .Include(d => d.MonHoc)
                    .Where(d => d.MaSV == sinhVien.MaSV)
                    .ToList();
                return View(danhGiaList);
            }
        }

        public async Task<IActionResult> Create()
        {   
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen");
            }
            else
            {
                var sinhVien = _context.SinhVien.FirstOrDefault(s => s.Email == user.Email);
                if (sinhVien == null) return NotFound();
                ViewBag.SinhVienList = new SelectList(_context.SinhVien.Where(s => s.MaSV == sinhVien.MaSV), "MaSV", "HoTen");
            }

            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DanhGia danhGia)
        {   
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin)
            {
                var sinhVien = _context.SinhVien.FirstOrDefault(s => s.Email == user.Email);
                if (sinhVien == null || sinhVien.MaSV != danhGia.MaSV)
                {
                    return Forbid();
                }
            }

            if (_context.DanhGia.Any(d => d.MaDG == danhGia.MaDG))
            {
                ModelState.AddModelError("MaDG", "Mã đánh giá này đã tồn tại");
                return View(danhGia);
            }

            if (danhGia.DiemDanhGia < 1 || danhGia.DiemDanhGia > 10)
            {
                ModelState.AddModelError("DiemDanhGia", "Điểm đánh giá phải từ 1 đến 10");
                return View(danhGia);
            }

            try
            {
                danhGia.NgayDanhGia = DateTime.Now;
                _context.DanhGia.Add(danhGia);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm mới đánh giá thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }

            if (isAdmin)
            {
                ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", danhGia.MaSV);
            }
            else
            {
                var sinhVien = _context.SinhVien.FirstOrDefault(s => s.Email == user.Email);
                ViewBag.SinhVienList = new SelectList(_context.SinhVien.Where(s => s.MaSV == sinhVien.MaSV), "MaSV", "HoTen", danhGia.MaSV);
            }
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", danhGia.MaMH);
            return View(danhGia);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var danhGia = _context.DanhGia
                .Include(d => d.SinhVien)
                .Include(d => d.MonHoc)
                .FirstOrDefault(d => d.MaDG == id);

            if (danhGia == null) return NotFound();

            if (!isAdmin)
            {
                var sinhVien = _context.SinhVien.FirstOrDefault(s => s.Email == user.Email);
                if (sinhVien == null || sinhVien.MaSV != danhGia.MaSV)
                {
                    return Forbid();
                }
            }

            if (isAdmin)
            {
                ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", danhGia.MaSV);
            }
            else
            {
                var sinhVien = _context.SinhVien.FirstOrDefault(s => s.Email == user.Email);
                ViewBag.SinhVienList = new SelectList(_context.SinhVien.Where(s => s.MaSV == sinhVien.MaSV), "MaSV", "HoTen", danhGia.MaSV);
            }
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", danhGia.MaMH);
            return View(danhGia);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("MaDG,MaSV,MaMH,DiemDanhGia,NhanXet")] DanhGia danhGia)
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (id != danhGia.MaDG)
            {
                return NotFound();
            }

            if (!isAdmin)
            {
                var sinhVien = _context.SinhVien.FirstOrDefault(s => s.Email == user.Email);
                if (sinhVien == null || sinhVien.MaSV != danhGia.MaSV)
                {
                    return Forbid();
                }
            }

            if (danhGia.DiemDanhGia < 1 || danhGia.DiemDanhGia > 10)
            {
                ModelState.AddModelError("DiemDanhGia", "Điểm đánh giá phải từ 1 đến 10");
                return View(danhGia);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingDanhGia = await _context.DanhGia.FindAsync(id);
                    if (existingDanhGia == null)
                    {
                        return NotFound();
                    }

                    existingDanhGia.MaSV = danhGia.MaSV;
                    existingDanhGia.MaMH = danhGia.MaMH;
                    existingDanhGia.DiemDanhGia = danhGia.DiemDanhGia;
                    existingDanhGia.NhanXet = danhGia.NhanXet;

                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật đánh giá thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhGiaExists(danhGia.MaDG))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            if (isAdmin)
            {
                ViewBag.SinhVienList = new SelectList(_context.SinhVien, "MaSV", "HoTen", danhGia.MaSV);
            }
            else
            {
                var sinhVien = _context.SinhVien.FirstOrDefault(s => s.Email == user.Email);
                ViewBag.SinhVienList = new SelectList(_context.SinhVien.Where(s => s.MaSV == sinhVien.MaSV), "MaSV", "HoTen", danhGia.MaSV);
            }
            ViewBag.MonHocList = new SelectList(_context.MonHoc, "MaMH", "TenMH", danhGia.MaMH);
            return View(danhGia);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var danhGia = await _context.DanhGia.FindAsync(id);
            if (danhGia == null)
            {
                return NotFound();
            }

            _context.DanhGia.Remove(danhGia);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Xóa đánh giá thành công!";
            return RedirectToAction(nameof(Index));
        }

        private bool DanhGiaExists(string id)
        {
            return _context.DanhGia.Any(e => e.MaDG == id);
        }
    }
}
