using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Controllers
{
    public class DeCuongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeCuongController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var danhSachDeCuong = _context.DeCuongs.Include(dc => dc.Khoa).ToList();
            return View(danhSachDeCuong);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.KhoaId = _context.Khoas.ToList(); // Để chọn khóa học trong dropdown
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeCuong deCuong)
        {
            if (ModelState.IsValid)
            {
                _context.DeCuongs.Add(deCuong);
                _context.SaveChanges();
                return RedirectToAction("Index", "Khoas"); // Quay về danh sách khóa học
            }
            ViewBag.KhoaId = _context.Khoas.ToList();
            return View(deCuong);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var deCuong = _context.DeCuongs.Find(id);
            if (deCuong == null)
            {
                return NotFound();
            }
            ViewBag.KhoaId = _context.Khoas.ToList();
            return View(deCuong);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(DeCuong deCuong)
        {
            if (ModelState.IsValid)
            {
                _context.DeCuongs.Update(deCuong);
                _context.SaveChanges();
                return RedirectToAction("Index", "Khoas");
            }
            ViewBag.KhoaId = _context.Khoas.ToList();
            return View(deCuong);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var deCuong = _context.DeCuongs
                .Include(dc => dc.Khoa) // Load thông tin Khoa
                .FirstOrDefault(dc => dc.Id == id);
            if (deCuong == null)
            {
                return NotFound();
            }
            return View(deCuong);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var deCuong = _context.DeCuongs.Find(id);
            if (deCuong != null)
            {
                _context.DeCuongs.Remove(deCuong);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Khoas");
        }

        public IActionResult Details(int id)
        {
            var deCuong = _context.DeCuongs
                .Include(dc => dc.Khoa)
                .Include(dc => dc.BaiGiangs)
                .FirstOrDefault(dc => dc.Id == id);
            if (deCuong == null)
            {
                return NotFound();
            }
            return View(deCuong);
        }
    }
}