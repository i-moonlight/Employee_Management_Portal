using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WebAPI.Authentication.Data.Entities;

namespace WebAPI.Authentication.Infrastructure
{
    public static class DefaultUsers
    {
        public static readonly IdentityUser Employee = new IdentityUser
        {
            UserName = "Employee",
            Email = "Employee@test.ru",
            EmailConfirmed = true
        };

        public static readonly IdentityUser Manager = new IdentityUser
        {
            UserName = "Manager",
            Email = "Manager@test.ru",
            EmailConfirmed = true
        };

        public static readonly IdentityUser Administrator = new IdentityUser
        {
            UserName = "Administrator",
            Email = "Admin@test.ru",
            EmailConfirmed = true
        };

        public static IEnumerable<IdentityUser> AllUsers
        {
            get
            {
                yield return Employee;
                yield return Manager;
                yield return Administrator;
            }
        }
    }
}