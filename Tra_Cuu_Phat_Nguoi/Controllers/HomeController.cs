using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tra_Cuu_Phat_Nguoi.Models;

namespace Tra_Cuu_Phat_Nguoi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Tracuu(string ten)
        {
            // Trả về biển số xe đã nhập cho yêu cầu AJAX
            return Content(ten);
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
}
