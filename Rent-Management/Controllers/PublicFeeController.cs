using CoreModules.Models;
using CoreModules.Services;
using Microsoft.AspNetCore.Mvc;

namespace Rent_Management.Controllers
{
    public class PublicFeeController : Controller
    {
        private readonly PublicFeeService _publicFeeService;
        private readonly SystemEnumService _systemEnumService;

        public PublicFeeController(PublicFeeService publicFeeService, SystemEnumService systemEnumService)
        {
            this._publicFeeService = publicFeeService;
            this._systemEnumService = systemEnumService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new PublicFeeViewModel()
            {
                PublicFees = await this._publicFeeService.GetAllAsync(),
                PublicFeeItems = await this._systemEnumService.GetByTypeAsync(nameof(PublicFee))
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

            return Ok();
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
