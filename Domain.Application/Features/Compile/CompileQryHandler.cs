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
                var sintaxis = new SyntaxStatus();
                if(_codeProcessor.checkSyntax(request._Code).IsOk==true)
                   {
                    sintaxis = _codeProcessor.compile(request._Code);  
                   }
                else 
                   {
                    sintaxis = new SyntaxStatus
                    {
                        IsOk = false,
                    };
                }   
                   return _mapper.Map<RunResponseVM>(sintaxis);                
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el analisis de sintaxis");
            }
        }
    }
}

