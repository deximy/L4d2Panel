using Microsoft.AspNetCore.Mvc;

namespace L4d2PanelBackend.API.Controllers
{
    [Route("init")]
    [ApiController]
    public class InitController : ControllerBase
    {
        //private readonly ILoggerService logger_service_;
        //private readonly OptionsService options_service_;

        //public InitController(ILoggerService logger_service, OptionsService options_service)
        //{
        //    logger_service_ = logger_service;
        //    options_service_ = options_service;
        //}

        //[HttpPost]
        //public async Task<IActionResult> InitDatabase([FromBody] string key)
        //{
        //    logger_service_.Db_.DbMaintenance.CreateDatabase();
        //    if (logger_service_.Db_.DbMaintenance.IsAnyTable("Options"))
        //    {
        //        if ((await options_service_.Query(x => x.name == "key")).Any())
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    return Ok();
        //}
    }
}
