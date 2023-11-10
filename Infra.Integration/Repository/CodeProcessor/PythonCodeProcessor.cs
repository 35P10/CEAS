using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Application.Contracts;
using Domain.Core.Models;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace Infra.Integration.Repository.CodeProcessor
{
    public class PythonCodeProcessor:ICodeProcessor
    {
        public SyntaxStatus checkSyntax(string codeToCheck)
        {
            var result = new SyntaxStatus();

            try
            {
                var engine = Python.CreateEngine();
                var scriptSource = engine.CreateScriptSourceFromString(codeToCheck, SourceCodeKind.Statements);
                scriptSource.Compile();

                result.IsOk = true;
            }
            catch (SyntaxErrorException ex)
            {
                result.IsOk = false;
                result.ErrorMsg.Add($"Syntax error in line {ex.Line}: {ex.Message}");
            }
            catch (Exception ex)
            {
                result.IsOk = false;
                result.ErrorMsg.Add($"Error: {ex.Message}");
            }

            return result;
        }


        public async Task<SyntaxStatus> CheckSyntaxAsync(string codeToCheck)
        {
            return checkSyntax(codeToCheck);
        }

        public SyntaxStatus compile(string code)
        {
            throw new NotImplementedException();
        }
    }
}
