using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;

namespace WebAPI.Infrastructure.Interfaces.Interfaces
{
    public interface IEmployeeDbContext
    {
        DbSet<Employee> Employees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}