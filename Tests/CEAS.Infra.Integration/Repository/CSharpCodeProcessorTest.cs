using Xunit;
using Infra.Integration.Repository.CodeProcessor;

namespace CEAS.Infra.Integration.Test.Repository
{
    public class CSharpCodeProcessorTest
    {
        CSharpCodeProcessor _cSharpCodeProcessor;
        public CSharpCodeProcessorTest()
        {
            _cSharpCodeProcessor = new CSharpCodeProcessor();
        }

        [Fact]
        public async Task CheckSyntax_ValidCode()
        {
            string codeToCheck = "using System;\npublic class HelloWorld\n{\npublic static void Main(string[] args)\n{\nConsole.WriteLine (\"Hello Mono World\");\n}\n}";
            var result = await  _cSharpCodeProcessor.CheckSyntaxAsync(codeToCheck);

            Assert.True(result.IsOk);
            Assert.Empty(result.ErrorMsg);
        }

        [Fact]
        public async Task CheckSyntax_InvalidCode()
        {
            string codeToCheck = "using System; class MyClass { static void Main() { Console.WriteLine(\"Hello, World!\") }";

            var result = await _cSharpCodeProcessor.CheckSyntaxAsync(codeToCheck);

            Assert.False(result.IsOk);
            Assert.NotEmpty(result.ErrorMsg);
        }

        [Fact]
        public async Task CheckSyntax_EmptyCode()
        {
            string codeToCheck = "";
            var result = await _cSharpCodeProcessor.CheckSyntaxAsync(codeToCheck);

            Assert.True(result.IsOk);
            Assert.Empty(result.ErrorMsg);
        }

        [Fact]
        public async Task CheckSyntax_CodeWithWarning()
        {
            string codeToCheck = "using System; class MyClass { static void Main() { Console.WriteLine(\"Hello, World!\"); } }";

            var result = await _cSharpCodeProcessor.CheckSyntaxAsync(codeToCheck);

            Assert.True(result.IsOk);
            Assert.Empty(result.ErrorMsg);
            Assert.NotEmpty(result.ObsMsg); // Asegura que hay observaciones (advertencias) en el resultado
        }

        [Fact]
        public async Task CheckSyntax_CodeWithMultipleErrors()
        {
            string codeToCheck = "using System; class MyClass { static void Main() { Console.WriteLine(\"Hello, World!\") }";

            var result = await _cSharpCodeProcessor.CheckSyntaxAsync(codeToCheck);

            Assert.False(result.IsOk);
            Assert.NotEmpty(result.ErrorMsg);
            Assert.True(result.ErrorMsg.Count > 1); // Asegura que hay más de un error en el resultado
        }

        [Fact]
        public async Task Run_ValidCode()
        {
            string codeToCheck = "using System;\npublic class HelloWorld\n{\npublic static void Main(string[] args)\n{\nConsole.WriteLine (\"Hello Mono World\");\n}\n}";
            var result = await  _cSharpCodeProcessor.CheckExecutionAsync(codeToCheck);

            Assert.Equal(1, result.IdResponse);
            Assert.Equal("Hello Mono World\r\n", result.Output);
        }
    }

}

