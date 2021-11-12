using System;
using System.Collections;
using System.Linq;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Interfaces.Interfaces;

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
            return _context.Departments.OrderBy(x => x.DepartmentId);
        }

        public IEnumerable ReadAll()
        {
            throw new NotImplementedException();
        }

        public Department Create(Department model)
        {
            _context.Departments.Add(model);
            _context.SaveChanges();
            return model;
        }
        
        public Department Update(Department model)
        {
            _context.Departments.Update(model);
            _context.SaveChanges();
            return model;
        }
        
        public void Delete(int id)
        {
            var model = _context.Departments.FirstOrDefault(x => x.DepartmentId == id);
            _context.Departments.Remove(model ?? throw new InvalidOperationException());
            _context.SaveChanges();
        }

        public string GetFileName(int id)
        {
            throw new NotImplementedException();
        }
    }
}