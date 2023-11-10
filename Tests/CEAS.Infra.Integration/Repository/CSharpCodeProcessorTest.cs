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

    }

}

