using Domain.Application.Contracts;
using Infra.Integration.Repository.CodeProcessor;
using Domain.Infra.Integration.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infra.Integration
{
    public static class IntegrationServiceRegistration
    {
       public static IServiceCollection AddIntegrationInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<PythonCodeProcessor>();
            services.AddTransient<CPPCodeProcessor>();
                        
            services.AddSingleton<ICodeProcessorFactory, CodeProcessorFactory>();
            return services;
        }
    }
}

