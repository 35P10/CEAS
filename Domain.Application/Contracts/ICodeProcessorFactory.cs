namespace Domain.Application.Contracts
{
    public interface ICodeProcessorFactory
    {
        ICodeProcessor GetCompiler(int idcode);
    }
}
