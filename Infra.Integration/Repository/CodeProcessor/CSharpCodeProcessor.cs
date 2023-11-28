using Domain.Application.Contracts;
using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using Infra.Integration.Helpers;

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
            var result = new RunResponse
            {
                IdResponse = 1
            };

            // Obtener las referencias necesarias
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .ToList();

            try
            {
                // Compilar y ejecutar el código dinámicamente
                var compilation = CSharpCompilation.Create("TempAssembly")
                    .AddReferences(references)
                    .AddSyntaxTrees(CSharpSyntaxTree.ParseText(code));

                var assembly = LoadAssembly(compilation);

                var entryPoint = assembly.EntryPoint;
                if (entryPoint != null)
                {
                    // Crear un objeto para capturar la salida de la consola
                    var consoleOutput = new ConsoleOutput();
                    Console.SetOut(consoleOutput);

                    // Ejecutar el método Main
                    entryPoint.Invoke(null, new object[] { Array.Empty<string>() });

                    // Obtener la salida de la consola
                    result.Output = consoleOutput.GetOutput();
                }
                else
                {
                    result.Output = "No se encontró un punto de entrada (Main) en el código proporcionado.";
                }
            }
            catch (Exception ex)
            {
                result.Output = $"Error durante la ejecución: {ex.Message}";
            }

            return result;
        }

        public async Task<RunResponse> CheckExecutionAsync(string codeToCheck)
        {
            return execute(codeToCheck);
        }

        private static Assembly LoadAssembly(CSharpCompilation compilation)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var result = compilation.Emit(ms);

                if (!result.Success)
                {
                    // Manejar errores de compilación
                    var errors = result.Diagnostics
                        .Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error)
                        .Select(diagnostic => diagnostic.GetMessage());

                    throw new InvalidOperationException($"Error de compilación:\n{string.Join("\n", errors)}");
                }
                else
                {
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    return Assembly.Load(ms.ToArray());
                }
            }
        }
    }
}
