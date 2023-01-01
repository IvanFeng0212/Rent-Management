using CoreModules.Models;
using CoreModules.Services;
using Microsoft.AspNetCore.Mvc;

namespace Rent_Management.Controllers
{
    public class PublicFeeController : Controller
    {
        private readonly PublicFeeService _publicFeeService;
        private readonly SysEnumService _sysEnumService;

        public PublicFeeController(PublicFeeService publicFeeService, SysEnumService sysEnumService)
        {
            this._publicFeeService = publicFeeService;
            this._sysEnumService = sysEnumService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new PublicFeeViewModel()
            {
                PublicFees = await this._publicFeeService.GetAllAsync(),
                PublicFeeItems = await this._sysEnumService.GetByType(nameof(PublicFee))
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("PublicFee")]
        public async Task<IActionResult> PublicFee(PublicFee data)
        {
            if (ModelState.IsValid)
            {
                await this._publicFeeService.AddAsync(data);
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Route("PublicFee")]
        public async Task<IActionResult> PublicFee(string itemId)
        {
            await this._publicFeeService.DeleteAsync(itemId);

            return Ok();
        }
    }
}
