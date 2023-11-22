using FrontEnd.Services.Contracts;
using FrontEnd.Services.Repository;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FrontEnd
{
    public static class ConfigureServiceRegistration
    {

        public static IServiceCollection AddConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebAssemblyHostEnvironment hostEnvironment)
        {
            // Registra el servicio HttpClient
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(hostEnvironment.BaseAddress) });

            // Registra CEASRepo con la fábrica que toma dos parámetros
            var apiUri = hostEnvironment.IsDevelopment() ?
                configuration.GetValue<string>("DebugSettings:ApiUri", "http://localhost:5071/")  :
                configuration.GetValue<string>("ReleaseSettings:ApiUri", "http://localhost:8080/") ;

            services.AddTransient<ICEAS>(provider =>
            {
                var httpClient = provider.GetRequiredService<HttpClient>();

                return new CEASRepo(apiUri, httpClient);
            });

            return services;
        }
    }
}