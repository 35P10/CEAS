using Domain.Application.Contracts;
using AutoMapper;
using MediatR;
using Domain.Application.Models;

namespace Domain.Application.Features.ProcessInputHandle
{
    public class ProcessInputQryHandler:IRequestHandler<ProcessInputQry, SyntaxResponseVM>
    {
        private ICodeProcessor _codeProcessor;
        private readonly IMapper _mapper;
        private readonly ICodeProcessorFactory _codeProcessorFactory;
        public ProcessInputQryHandler (IMapper mapper, ICodeProcessorFactory codeProcessorFactory) 
        {
            _mapper = mapper;
            _codeProcessorFactory = codeProcessorFactory;
        }        

        public async Task<SyntaxResponseVM> Handle(ProcessInputQry request, CancellationToken cancellationToken)
        {
            try
            {
                _codeProcessor = _codeProcessorFactory.GetCompiler(request._IdCodeLang);
                
                var sintaxis = _codeProcessor.checkSyntax(request._Code);
                return _mapper.Map<SyntaxResponseVM>(sintaxis);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el analisis de sintaxis");
            }
        }
    }
}

