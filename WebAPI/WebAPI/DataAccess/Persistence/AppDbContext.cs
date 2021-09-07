using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;

namespace WebAPI.DataAccess.Persistence
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) {}
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
