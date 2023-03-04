using CoreModules.Models;
using CoreModules.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rent_Management.Models;
using System.Diagnostics;

namespace Rent_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AnnouncementService _announcementService;

        public HomeController(AnnouncementService announcementService)
        {
            this._announcementService = announcementService;
        }

        public async Task<IActionResult> Index()
        {
            Task.WaitAll(this._announcementService.DeleteNoMatchSystemEnumAsync(), Task.Delay(TimeSpan.FromSeconds(1)));

            ViewBag.Announcement = await this._announcementService.GetAnnouncementAsync();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}