using System;
using System.Collections;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using WebAPI.DataAccess.Persistence;
using WebAPI.Domain.Entities;
using WebAPI.Helpers;
using WebAPI.UseCases.Services;

namespace WebAPI.DataAccess.Repositories
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRepository : ICrudRepository<Employee>
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext ctx) => _context = ctx;

        /// <summary>
        /// Gets a list of employees ordered by ID.
        /// </summary>
        /// <returns>Returns employees list.</returns>
        public IEnumerable Read()
        {
            return _context.Employees.OrderBy(e => e.Id).ToList();
        }

        /// <summary>
        /// Gets the employee by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns employee.</returns>
        public Employee Read(Guid id)
        {
            return _context.Employees.Find(id);
        }

        /// <summary>
        /// Gets department names list sorted alphabetically.
        /// </summary>
        /// <returns>Returns department names list.</returns>
        public IEnumerable ReadAll()
        {
            return _context.Departments.OrderBy(d => d.Name).Select(d => d.Name).ToList();
        }

        public Employee Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return employee;
        }

        public void Delete(int id)
        {
            var model = _context.Employees.FirstOrDefault(e => e.Id == id);
            _context.Employees.Remove(model ?? throw new InvalidOperationException());
            _context.SaveChanges();

            var path = Constants.StoragePath + model.PhotoFileName;
            if (File.Exists(path)) File.Delete(path);
        }

        public string GetFileName(int id)
        {
            return _context.Employees.Find(id).PhotoFileName;
        }
    }
}
