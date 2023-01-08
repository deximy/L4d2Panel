using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using L4d2PanelBackend.API.Hubs;
using L4d2PanelBackend.API.Services;

namespace L4d2PanelBackend.API.Controllers
{
    [Route("processes")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly ILogger<ServerController> logger_;
        private readonly IHubContext<MessageHub> hub_context_;
        private readonly IProcessService process_service_;

        public ServerController(ILogger<ServerController> logger, IHubContext<MessageHub> hub_context, IProcessService process_service)
        {
            logger_ = logger;
            hub_context_ = hub_context;
            process_service_ = process_service;
        }

        //[HttpGet]
        //public IActionResult GetServerState()
        //{
        //    var process_state = server_service_.process_info;
        //    if (process_state == null || server_service_.is_running == false)
        //    {
        //        return Ok(
        //            new ViewModels.ServerState()
        //            {
        //                is_running = false,
        //                process_state = null,
        //                game_state = null,
        //            }
        //        );
        //    }

        //    if (process_state.has_exited == false)
        //    {
        //        return Ok(
        //            new ViewModels.ServerState()
        //            {
        //                is_running = true,
        //                process_state = new ViewModels.ServerProcessState()
        //                {
        //                    start_time = process_state.start_time,
        //                    has_exited = false,
        //                },
        //                game_state = new ViewModels.ServerGameState()
        //                {
        //                }
        //            }
        //        );
        //    }

        //    logger_.LogError($"Unexpected state: {{ is_running: {server_service_.is_running}, process_state: {process_state}}}");
        //    return StatusCode(StatusCodes.Status500InternalServerError);
        //    return Ok();
        //}

        [HttpGet]
        public async Task<IActionResult> GetServerProcessId([FromQuery] int? count)
        {
            return Ok(await process_service_.GetProcessStateId(count ?? 1));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServerProcessState([FromRoute] Guid id)
        {
            return Ok(await process_service_.GetProcessState(id));
        }

        //[HttpGet("history")]
        //public IActionResult GetLastServerStateHistory()
        //{
        //    if (process_service_.is_running != false)
        //    {
        //        logger_.LogInformation("Unable to get last history of server state because server is running.");
        //        return UnprocessableEntity("Server is running.");
        //    }

        //    var process_state = process_service_.process_info;
        //    if (process_state == null)
        //    {
        //        logger_.LogInformation("Nothing should be returned because it's the first time for server to run.");
        //        return NoContent();
        //    }

        //    return Ok(
        //        new ViewModels.ServerProcessState() {
        //            start_time = process_state.start_time,
        //            exit_code = process_state.exit_code,
        //            exit_time = process_state.exit_time,
        //            has_exited = true,
        //        }
        //    );
        //}

        [HttpPost]
        public async Task<IActionResult> RunServerAsync()
        {
            Guid? guid = null;
            try
            {
                logger_.LogInformation("Try to start server.");
                guid = await process_service_.RunServer(
                    (msg) =>
                    {
                        hub_context_.Clients.All.SendAsync("ReceiveMessage", msg);
                    }
                );
            }
            catch (InvalidOperationException)
            {
                logger_.LogInformation("Server failed to start. Reason: Server is running.");
                return Conflict();
            }

            logger_.LogInformation("Server started successfully.");
            return Ok(guid);
        }

        [HttpPatch]
        public async Task<IActionResult> ShutdownServerAsync()
        {
            try
            {
                logger_.LogInformation("Try to end server.");
                await process_service_.StopServer();
            }
            catch (InvalidOperationException)
            {
                logger_.LogInformation("Server failed to end. Reason: Server is not running.");
                return UnprocessableEntity();
            }

            return NoContent();
        }

        [HttpPost("command")]
        public IActionResult ExecCommand([FromForm] string command)
        {
            logger_.LogInformation($"Try to execute command: {command}");
            try
            {
                process_service_.ExecCommand(command);
            }
            catch (InvalidOperationException)
            {
                logger_.LogInformation("Server failed to end. Reason: Server is not running.");
                return UnprocessableEntity();
            }

            logger_.LogInformation($"Succeefully execute command: {command}.");
            return NoContent();
        }
    }
}
