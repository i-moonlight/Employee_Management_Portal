using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using static System.Reflection.Assembly;

namespace WebAPI.Utils.Logging
{
    public static class LoggingConfiguration
    {
        #region Configure Logging

        /// <summary>
        /// Configure Logging.
        /// </summary>
        public static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            // Create Logger.
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        #endregion

        #region Configure Elasticsearch Sink

        /// <summary>
        /// Configure Elasticsearch Sink.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        /// <returns>Returns options the elasticsearch sink.</returns>
        public static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot config, string env)
        {
            var sink = new ElasticsearchSinkOptions(new Uri(config["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $@"{GetExecutingAssembly()
                    .GetName().Name?
                    .ToLower()
                    .Replace(".", "-")}-{env?.ToLower()
                    .Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
            return sink;
        }

        #endregion
    }
}