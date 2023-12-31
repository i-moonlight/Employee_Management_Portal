﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Authentication.Data.Entities;

namespace WebAPI.Authentication.Infrastructure
{
    public static class RoleManager
    {
        public static async Task Initialize(IServiceProvider provider)
        {
            foreach (var roleName in RoleNames.AllRoles)
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
            var empResult = await userManager.CreateAsync((User) DefaultUsers.Employee, "User123!");
            var managerResult = await userManager.CreateAsync((User) DefaultUsers.Manager, "User123!");
            var adminResult = await userManager.CreateAsync((User) DefaultUsers.Administrator, "User123!");

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
