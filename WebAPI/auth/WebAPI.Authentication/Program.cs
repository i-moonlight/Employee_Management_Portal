using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.DataAccess.MsSql.Identity;

namespace WebAPI.Authentication
{
    public class Program
    {
        /// <summary>
        /// Program initialize.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void Main(string[] args)
        {
            try
            {
                #region Authentication server initialize

                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope();

#pragma warning disable 4014
                RoleManager.Initialize(scope.ServiceProvider);
#pragma warning restore 4014

                #endregion

                host.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine("{0} Exception caught.", exception);
                throw;
            }
        }

        /// <summary>
        /// Create web host.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>web host</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>())
                .ConfigureAppConfiguration(configuration =>
                    configuration.AddJsonFile("appsettings.json", false, true));
    }
}