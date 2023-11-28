namespace Infra.Integration.Helpers
{
    public class ConsoleOutput : System.IO.TextWriter
    {
        private readonly List<string> outputLines = new List<string>();

        public override void Write(char value)
        {
            outputLines.Add(value.ToString());
        }

        public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;

        public string GetOutput()
        {
            return string.Join("", outputLines);
        }
    }
}
