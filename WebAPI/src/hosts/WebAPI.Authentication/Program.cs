using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebAPI.DataAccess.MsSql.Identity;

namespace WebAPI.Authentication
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
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

            try
            {
                Log.Information("Authentication server initialize");

                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

#pragma warning disable 4014
                RoleManager.Initialize(scope.ServiceProvider);
#pragma warning restore 4014

                host.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine("{0} Exception caught.", exception);
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
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureAppConfiguration(confBuilder => confBuilder.AddJsonFile("appsettings.json", false, true))
                .UseSerilog();
        }

        #endregion
    }
}