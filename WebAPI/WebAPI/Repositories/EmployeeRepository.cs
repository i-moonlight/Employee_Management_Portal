using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataBase;

namespace WebAPI.Repositories
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable Read()
        {
            return _context.Employees.OrderBy(x => x.EmployeeId);
        }
    }
}