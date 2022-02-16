using System.Collections;
using System.Linq;
using WebAPI.DataAccess.MsSql.Persistence.Context;

namespace WebAPI.DataAccess.MsSql.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) =>
            _context = context;

        /// <summary>
        /// Gets a list of employees ordered by ID.
        /// </summary>
        /// <returns>Returns employees list.</returns>
        public IEnumerable Read()
        {
            return _context.Employees.OrderBy(e => e.Id).ToList();
        }
    }
}