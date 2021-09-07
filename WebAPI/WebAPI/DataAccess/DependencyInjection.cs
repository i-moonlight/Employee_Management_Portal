using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.DataAccess.Persistence;
using WebAPI.DataAccess.Repositories;
using WebAPI.Domain.Entities;
using WebAPI.UseCases.Services;

namespace WebAPI.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(p => p.GetService<AppDbContext>());
            services.AddScoped<ICrudRepository<Employee>, EmployeeRepository>();
            services.AddScoped<ICrudRepository<Department>, DepartmentRepository>();
            return services;
        }
    }
}
