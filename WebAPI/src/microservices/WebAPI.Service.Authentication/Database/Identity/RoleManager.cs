using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Service.Authentication.Entities;
using WebAPI.Service.Authentication.UseCases.Constants;

namespace WebAPI.Service.Authentication.Database.Identity
{
    public static class RoleManager
    {
         public static async Task Initialize(IServiceProvider provider)
        {
            foreach (var roleName in RoleNameTypes.AllRoles)
            {
                await CreateRole(provider, roleName);
            }
            await CreateUserRoles(provider);
        }

        /// <summary>
        /// Create a role if not exists.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="roleName"></param>
        private static async Task CreateRole(IServiceProvider provider, string roleName)
        {
             var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
             var roleExists = roleManager.RoleExistsAsync(roleName);
             roleExists.Wait();

            if (!roleExists.Result)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Create a user of roles on email confirmation.
        /// </summary>
        /// <param name="provider"></param>
        private static async Task CreateUserRoles(IServiceProvider provider)
        {
            // Create users.
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var empResult = await userManager.CreateAsync(DefaultUsers.Employee, "Es4$hF9D#eLfo83#9fv");
            var managerResult = await userManager.CreateAsync(DefaultUsers.Manager, "Ms4$hF9D#eLfo83#9fv");
            var adminResult = await userManager.CreateAsync(DefaultUsers.Administrator, "As4$hF9D#eLfo83#9fv");

            // Create user roles based on email confirmation.
            if (empResult.Succeeded || managerResult.Succeeded || adminResult.Succeeded)
            {
                var empUser = await userManager.FindByEmailAsync(DefaultUsers.Employee.Email);
                var managerUser = await userManager.FindByEmailAsync(DefaultUsers.Manager.Email);
                var adminUser = await userManager.FindByEmailAsync(DefaultUsers.Administrator.Email);
                
                await userManager.AddToRoleAsync(empUser, RoleNameTypes.Employee);
                await userManager.AddToRoleAsync(managerUser, RoleNameTypes.Manager);
                await userManager.AddToRoleAsync(adminUser, RoleNameTypes.Administrator);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}