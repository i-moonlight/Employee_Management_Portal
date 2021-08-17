using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace WebAPI
{
    public static class Program
    {
        /// <summary>
        /// Program initialization.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void Main(string[] args)
        {
            // Configure logging first.
            ConfigureLogging();

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
        /// Configure Logging.
        /// </summary>
        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        /// <summary>
        /// Configure Elastic Sink.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        /// <returns>Options the elasticsearch sink</returns>
        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot config, string env)
        {
            var sink = new ElasticsearchSinkOptions(new Uri(config["ElasticConfiguration:Uri"]));
            sink.AutoRegisterTemplate = true;
            sink.IndexFormat = $@"{Assembly.GetExecutingAssembly()
                .GetName().Name
                .ToLower().Replace(".", "-")}-{env?.ToLower()
                .Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";
            return sink;
        }

        /// <summary>
        /// Create web host.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>web host</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureAppConfiguration(configuration =>
                {
                    configuration.AddJsonFile("appsettings.json", false, true);
                    configuration.AddJsonFile(
                        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true);
                })
                .UseSerilog()
                .UseNLog(); // NLog: Setup NLog for Dependency injection.
    }
}
