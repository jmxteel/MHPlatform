using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Installation.Service
{
    public static class ServiceCollection
    {
        public static IServiceCollection MHPlatformServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}