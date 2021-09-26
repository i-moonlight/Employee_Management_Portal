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
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using WebAPI.DataAccess;
using WebAPI.DataAccess.Infrastructure;
using WebAPI.DataAccess.Persistence;
using WebAPI.Domain.Entities;
using WebAPI.UseCases;
using WebAPI.UseCases.Mappings;

namespace WebAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration config) => Configuration = config;

        public void ConfigureServices(IServiceCollection services)
        {
            #region Dependency Injection
            
            services.AddControllers();
            services.AddUseCases();
            services.AddDataAccess(Configuration);
            
            #endregion

            #region JSON Serializer
            
            services.AddControllersWithViews().AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opts.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            
            #endregion

            // Enable application context.
            //services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppDbContext>(opts => opts
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly("WebAPI.DataAccess")));

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

            #region Mapper

            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IAppDbContext).Assembly));
            });

            services.AddAutoMapper(typeof(AssemblyMappingProfile));

            #endregion
            
            #region Static Files

            services.Configure<StaticFileOptions>(opts =>
            {
                var fileDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Photos"));
                if (!fileDirectory.Exists) fileDirectory.Create();
                opts.RequestPath = "/Photos";
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles(app.ApplicationServices.GetRequiredService<IOptions<StaticFileOptions>>().Value);

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCors();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
