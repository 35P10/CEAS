using Domain.Application.Contracts;
using Infra.Integration.Repository.CodeProcessor;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Infra.Integration.Factories
{
    public class CodeProcessorFactory:ICodeProcessorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CodeProcessorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICodeProcessor GetCompiler(int idCode)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                switch (idCode)
                {
                    case 1:
                        return scope.ServiceProvider.GetRequiredService<PythonCodeProcessor>();
                    case 2:
                        return scope.ServiceProvider.GetRequiredService<CPPCodeProcessor>();
                    case 3:
                        return scope.ServiceProvider.GetRequiredService<CSharpCodeProcessor>();
                    default:
                        throw new InvalidOperationException("Lenguaje no admitido");
                }
            }
        }

    }
}
