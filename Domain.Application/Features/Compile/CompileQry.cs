using Domain.Application.Models;
using MediatR;

namespace Domain.Application.Features.CompileHandle
{
    public class CompileQry  : IRequest<RunResponseVM>
    {
        public int _IdCodeLang;
        public string _Code;

        public CompileQry (int idCode, string code = "") 
        {
            _IdCodeLang = idCode;
            _Code = code; 
        }        
    }
}

