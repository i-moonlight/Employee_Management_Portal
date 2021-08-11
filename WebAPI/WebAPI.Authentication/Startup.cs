using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using WebAPI.Authentication.Data;
using WebAPI.Authentication.Data.Entities;

namespace WebAPI.Authentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            // JSON Serializer.
            services.AddControllersWithViews()
                .AddNewtonsoftJson(opts =>
                    opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(opts =>
                    opts.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services
                .AddIdentity<User, IdentityRole>(opts => {})
                .AddEntityFrameworkStores<AppDbContext>();
            
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebAPI.Authentication",
                    Description = "An ASP.NET Core Web API for managing API documentation"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            });
            
            services.AddIdentity<User,IdentityRole>(opts =>
                {
                    opts.Password.RequireDigit = true;
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireUppercase = true;
                    opts.Lockout.MaxFailedAccessAttempts = 6;
                    opts.User.RequireUniqueEmail = true;
                    opts.SignIn.RequireConfirmedEmail = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(opts =>
                {
                    opts.RoutePrefix = string.Empty;
                    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI.Authentication v1");
                });
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}