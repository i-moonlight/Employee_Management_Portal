using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static WebAPI.Authentication.Infrastructure.DependencyInjection.ClaimRoleManager;

namespace WebAPI.Authentication
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
#pragma warning disable 4014
            ClaimRole(scope.ServiceProvider);
#pragma warning restore 4014
            host.Run();
        }
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}