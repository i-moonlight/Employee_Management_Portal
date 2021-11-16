using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.DataAccess.MsSql.Repositories;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Interfaces.Interfaces;

namespace WebAPI.DataAccess.MsSql
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppDbContext>(provider =>
                provider.GetService<AppDbContext>());

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("WebAPI.DataAccess.MsSql")));

            services.AddScoped<ICrudRepository<Employee>, EmployeeRepository>();
            services.AddScoped<ICrudRepository<Department>, DepartmentRepository>();

            return services;
        }
    }
}