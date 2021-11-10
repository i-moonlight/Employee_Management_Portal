using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;

namespace WebAPI.Persistence.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        #region Bussiness Entities

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        #endregion
    }
}