using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Authentication.Infrastructure.DependencyInjection
{
    public static class ClaimRoleManager
    {
        public static async Task ClaimRole(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();

            var empResult = await userManager.CreateAsync(DefaultUsers.Employee, "Emp123");
            var managerResult = await userManager.CreateAsync(DefaultUsers.Manager, "Manager123");
            var adminResult = await userManager.CreateAsync(DefaultUsers.Administrator, "Admin123");
            
            foreach (var roleName in RoleNames.AllRoles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                
                if (role != null) continue;
                var result = roleManager.CreateAsync(new IdentityRole { Name = roleName }).Result;
                if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
            }

            if (empResult.Succeeded || managerResult.Succeeded || adminResult.Succeeded)
            {
                var empUser = await userManager.FindByEmailAsync(DefaultUsers.Employee.Email);
                var managerUser = await userManager.FindByEmailAsync(DefaultUsers.Manager.Email);
                var adminUser = await userManager.FindByEmailAsync(DefaultUsers.Administrator.Email);

                await userManager.AddToRoleAsync(empUser, RoleNames.Employee);
                await userManager.AddToRoleAsync(managerUser, RoleNames.Manager);
                await userManager.AddToRoleAsync(adminUser, RoleNames.Administrator);
            }
        }
    }
}