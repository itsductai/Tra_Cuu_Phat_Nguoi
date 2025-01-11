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
        async public Task<IActionResult> Tracuu(string ten)
        {
            // Trả về cho yêu cầu AJAX
            
            return Content(await ProcesstraCuu(ten));
        }

         async Task<string> ProcesstraCuu(string ten)
        {
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://www.csgt.vn/tra-cuu-phuong-tien-vi-pham.html?&LoaiXe=1&BienKiemSoat={ten}");
                if(response != null && response.IsSuccessStatusCode)
                {
                    var html = await response.Content.ReadAsStringAsync();

                    //Tao doi tuong htmlAgilitypack de phan tich html
                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(html);

                    //Tim phan tu co id = "bodyPrint123"
                    var targetNode = htmlDoc.GetElementbyId("bodyPrint123");

                    // Nếu tìm thấy phần tử, trả về nội dung
                    if (targetNode != null)
                    {
                        return targetNode.InnerHtml; // Lấy nội dung bên trong phần tử
                    }
                    else
                    {
                        return "Không tìm thấy phần tử có id = bodyPrint123.";
                    }
                }
            }
            return "Không truy cập được hoặc không nhận được dữ liệu";
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
