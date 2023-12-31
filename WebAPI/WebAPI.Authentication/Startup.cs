using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authentication.Data;
using WebAPI.Authentication.Data.Entities;
using WebAPI.Authentication.Infrastructure;

namespace WebAPI.Authentication
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration config) => Configuration = config;

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

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
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<AppDbContext>();

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
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;
                });

            // services.AddDefaultIdentity<IdentityUser>(opts => 
            //         opts.SignIn.RequireConfirmedAccount = true)
            //     .AddEntityFrameworkStores<AppDbContext>();

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

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
