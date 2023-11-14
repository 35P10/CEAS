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

        [Fact]
        public async Task CheckSyntax_InvalidCode2()
        {
            // Arrange
            string pythonCode = "code";

            var result = await _PythonCodeProcessor.CheckSyntaxAsync(pythonCode);

            // Assert
            Assert.False(result.IsOk);
            Assert.NotEmpty(result.ErrorMsg);
        }

        [Fact]
        public async Task CheckSyntax_EmptyCode()
        {
            string codeToCheck = "";

            var result = await _PythonCodeProcessor.CheckSyntaxAsync(codeToCheck);

            Assert.True(result.IsOk);
            Assert.Empty(result.ErrorMsg);
        }

        [Fact]
        public async Task CheckSyntax_CodeWithWarning()
        {
            string codeToCheck = "print('Hello, World!')\nprint('Additional statement')";

            var result = await _PythonCodeProcessor.CheckSyntaxAsync(codeToCheck);

            Assert.True(result.IsOk);
            Assert.Empty(result.ErrorMsg);
            Assert.NotEmpty(result.ObsMsg);
        }

        [Fact]
        public async Task CheckSyntax_CodeWithMultipleErrors()
        {
            string codeToCheck = "print('Hello, World')\nprint('Additional statement'";

            var result = await _PythonCodeProcessor.CheckSyntaxAsync(codeToCheck);

            Assert.False(result.IsOk);
            Assert.NotEmpty(result.ErrorMsg);
            Assert.True(result.ErrorMsg.Count > 1);
        }

        [Fact]
        public async Task ExecutePythonCode_ValidCodeAsync()
        {
            // Arrange
            string pythonCode = "print('Hello, World!')";

            var result = await _PythonCodeProcessor.CheckExecutionAsync(pythonCode);

            // Assert
            Assert.Equal(1, result.IdResponse);
            Assert.Equal("Hello, World!\r\n", result.Output);
        }

        [Fact]
        public async Task ExecutePythonCode_InvalidCode()
        {
            // Arrange
            string pythonCode = "print('Hello, World'";

            var result = await _PythonCodeProcessor.CheckExecutionAsync(pythonCode);

            // Assert
            Assert.Equal(-1, result.IdResponse);
        }

        [Fact]
        public async Task  ExecutePythonCode_EmptyCode()
        {
            // Arrange
            string pythonCode = "";

            var result = await _PythonCodeProcessor.CheckExecutionAsync(pythonCode);

            // Assert
            Assert.Equal(1, result.IdResponse);
            Assert.Equal("", result.Output);
        }

        [Fact]
        public async Task ExecutePythonCode_CodeWithException()
        {
            string pythonCode = "1/0"; 

            var result = await _PythonCodeProcessor.CheckExecutionAsync(pythonCode);

            // Assert
            Assert.Equal(-1, result.IdResponse);
            Assert.Contains("Attempted to divide by zero", result.Output);
        }

        [Fact]
        public async Task ExecutePythonCode_ValidCodeWithDef()
        {
            string pythonCode = "def greet(name):\n    return 'Hello, ' + name\n\noutput = greet('World')\nprint(output)\n";

            var result = await _PythonCodeProcessor.CheckExecutionAsync(pythonCode);

            // Assert
            Assert.Equal(1, result.IdResponse);
            Assert.Equal("Hello, World\r\n", result.Output);
        }

        [Fact]
        public async Task ExecutePythonCode_MultipleOutputs()
        {
            string pythonCode = "print('Output 1')\nprint('Output 2')\n";

            var result = await _PythonCodeProcessor.CheckExecutionAsync(pythonCode);

            // Assert
            Assert.Equal(1, result.IdResponse);
            Assert.Equal("Output 1\r\nOutput 2\r\n", result.Output);
        }

        [Fact]
        public async Task ExecutePythonCode_FunctionWithMultipleOutputs()
        {
            string pythonCode = "def multiple_outputs():\n    print('First Output')\n    print('Second Output')\n\nmultiple_outputs()\n";

            // Act
            var result = await _PythonCodeProcessor.CheckExecutionAsync(pythonCode);

            // Assert
            Assert.Equal(1, result.IdResponse);
            Assert.Equal("First Output\r\nSecond Output\r\n", result.Output);
        }
    }
}

