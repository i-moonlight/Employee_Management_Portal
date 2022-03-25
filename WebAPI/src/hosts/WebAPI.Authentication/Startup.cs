using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Implementation.Services;
using WebAPI.Infrastructure.Interfaces.Options;
using WebAPI.Infrastructure.Interfaces.Services;
using WebAPI.UseCases;

namespace WebAPI.Authentication
{
    public class Startup
    {
        /// <summary>
        /// Represents a application configuration property.
        /// </summary>
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region Enable Session
            services
                .AddSession(options =>
                {
                    options.Cookie.Domain = "localhost";
                    options.Cookie.Name = Configuration["SessionOptions:Cookie"];
                    options.IdleTimeout = TimeSpan.FromMinutes(10);
                    // options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                })
                .Configure<CookiePolicyOptions>(options =>
                {
                    options.MinimumSameSitePolicy = SameSiteMode.Strict;
                    options.HttpOnly = HttpOnlyPolicy.Always;
                    options.Secure = CookieSecurePolicy.Always; // Https
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/forgot-password";
                    //options.LogoutPath = "/auth/logout";
                });
            #endregion

            #region DataBase Connection

            var connection = Configuration["Connection:DefaultConnection"];
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

            #endregion

            #region Configurations

            services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));
            services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));

            #endregion

            #region Dependency Injection
            services
                .AddHttpContextAccessor()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<IHttpService, HttpService>()
                .AddScoped<IEmailService, EmailService>()
                .AddUseCases();
            #endregion

            #region JSON Serializer

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            });

            #endregion

            #region Role Identity

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireUppercase = true;
                    options.Lockout.MaxFailedAccessAttempts = 6;
                    options.Lockout.AllowedForNewUsers = true;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddSignInManager<SignInManager<User>>();

            #endregion

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("ApiCorsPolicy", builder =>
                {
                    builder.AllowCredentials().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                        .WithOrigins("http://localhost:4200", "https://localhost:5001;http://localhost:5000");
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
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseCors("ApiCorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}