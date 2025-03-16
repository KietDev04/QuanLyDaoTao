using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Controllers
{
    public class BaiGiangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaiGiangController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var danhSachBaiGiang = _context.BaiGiangs.Include(bg => bg.DeCuong).ToList();
            return View(danhSachBaiGiang);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.DeCuongId = _context.DeCuongs.ToList(); // Để chọn đề cương trong dropdown
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BaiGiang baiGiang)
        {
            if (ModelState.IsValid)
            {
                _context.BaiGiangs.Add(baiGiang);
                _context.SaveChanges();
                return RedirectToAction("Index", "Khoas"); // Quay về danh sách khóa học
            }
            ViewBag.DeCuongId = _context.DeCuongs.ToList();
            return View(baiGiang);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var baiGiang = _context.BaiGiangs.Find(id);
            if (baiGiang == null)
            {
                return NotFound();
            }
            ViewBag.DeCuongId = _context.DeCuongs.ToList();
            return View(baiGiang);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(BaiGiang baiGiang)
        {
            if (ModelState.IsValid)
            {
                _context.BaiGiangs.Update(baiGiang);
                _context.SaveChanges();
                return RedirectToAction("Index", "Khoas");
            }
            ViewBag.DeCuongId = _context.DeCuongs.ToList();
            return View(baiGiang);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var baiGiang = _context.BaiGiangs
                .Include(bg => bg.DeCuong) // Load thông tin DeCuong
                .FirstOrDefault(bg => bg.Id == id);
            if (baiGiang == null)
            {
                return NotFound();
            }
            return View(baiGiang);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var baiGiang = _context.BaiGiangs.Find(id);
            if (baiGiang != null)
            {
                _context.BaiGiangs.Remove(baiGiang);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Khoas");
        }

        public IActionResult Details(int id)
        {
            var baiGiang = _context.BaiGiangs
                .Include(bg => bg.DeCuong)
                .FirstOrDefault(bg => bg.Id == id);
            if (baiGiang == null)
            {
                return NotFound();
            }
            return View(baiGiang);
        }
    }
}