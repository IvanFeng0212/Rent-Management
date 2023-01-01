using CoreModules.Models;
using CoreModules.Services;
using Microsoft.AspNetCore.Mvc;

namespace Rent_Management.Controllers
{
    public class PersonTransController : Controller
    {
        private readonly PersonTransService _personTransService;
        private readonly SysEnumService _sysEnumService;

        public PersonTransController(PersonTransService personTransService, SysEnumService sysEnumService)
        {
            this._personTransService = personTransService;
            this._sysEnumService = sysEnumService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new PersonViewModel()
            {
                PersonFees = await this._personTransService.GetAllAsync(),
                PersonItems = await this._sysEnumService.GetByTypeAsync(nameof(Person))
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("PersonTrans")]
        public async Task<IActionResult> FixedFee(Person data)
        {
            if (ModelState.IsValid)
            {
                await this._personTransService.AddAsync(data);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("PersonTrans")]
        public async Task<IActionResult> FixedFee(string itemId)
        {
            await this._personTransService.DeleteAsync(itemId);

            return Ok();
        }
    }
}
