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
            services.AddTransient<ICEAS>(provider =>
            {
                var httpClient = provider.GetRequiredService<HttpClient>();

                var uri = "http://localhost:5071/";

                return new CEASRepo(uri, httpClient);
            });

            return services;
        }
    }
}