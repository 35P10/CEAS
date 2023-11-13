using Domain.Application.Contracts;
using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Infra.Integration.Repository.CodeProcessor
{
    public class CSharpCodeProcessor:ICodeProcessor
    {
        public SyntaxStatus checkSyntax(string codeToCheck)
        {
            var result = new SyntaxStatus();

            var syntaxTree = CSharpSyntaxTree.ParseText(codeToCheck);

            // Obtener las referencias necesarias
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .ToList();

            // Agregar las referencias al ensamblado que contiene Object y Decimal
            var compilation = CSharpCompilation.Create("TempAssembly")
                .AddReferences(references)
                .AddSyntaxTrees(syntaxTree);

            var diagnostics = compilation.GetDiagnostics();

            foreach (var diagnostic in diagnostics)
            {
                if (diagnostic.Severity == DiagnosticSeverity.Error)
                {
                    result.ErrorMsg.Add(diagnostic.GetMessage());
                    result.IsOk = false;
                }
                else
                {
                    result.ObsMsg.Add(diagnostic.GetMessage());
                }
            }

            return result;
        }

        public async Task<SyntaxStatus> CheckSyntaxAsync(string codeToCheck)
        {
            var result = new SyntaxStatus();

            var syntaxTree = CSharpSyntaxTree.ParseText(codeToCheck);

            // Obtener las referencias necesarias
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .ToList();

            // Agregar las referencias al ensamblado que contiene Object y Decimal
            var compilation = CSharpCompilation.Create("TempAssembly")
                .AddReferences(references)
                .AddSyntaxTrees(syntaxTree);

            var diagnostics = compilation.GetDiagnostics();

            foreach (var diagnostic in diagnostics)
            {
                if (diagnostic.Severity == DiagnosticSeverity.Error)
                {
                    result.ErrorMsg.Add(diagnostic.GetMessage());
                    result.IsOk = false;
                }
                else
                {
                    result.ObsMsg.Add(diagnostic.GetMessage());
                }
            }

            return result;
        }

        public RunResponse execute(string code)
        {
            throw new NotImplementedException();
        }
    }
}
