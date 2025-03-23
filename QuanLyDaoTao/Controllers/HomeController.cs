using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDaoTao.Models;
using QuanLyDaoTaoWeb.Models;  // Thêm namespace này
using Microsoft.Extensions.Logging; // Thêm namespace này

namespace QuanLyDaoTao.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IActionResult Index()
    {
        if (User.IsInRole("Admin"))
        {
            return RedirectToAction("IndexAdmin", "Admin");
        }
        else if (User.IsInRole("GiangVien"))
        {
            return RedirectToAction("IndexGiangVien", "GiangVien");
        }
        else if (User.IsInRole("SinhVien"))
        {
            return RedirectToAction("IndexSinhVien", "SinhVien");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
