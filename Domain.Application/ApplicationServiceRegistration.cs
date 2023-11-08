using Domain.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Externos.Domain.Application.Mappings;

namespace Infra.Integration
{
    public static class ApplicationServiceRegistration
    {
       public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfileApp));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}

