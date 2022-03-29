using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using WebAPI.DataAccess.MsSql;
using WebAPI.DataAccess.MsSql.Persistence;
using WebAPI.UseCases;

namespace WebAPI.Web
{
    public class Startup
    {
        /// <summary>
        /// Represents a application configuration property.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Represents a application constructor.
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public Startup(IConfiguration configuration) => Configuration = configuration;

        /// <summary>
        /// The optional method registers the services that are used by the application.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region Dependency Injection
            services.AddUseCases();
            services.AddDataAccess(Configuration);
            #endregion

            #region JSON Serialization
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            #endregion

            #region Role Identity
            //services
            //.AddIdentity<User, IdentityRole>()
            //.AddEntityFrameworkStores<AppDbContext>();
            #endregion

            #region Documentaton
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebAPI",
                    Description = "An ASP.NET Core Web API for managing API documentation"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            });
            #endregion

            #region Logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                    .AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
                    .AddDebug();
            });
            services.AddSingleton(Log.Logger);
            #endregion

            #region CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            #endregion

            #region Authentication JWT
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var key = Encoding.ASCII.GetBytes(Configuration["JwtOptions:Secret"]);
                    var issuer = Configuration["JwtOptions:Issuer"];
                    var audience = Configuration["JwtOptions:Audience"];

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        RequireExpirationTime = true,
                    };
                });
            #endregion

            #region Static Files
            services.Configure<StaticFileOptions>(options =>
            {
                var fileDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Photos"));
                if (!fileDirectory.Exists) fileDirectory.Create();
                options.RequestPath = "/Photos";
            });
            #endregion
        }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseStaticFiles(app.ApplicationServices.GetRequiredService<IOptions<StaticFileOptions>>().Value);
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}