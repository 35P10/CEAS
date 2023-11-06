using System.Net;
using Domain.Application;
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
        [Route("checkSyntax")]
        [ProducesResponseType(typeof(SyntaxStatusVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SyntaxStatusVM>> ProcessInput([FromQuery] int idlangcode, [FromQuery] string code)
        {
            var response = new SyntaxStatusVM();
            return Ok(response);
        }

        [HttpGet]
        [Route("run")]
        [ProducesResponseType(typeof(RunResponseVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RunResponseVM>> Compile([FromQuery] int idlangcode, [FromQuery] string code)
        {
            var response = new RunResponseVM();
            return Ok(response);
        }
    }
}