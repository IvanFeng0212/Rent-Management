using CoreModules.Models;
using CoreModules.Services;
using Microsoft.AspNetCore.Mvc;

namespace Rent_Management.Controllers
{
    public class SystemEnumController : Controller
    {
        private readonly SystemEnumService _systemEnumService;

        public SystemEnumController(SystemEnumService systemEnumService)
        {
            this._systemEnumService = systemEnumService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new SystemEnumViewModel()
            {
                SysEnums = await this._systemEnumService.GetAllAsync(),
                ItemList = this._systemEnumService.GetItemList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("SysEnum")]
        public async Task<IActionResult> SysEnum(SystemEnum data)
        {
            if (ModelState.IsValid)
            {
                await this._systemEnumService.AddAsync(data);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("SysEnum")]
        public async Task<IActionResult> SysEnum(string itemId)
        {
            await this._systemEnumService.DeleteAsync(itemId);

            return Ok();
        }
    }
}
