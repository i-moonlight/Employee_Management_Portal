using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using Microsoft.Extensions.Configuration;
using Serilog;
using WebAPI.Utils.Logging;

namespace WebAPI.Web
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
            LoggingSets.ElasticsearchSetup();

            try
            {
                Log.Information("Resource server initialize.");

                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                
                host.Run();
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Resource server failed.");
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
        /// <returns>Returns web host.</returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureAppConfiguration(confBuilder => confBuilder.AddJsonFile("appsettings.json", false, true))
                .UseDefaultServiceProvider(options => options.ValidateScopes = false) // needed for mediatr DI.
                .UseSerilog()
                .UseNLog();
        }

        #endregion
    }
}