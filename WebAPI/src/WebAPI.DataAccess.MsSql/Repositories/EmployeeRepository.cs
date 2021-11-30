using System;
using System.Collections;
using System.Linq;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.DataAccess.MsSql.Repositories
{
    /// <summary>
    /// Provides access to the database.
    /// </summary>
    public class EmployeeRepository : ICrudRepository<Employee>
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context) => _context = context;

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

        /// <summary>
        /// Adds an employee to the database.
        /// </summary>
        /// <param name="model"></param>
        public void Create(Employee model)
        {
            _context.Employees.Add(model);
            _context.SaveChanges();
        }

        public void Update(Employee model)
        {
            _context.Employees.Update(model);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var model = _context.Employees.FirstOrDefault(x => x.Id == id);
            _context.Employees.Remove(model ?? throw new InvalidOperationException());
            _context.SaveChanges();
        }

        public string GetFileName(Guid id)
        {
            return _context.Employees.Find(id).PhotoFileName;
        }
    }
}