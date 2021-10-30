using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Domain.Core.Entities;

namespace WebAPI.Presentation.Identity
{
    public static class SeedData
    {
        public static async Task Initializer(IServiceProvider provider)
        {
            // Create roles.
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var roleName in RoleNames.AllRoles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role != null) continue;
                var result = roleManager
                    .CreateAsync(new IdentityRole {Name = roleName})
                    .Result;
                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Description);
            }
            
            // Create users.
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var empResult = await userManager.CreateAsync(DefaultUsers.Employee, "User123!");
            var managerResult = await userManager.CreateAsync(DefaultUsers.Manager, "User123!");
            var adminResult = await userManager.CreateAsync(DefaultUsers.Administrator, "User123!");

            // Create user roles based on email confirmation.
            if (empResult.Succeeded || managerResult.Succeeded || adminResult.Succeeded)
            {
                var empUser = await userManager.FindByEmailAsync(DefaultUsers.Employee.Email);
                var managerUser = await userManager.FindByEmailAsync(DefaultUsers.Manager.Email);
                var adminUser = await userManager.FindByEmailAsync(DefaultUsers.Administrator.Email);
                await userManager.AddToRoleAsync(empUser, RoleNames.Employee);
                await userManager.AddToRoleAsync(managerUser, RoleNames.Manager);
                await userManager.AddToRoleAsync(adminUser, RoleNames.Administrator);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}