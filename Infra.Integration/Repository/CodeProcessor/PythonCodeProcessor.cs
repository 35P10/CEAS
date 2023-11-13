using System;
using System.IO;
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

        public RunResponse execute(string code)
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();

            var outputStream = new MemoryStream();
            engine.Runtime.IO.SetOutput(outputStream, new StreamWriter(outputStream));

            try
            {

            var scriptSource = engine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);
            scriptSource.Execute(scope);

            // Obtener la salida del MemoryStream como una cadena
            outputStream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(outputStream);
            string output = reader.ReadToEnd();


                return new RunResponse
                {
                    IdResponse = 1,
                    Output = output
                };
            }
            catch (Exception ex)
            {
                return new RunResponse
                {
                    IdResponse = -1,
                    Output = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<RunResponse> CheckExecutionAsync(string codeToCheck)
        {
            return execute(codeToCheck);
        }
    }
}
