using Domain.Application.Contracts;
using AutoMapper;
using MediatR;
using Domain.Application.Models;
using Domain.Core.Models;

namespace Domain.Application.Features.CompileHandle
{
    public class CompileHandler : IRequestHandler<CompileQry, RunResponseVM>
    {
        private ICodeProcessor _codeProcessor;
        private readonly IMapper _mapper;
        private readonly ICodeProcessorFactory _codeProcessorFactory;
        public CompileHandler(IMapper mapper, ICodeProcessorFactory codeProcessorFactory)
        {
            _mapper = mapper;
            _codeProcessorFactory = codeProcessorFactory;
        }

        public async Task<RunResponseVM> Handle(CompileQry request, CancellationToken cancellationToken)
        {
            try
            {
                _codeProcessor = _codeProcessorFactory.GetCompiler(request._IdCodeLang);
                var Execution = new RunResponse();
                if (_codeProcessor.checkSyntax(request._Code).IsOk == true)
                {
                    Execution = _codeProcessor.execute(request._Code);  
                }
                else 
                {
                    Execution =  new RunResponse
                    {
                        IdResponse = -1,
                        Output = "Error de sintaxis"
                    };
                }   
                return _mapper.Map<RunResponseVM>(Execution);                
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la ejecucion del code");
            }
        }
    }
}

