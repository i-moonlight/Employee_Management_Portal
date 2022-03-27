using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebAPI.Service.Authentication.Database.Identity;

namespace WebAPI.Service.Authentication
{
    public class Program
    {
        #region Program initialization
        /// <summary>
        /// Program initialization.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("Authentication server initialize.");
                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

#pragma warning disable 4014
                RoleManager.Initialize(scope.ServiceProvider);
#pragma warning restore 4014

                host.Run();
            }
            catch (Exception exp)
            {
                Log.Fatal(exp, "Resource server failed.");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        #endregion

        #region Create web host
        /// <summary>
        /// Create web host.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Web host.</returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(wb => wb.UseStartup<Startup>())
                .ConfigureAppConfiguration(cb => cb.AddJsonFile("appsettings.json", false, true))
                .UseDefaultServiceProvider(opts => opts.ValidateScopes = false) // needed for mediatr DI.
                .UseSerilog();
        }
        #endregion
    }
}