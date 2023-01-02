using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rent_Management.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HeartbeatController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Heartbeat()
        {
            return Ok();
        }
    }
}
