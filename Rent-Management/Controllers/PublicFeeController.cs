using CoreModules.Models;
using CoreModules.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Rent_Management.Controllers
{
    public class PublicFeeController : Controller
    {
        private readonly PublicFeeService _publicFeeService;

        public PublicFeeController(PublicFeeService publicFeeService)
        {
            this._publicFeeService = publicFeeService;
        }

        public async Task<IActionResult> Index()
        {
            var publicFees = await this._publicFeeService.GetAllAsync();

            return View(publicFees);
        }

        [HttpPost]
        [Route("PublicFee")]
        public async Task<IActionResult> PublicFee(PublicFee data)
        {
            await this._publicFeeService.AddAsync(data);

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
