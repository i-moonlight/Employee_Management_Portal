using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.DataBase
{
    public class AppDbContext : DbContext
    { 
        public AppDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}