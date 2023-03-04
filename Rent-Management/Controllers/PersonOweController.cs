using CoreModules.Models;
using CoreModules.Services;
using Microsoft.AspNetCore.Mvc;

namespace Rent_Management.Controllers
{
    public class PersonOweController : Controller
    {
        private readonly PersonOweService _personOweService;
        private readonly SystemEnumService _systemEnumService;

        public PersonOweController(PersonOweService personOweService, SystemEnumService systemEnumService)
        {
            this._personOweService = personOweService;
            this._systemEnumService = systemEnumService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new PersonViewModel()
            {
                PersonFees = await this._personOweService.GetAllAsync(),
                PersonItems = await this._systemEnumService.GetByTypeAsync(nameof(PersonOwe))
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("PersonOwe")]
        public async Task<IActionResult> FixedFee(PersonOwe data)
        {
            if (ModelState.IsValid)
            {
                await this._personOweService.AddAsync(data);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("PersonOwe")]
        public async Task<IActionResult> FixedFee(string itemId)
        {
            await this._personOweService.DeleteAsync(itemId);

            return Ok();
        }
    }
}
