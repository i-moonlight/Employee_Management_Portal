using System;
using System.Collections;
using System.Linq;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.DataAccess.MsSql.Repositories
{
    public class EmployeeRepository : ICrudRepository<Employee>
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public Employee Read(Guid id)
        {
            return _context.Employees.Find(id);
        }
        
        public IEnumerable ReadAll()
        {
            return _context.Departments.OrderBy(x => x.Id);
        }
        
        public Employee Create(Employee model)
        {
            _context.Employees.Add(model);
            _context.SaveChanges();
            return model;
        }
        
        public Employee Update(Employee model)
        {
            _context.Employees.Update(model);
            _context.SaveChanges();
            return model;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public string GetFileName(Guid id)
        {
            return _context.Employees.Find(id).PhotoFileName;
        }
    }
}