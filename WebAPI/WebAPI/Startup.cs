using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using WebAPI.DataBase;
using WebAPI.Domain.Entities;
using WebAPI.Repositories.Implementations;
using WebAPI.Repositories.Interfaces;

namespace WebAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration config) => Configuration = config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            // JSON Serializer.
            services
                .AddControllersWithViews()
                .AddNewtonsoftJson(opts => opts
                    .SerializerSettings
                    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(opts => opts
                    .SerializerSettings
                    .ContractResolver = new DefaultContractResolver());
            
            // Enable application context.
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Dependency injection.
            services.AddScoped<ICrudRepository<Employee>, EmployeeRepository>();
            services.AddScoped<ICrudRepository<Department>, DepartmentRepository>();

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

            #endregion
            
            #region Role Identity

            services
                .AddIdentity<User, IdentityRole>(_ => {})
                .AddEntityFrameworkStores<AppDbContext>();
            
            #endregion Role Identity
            
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // This middleware is used to returns static files and short-circuits further request processing.   
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
                RequestPath = "/Photos"
            });

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCors();
            
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
