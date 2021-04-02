using System.Collections;
using System.Linq;
using WebAPI.DataBase;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;


namespace WebAPI.Repositories.Implementations
{
    public class DepartmentRepository : ICrudRepository<Department>
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable Read()
        {
            return _context.Departments.OrderBy(x => x.DepartmentId);
        }
    }
}