using Microsoft.AspNetCore.Mvc;
using Rent_Management.Models;
using System.Diagnostics;

namespace Rent_Management.Controllers
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
            var secretKey = Environment.GetEnvironmentVariable("RM_SecretKey");

            ViewBag.Env = string.IsNullOrEmpty(secretKey) ? "None" : secretKey;

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
}