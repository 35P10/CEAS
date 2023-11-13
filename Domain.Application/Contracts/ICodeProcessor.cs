using Domain.Core.Models;

namespace Domain.Application.Contracts
{
    public interface ICodeProcessor
    {
        SyntaxStatus checkSyntax(string code);
        RunResponse execute(string code);
    }
}
