using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompilerController : ControllerBase
    {
        private readonly ILogger<CompilerController> _logger;

        public CompilerController(ILogger<CompilerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> ProcessInput(int idlangcode, string code)
        {
            return new List<WeatherForecast>();
            ;
        }
    }
}