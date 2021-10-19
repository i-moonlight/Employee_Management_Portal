using System;
using System.Collections;
using System.Linq;
using WebAPI.DataAccess.Persistence;
using WebAPI.Domain.Entities;
using WebAPI.UseCases.Services;

namespace WebAPI.DataAccess.Repositories
{
    public class DepartmentRepository : ICrudRepository<Department>
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext ctx) => _context = ctx;

        /// <summary>
        /// Gets a list of departments ordered by ID.
        /// </summary>
        /// <returns>Returns departments list.</returns>
        public IEnumerable Read()
        {
            return _context.Departments.OrderBy(d => d.Id);
        }

        /// <summary>
        /// Gets the department by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns department.</returns>
        public Department Read(Guid id)
        {
            return _context.Departments.Find(id);
        }

        public IEnumerable GetDepartmentNameList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds an department to the database.
        /// </summary>
        /// <param name="model"></param>
        public void Create(Department model)
        {
            _context.Departments.Add(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates the department.
        /// </summary>
        /// <param name="model">Department model.</param>
        public void Update(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var model = _context.Departments.FirstOrDefault(d => d.Id == id);
            _context.Departments.Remove(model ?? throw new InvalidOperationException());
            _context.SaveChanges();
        }

        public string GetFileName(int id)
        {
            throw new NotImplementedException();
        }
    }
}
