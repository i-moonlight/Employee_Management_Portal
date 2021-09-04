using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WebAPI.UseCases
{
    /// <summary>
    /// Dependency injection for class library.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
