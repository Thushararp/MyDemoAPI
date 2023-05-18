using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyDemoAPI.APIControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyDemoAPIController : ControllerBase
    {
        private readonly ILogger<MyDemoAPIController> _logger;
        public MyDemoAPIController(ILogger<MyDemoAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetDemoAPIResult()
        {
            try
            {
                return Ok("Hello from MyDemoAPI");
            }
            catch
            {
                _logger.LogError("Error ocurred in GetDemoAPIResult()");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
