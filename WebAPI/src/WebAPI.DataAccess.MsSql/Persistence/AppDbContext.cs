using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.DataAccess.MsSql.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        #region Bussiness Entities
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        #endregion
    }
}