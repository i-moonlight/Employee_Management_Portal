using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using WebAPI.DataAccess.MsSql;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.Entities.Common;
using WebAPI.Entities.Models;
using WebAPI.UserCases;

namespace WebAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            #region Dependency Injection
            
            services.AddControllers();
            services.AddUserCases();
            services.AddDataAccess(Configuration);
            
            #endregion

            #region JSON Serializer

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            #endregion

            #region Role Identity

            services.AddIdentity<User, IdentityRole>(options => {})
                .AddEntityFrameworkStores<AppDbContext>();
            
            #endregion

            #region Swagger
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebAPI.Authentication",
                    Description = "An ASP.NET Core Web API for managing API documentation"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            });

            #endregion

            #region Enable logging

            services.AddLogging(loggingBuilder =>
            {
                // Enable logging.
                loggingBuilder.AddConsole().AddFilter(
                    DbLoggerCategory.Database.Command.Name, LogLevel.Information);
                // Display output IDE.
                loggingBuilder.AddDebug();
            });
            
            // Enable Serilog.
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

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
                    var issuer = Configuration["JwtConfig:Issuer"];
                    var audience = Configuration["JwtConfig:Audience"];

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
        }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
                RequestPath = "/Photos"
            });

            app.UseRouting();

            app.UseCors();
            // Enable CORS.
            // app.UseCors(options => options
            //     .WithOrigins("http://localhost:4200", "http://localhost:9876")
            //     .AllowAnyMethod()
            //     .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapGet("/", async context =>
            //     {
            //         await context.Response.WriteAsync("Resource server works");
            //     });
            // });
            
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}