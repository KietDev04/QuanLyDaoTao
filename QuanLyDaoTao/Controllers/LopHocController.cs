using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDaoTaoWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace QuanLyDaoTaoWeb.Controllers
{
    [Authorize(Roles = "Admin,GiangVien")]
    public class LopHocController : AdminController
    {
        public LopHocController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
            : base(context, userManager)
        {
        }

        public new IActionResult Index()
        {
            var lopHocList = _context.LopHoc
                .Include(l => l.ChuongTrinhDaoTao)
                .Include(l => l.DangKyLopHocs)
                .ToList();
            return View(lopHocList);
        }

        public IActionResult Create()
        {
            ViewBag.ChuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCT", "TenCT");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LopHoc lopHoc)
        {
            if (_context.LopHoc.Any(l => l.MaLop == lopHoc.MaLop))
            {
                ModelState.AddModelError("MaLop", "Mã lớp học này đã tồn tại");
                ViewBag.ChuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCT", "TenCT", lopHoc.MaCTDT);
                return View(lopHoc);
            }

            try
            {
                _context.LopHoc.Add(lopHoc);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm mới lớp học thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }

            ViewBag.ChuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCT", "TenCT", lopHoc.MaCTDT);
            return View(lopHoc);
        }

        public IActionResult Edit(string id)
        {
            var lopHoc = _context.LopHoc
                .Include(l => l.ChuongTrinhDaoTao)
                .FirstOrDefault(l => l.MaLop == id);

            if (lopHoc == null) return NotFound();

            ViewBag.ChuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCT", "TenCT", lopHoc.MaCTDT);
            return View(lopHoc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("MaLop,MaCTDT,TenLop")] LopHoc lopHoc)
        {
            if (id != lopHoc.MaLop)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingLopHoc = await _context.LopHoc.FindAsync(lopHoc.MaLop);
                    if (existingLopHoc == null)
                    {
                        ModelState.AddModelError("", $"Không tìm thấy lớp học với MaLop = {lopHoc.MaLop}");
                        ViewBag.ChuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCT", "TenCT", lopHoc.MaCTDT);
                        return View(lopHoc);
                    }

                    existingLopHoc.MaCTDT = lopHoc.MaCTDT;
                    existingLopHoc.TenLop = lopHoc.TenLop;

                    _context.Entry(existingLopHoc).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật lớp học thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
                }
            }

            ViewBag.ChuongTrinhDaoTaoList = new SelectList(_context.ChuongTrinhDaoTao, "MaCT", "TenCT", lopHoc.MaCTDT);
            return View(lopHoc);
        }

        public IActionResult Delete(string id)
        {
            var lopHoc = _context.LopHoc.Find(id);
            if (lopHoc == null) return NotFound();
            _context.LopHoc.Remove(lopHoc);
            _context.SaveChanges();
            TempData["Success"] = "Xóa lớp học thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
