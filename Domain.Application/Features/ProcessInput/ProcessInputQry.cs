using Domain.Application.Models;
using MediatR;

namespace Domain.Application.Features.ProcessInputHandle
{
    public class ProcessInputQry  : IRequest<SyntaxResponseVM>
    {
        public int _IdCodeLang;
        public string _Code;

        public ProcessInputQry (int idCode, string code = "") 
        {
            _IdCodeLang = idCode;
            _Code = code; 
        }        
    }
}

