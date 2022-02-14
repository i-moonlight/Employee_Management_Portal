using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.Utils.Constants;
using WebAPI.Web;

namespace WebAPI.Tests.Common
{
    /// <summary>
    /// Factory for executing functional end to end api tests.
    /// </summary>
    /// <typeparam name="TStartup">A type in the entry point assembly of the application.</typeparam>
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(desc =>
                    desc.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null) services.Remove(descriptor);

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services
                    .AddDbContext<AppDbContext>(options =>
                        options.UseInMemoryDatabase("EmployeeTestDB").UseInternalServiceProvider(serviceProvider))
                    .AddControllers()
                    .AddApplicationPart(typeof(Startup).Assembly);

                var provider = services.BuildServiceProvider();

                using (var scope = provider.CreateScope())
                {
                    using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
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