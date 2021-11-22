using System;
using System.Collections;
using System.Linq;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.DataAccess.MsSql.Repositories
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
            return _context.Departments.OrderBy(x => x.Id);
        }

        public Department Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Create(Department model)
        {
            _context.Departments.Add(model);
            _context.SaveChanges();
        }
        
        public void Update(Department model)
        {
            _context.Departments.Update(model);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var model = _context.Departments.FirstOrDefault(x => x.Id == id);
            _context.Departments.Remove(model ?? throw new InvalidOperationException());
            _context.SaveChanges();
        }

        public string GetFileName(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}