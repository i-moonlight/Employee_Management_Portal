using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebAPI.Service.Authentication.Database;
using WebAPI.Service.Authentication.UseCases.Constants;

namespace WebAPI.Service.Authentication.Tests.Common
{
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(desc =>
                    desc.ServiceType == typeof(DbContextOptions<AuthDbContext>));

                if (descriptor != null) services.Remove(descriptor);

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services
                    .AddDbContext<AuthDbContext>(options =>
                        options.UseInMemoryDatabase("EmployeeTestDB").UseInternalServiceProvider(serviceProvider))
                    .AddControllers()
                    .AddApplicationPart(typeof(Startup).Assembly);

                var provider = services.BuildServiceProvider();

                using (var scope = provider.CreateScope())
                {
                    using (var appContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>())
                    {
                        var logger = scope.ServiceProvider
                            .GetRequiredService<ILogger<TestWebApplicationFactory<TStartup>>>();
                        try
                        {
                            appContext.Database.EnsureCreated();
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex.Message, MessageTypes.ErrorMessage);
                            throw;
                        }
                    }
                }
            });
        } 
    }
}