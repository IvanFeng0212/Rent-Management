using CoreModules.Models;
using CoreModules.Services;
using Microsoft.AspNetCore.Mvc;

namespace Rent_Management.Controllers
{
    public class SysEnumController : Controller
    {
        private readonly SysEnumService _sysEnumService;

        public SysEnumController(SysEnumService sysEnumService)
        {
            this._sysEnumService = sysEnumService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new SysEnumViewModel()
            {
                SysEnums = await this._sysEnumService.GetAllAsync(),
                ItemList = this._sysEnumService.GetItemList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("SysEnum")]
        public async Task<IActionResult> SysEnum(SysEnum data)
        {
            if (ModelState.IsValid)
            {
                await this._sysEnumService.AddAsync(data);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("SysEnum")]
        public async Task<IActionResult> SysEnum(string itemId)
        {
            await this._sysEnumService.DeleteAsync(itemId);

            return Ok();
        }
    }
}
