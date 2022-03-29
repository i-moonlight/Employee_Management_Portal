using System.Collections.Generic;
using WebAPI.Service.Authentication.Entities;

namespace WebAPI.Service.Authentication.Database.Identity
{
    public static class DefaultUsers
    {
        public static readonly User Employee = new()
        {
            UserName = "Employee",
            EmailConfirmed = true,
            Email = "Employee@test.ru"
        };

        public static readonly User Manager = new()
        {
            UserName = "Manager",
            EmailConfirmed = true,
            Email = "Manager@test.ru"
        };

        public static readonly User Administrator = new()
        {
            UserName = "Admin",
            EmailConfirmed = true,
            Email = "Admin@test.ru"
        };

        public static IEnumerable<User> AllUsers
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