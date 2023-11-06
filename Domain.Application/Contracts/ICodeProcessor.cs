using System.Security.Permissions;
using Domain.Core;

namespace Domain.Application.Contracts
{
    public interface ICodeProcessor
    {
        SyntaxStatus checkSyntax(string code);
        SyntaxStatus compile(string code);
    }
}
