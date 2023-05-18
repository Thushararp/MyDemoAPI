using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Contracts;

namespace MyDemoAPI.APIControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyDemoAPIController : ControllerBase
    {
        private readonly ILogger<MyDemoAPIController> _logger;
        private readonly IStarRezGameService _starRezGameService;
        public MyDemoAPIController(ILogger<MyDemoAPIController> logger, IStarRezGameService starRezGameService)
        {
            _logger = logger;
            _starRezGameService = starRezGameService;

        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int from, [FromQuery] int to)
        {
            try
            {
                if (from < 1 || from > 1000)
                {
                    return BadRequest("Invalid 'from' value. It should be between 1 and 1000.");
                }

                if (to < 1 || to > 1000)
                {
                    return BadRequest("Invalid 'to' value. It should be between 1 and 1000.");
                }

                if (to < from)
                {
                    return BadRequest("Invalid 'to' value. It should be between 1 and 1000, and larger than 'from' value.");
                }
                List<AllDto> result = _starRezGameService.GetAll(from, to);

                return Ok(result);
            }
            catch
            {
                _logger.LogError("Error ocurred in GetAll()");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Validate([FromBody] ValidateDTO validatePayLoad)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool result = _starRezGameService.Validate(validatePayLoad);
                return Ok(result);
            }
            catch (Exception)
            {
                _logger.LogError("Error ocurred in Validate()");
                return StatusCode(500, "Internal server error");
            }


        }
    }
}
