using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Controllers
{
    public class KhoasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhoasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var danhSachKhoa = _context.Khoas.ToList();
            if (danhSachKhoa == null || !danhSachKhoa.Any())
            {
                // Log hoặc debug để kiểm tra
                Console.WriteLine("Danh sách rỗng hoặc null");
            }
            return View(danhSachKhoa);
        }
        

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                _context.Khoas.Add(khoa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khoa);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var khoa = _context.Khoas.Find(id);
            if (khoa == null)
            {
                return NotFound();
            }
            return View(khoa);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                _context.Khoas.Update(khoa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khoa);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var khoa = _context.Khoas.Find(id);
            if (khoa == null)
            {
                return NotFound();
            }
            return View(khoa);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var khoa = _context.Khoas.Find(id);
            if (khoa != null)
            {
                _context.Khoas.Remove(khoa);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var khoa = _context.Khoas
                .Include(k => k.DeCuongs)
                .ThenInclude(dc => dc.BaiGiangs)
                .FirstOrDefault(k => k.Id == id);

            if (khoa == null)
            {
                return NotFound();
            }
            return View(khoa);
        }
    }
}