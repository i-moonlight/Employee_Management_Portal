using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using WebAPI.UseCases.Common.Behaviors;

namespace WebAPI.UseCases
{
    /// <summary>
    /// Dependency injection for class library.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblies(new[] {Assembly.GetExecutingAssembly()});
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}