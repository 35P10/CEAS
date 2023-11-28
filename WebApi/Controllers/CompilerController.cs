using System.Net;
using Domain.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Application.Features.ProcessInputHandle;
using Domain.Application.Features.CompileHandle;
using Domain.Application.Models;
using Domain.Core.Models;

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

        [HttpPost]
        [Route("checkSyntax")]
        [ProducesResponseType(typeof(SyntaxResponseVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SyntaxResponseVM>> ProcessInput(InputCode inputCode)
        {
            var query = new ProcessInputQry(inputCode.idCode,inputCode.Code);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        [Route("run")]
        [ProducesResponseType(typeof(RunResponseVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RunResponseVM>> Compile( InputCode inputCode)
        {
            var query = new CompileQry(inputCode.idCode,inputCode.Code);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("/")]
        [ProducesResponseType(typeof(SyntaxResponseVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SyntaxResponseVM>> Idle()
        {
            var temp = new InputCode(){
                idCode = 1,
                Code = "print('Hello World')"
            };
            var query = new ProcessInputQry(temp.idCode,temp.Code);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("test")]
        [ProducesResponseType(typeof(SyntaxResponseVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SyntaxResponseVM>> Idle2()
        {
            var temp = new InputCode(){
                idCode = 1,
                Code = "print('Hello World')"
            };
            var query = new ProcessInputQry(temp.idCode,temp.Code);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        [Route("test")]
        [ProducesResponseType(typeof(SyntaxResponseVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SyntaxResponseVM>> Idle3()
        {
            var temp = new InputCode(){
                idCode = 1,
                Code = "print('Hello World')"
            };
            var query = new ProcessInputQry(temp.idCode,temp.Code);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}