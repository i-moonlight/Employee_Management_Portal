using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataBase;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories.Implementations
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRepository : ICrudRepository<Employee>
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

        public void Delete(int id)
        {
            var model = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
            _context.Employees.Remove(model ?? throw new InvalidOperationException());
            _context.SaveChanges();
        }
    }
}
