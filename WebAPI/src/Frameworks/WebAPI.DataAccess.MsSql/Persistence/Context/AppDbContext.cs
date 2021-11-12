using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Interfaces.Interfaces;

namespace WebAPI.DataAccess.MsSql.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<User>, IEmployeeDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        #region Bussiness Entities

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        #endregion
    }
}