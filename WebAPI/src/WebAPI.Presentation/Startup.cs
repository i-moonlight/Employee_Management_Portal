using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using WebAPI.Domain.Core.Entities;
using WebAPI.Domain.Core.Interfaces;
using WebAPI.Infrastructure.Data.Persistence.Context;
using WebAPI.Infrastructure.Data.Repositories.Implementations;

namespace WebAPI.Presentation
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region JSON Serializer

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            #endregion

            #region Enable application context
            
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #endregion

            #region Dependency injection
            
            services.AddScoped<ICrudRepository<Employee>, EmployeeRepository>();
            services.AddScoped<ICrudRepository<Department>, DepartmentRepository>();
            
            #endregion

            #region Role Identity
            

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireUppercase = true;
                    options.Lockout.MaxFailedAccessAttempts = 6;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = true;
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddRoles<IdentityRole>()
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
                loggingBuilder.AddConsole()
                // Display sql commands.
                .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
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
            
            app.UseAuthentication();
            
            app.UseAuthorization();
            
            // Enable CORS.
            // app.UseCors(options => options
            //     .WithOrigins("http://localhost:4200", "http://localhost:9876")
            //     .AllowAnyMethod()
            //     .AllowAnyHeader());
            app.UseCors();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}