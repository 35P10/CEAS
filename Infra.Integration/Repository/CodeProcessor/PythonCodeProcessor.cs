using Domain.Application.Contracts;
using Domain.Core.Models;

namespace Infra.Integration.Repository.CodeProcessor
{
    public class PythonCodeProcessor:ICodeProcessor
    {
        public SyntaxStatus checkSyntax(string code)
        {
            throw new NotImplementedException();
        }


        public async Task<SyntaxStatus> CheckSyntaxAsync(string codeToCheck)
        {
            throw new NotImplementedException();
        }

        public SyntaxStatus compile(string code)
        {
            throw new NotImplementedException();
        }
    }
}
