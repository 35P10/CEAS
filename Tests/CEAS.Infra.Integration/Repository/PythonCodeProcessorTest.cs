using Xunit;
using Infra.Integration.Repository.CodeProcessor;

namespace CEAS.Infra.Integration.Test.Repository
{
    public class PythonCodeProcessorTest
    {
        PythonCodeProcessor _PythonCodeProcessor;
        public PythonCodeProcessorTest()
        {
            _PythonCodeProcessor = new PythonCodeProcessor();
        }

        [Fact]
        public async Task CheckSyntax_ValidCode()
        {
            string codeToCheck = "print('Hello, World!')";

            var result = await _PythonCodeProcessor.CheckSyntaxAsync(codeToCheck);

            Assert.True(result.IsOk);
            Assert.Empty(result.ErrorMsg);
        }

        [Fact]
        public async Task CheckSyntax_InvalidCode()
        {
            string codeToCheck = "print('Hello, World'";

            var result = await _PythonCodeProcessor.CheckSyntaxAsync(codeToCheck);

            Assert.False(result.IsOk);
            Assert.NotEmpty(result.ErrorMsg);
        }

    }

}

