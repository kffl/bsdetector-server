using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BSDetector.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpGet("name")]
        public IActionResult Index()
        {
            return Ok("Lol");
        }

    }
}