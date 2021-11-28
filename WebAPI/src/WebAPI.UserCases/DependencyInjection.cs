using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WebAPI.UserCases
{
    /// <summary>
    /// Dependency injection for class library.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserCases(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(MapperProfile));
            //services.AddMediatR(typeof(CreateEmployeeRequest));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}