using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ServiceCenterWeb.Models;

namespace ServiceCenterWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult About()
        {
            ViewData["Message"] = "Сервисный центр компьютерной техники.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Контактная информация нашей компании.";

            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
