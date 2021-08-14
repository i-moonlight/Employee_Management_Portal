using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace WebAPI
{
    public static class Program
    {
        /// <summary>
        /// User roles initialization.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void Main(string[] args)
        {
            var logger = NLogBuilder
                .ConfigureNLog("Nlog.config")
                .GetCurrentClassLogger();
            try
            {
                logger.Debug("Program initialization");

                #region Program initialization

                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope();

#pragma warning disable 4014
                SeedData.Initializer(scope.ServiceProvider);
#pragma warning restore 4014

                #endregion

                host.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Program fall");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        /// <summary>
        /// Create web host.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder
                .UseStartup<Startup>()
                .UseNLog());
    }
}
