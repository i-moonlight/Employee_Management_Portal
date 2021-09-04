using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;

namespace WebAPI.DataAccess.Infrastructure
{
    public interface IAppDbContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<Employee> Employees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
