using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebAPI.Infrastructure.Interfaces.Interfaces;
using WebAPI.UserCases.Common.Mappings;

namespace WebAPI.UserCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
           // services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            return services;
        }
    }
}