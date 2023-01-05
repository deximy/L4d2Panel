using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L4d2PanelBackend.Market.API.Controllers
{
    [Route("packages")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPackagesList()
        {
            return Ok();
        }
    }
}
