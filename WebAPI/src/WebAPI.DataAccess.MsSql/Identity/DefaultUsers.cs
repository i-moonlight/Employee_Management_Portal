using System.Collections.Generic;
using WebAPI.Entities.Models;

namespace WebAPI.DataAccess.MsSql.Identity
{
    public static class DefaultUsers
    {
        public static readonly User Employee = new User
        {
            UserName = "Employee",
            EmailConfirmed = true,
            Email = "Employee@test.ru"
        };

        public static readonly User Manager = new User
        {
            UserName = "Manager",
            EmailConfirmed = true,
            Email = "Manager@test.ru"
        };

        public static readonly User Administrator = new User
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