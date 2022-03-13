using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.DataAccess.MsSql.Repositories;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.DataAccess.MsSql
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                config.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly("WebAPI.DataAccess.MsSql")));
            services.AddScoped<ICrudRepository<Employee>, EmployeeRepository>();
            services.AddScoped<ICrudRepository<Department>, DepartmentRepository>();

            return services;
        }
    }
}