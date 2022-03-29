using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Infrastructure.Interfaces.DataAccess
{
    public interface IAppDbContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<Employee> Employees { get; set; }
    }
}