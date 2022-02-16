using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities.Models;

namespace WebAPI.Infrastructure.Interfaces.DataAccess
{
    public interface IAppDbContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<Employee> Employees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}