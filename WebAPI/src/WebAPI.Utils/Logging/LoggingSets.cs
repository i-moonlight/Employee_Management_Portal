using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using static System.Reflection.Assembly;
using static WebAPI.Utils.Logging.ConsoleCustomTheme;

namespace WebAPI.Utils.Logging
{
    public static class LoggingSets
    {
        #region Configure Logging

        /// <summary>
        /// The setup of logging settings by default.
        /// </summary>
        public static void DefaultSetup()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(theme: Signal, outputTemplate: ConsoleOutputTemplate)
                .CreateLogger();
        }

        /// <summary>
        /// The set of logging settings for Elasticsearch.
        /// </summary>
        public static void ElasticsearchSetup()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console(theme: Signal, outputTemplate: ConsoleOutputTemplate)
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
            var sink = new ElasticsearchSinkOptions(new Uri(config["ElasticOptions:Uri"]))
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