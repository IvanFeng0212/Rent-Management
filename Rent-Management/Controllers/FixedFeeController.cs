using CoreModules.Models;
using CoreModules.Services;
using Microsoft.AspNetCore.Mvc;

namespace Rent_Management.Controllers
{
    public class FixedFeeController : Controller
    {
        private readonly FixedFeeService _fixedFeeService;
        private readonly SystemEnumService _systemEnumService;

        public FixedFeeController(FixedFeeService fixedFeeService, SystemEnumService systemEnumService)
        {
            this._fixedFeeService = fixedFeeService;
            this._systemEnumService = systemEnumService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new FixedFeeViewModel()
            {
                FixedFees = await this._fixedFeeService.GetAllAsync(),
                FixedFeeItems = await this._systemEnumService.GetByTypeAsync(nameof(FixedFee))
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("FixedFee")]
        public async Task<IActionResult> FixedFee(FixedFee data)
        {
            if (ModelState.IsValid)
            {
                await this._fixedFeeService.AddAsync(data);
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Route("FixedFee")]
        public async Task<IActionResult> FixedFee(string itemId)
        {
            await this._fixedFeeService.DeleteAsync(itemId);

            return Ok();
        }
    }
}
