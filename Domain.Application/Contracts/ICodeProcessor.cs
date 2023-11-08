using Domain.Core.Models;

namespace Domain.Application.Contracts
{
    public interface ICodeProcessor
    {
        SyntaxStatus checkSyntax(string code);
        SyntaxStatus compile(string code);
    }
}
