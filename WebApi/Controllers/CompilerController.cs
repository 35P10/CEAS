using System.Net;
using Domain.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Application.Features.ProcessInputHandle;
using Domain.Application.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompilerController : ControllerBase
    {
        private readonly ILogger<CompilerController> _logger;
        private readonly IMediator _mediator;

        public CompilerController(ILogger<CompilerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("checkSyntax")]
        [ProducesResponseType(typeof(SyntaxResponseVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SyntaxResponseVM>> ProcessInput([FromQuery] int idlangcode, [FromQuery] string code)
        {
            var query = new ProcessInputQry(idlangcode,code);
            var response = await _mediator.Send(query);
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