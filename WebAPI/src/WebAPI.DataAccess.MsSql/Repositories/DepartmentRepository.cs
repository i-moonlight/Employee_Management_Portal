using System;
using System.Collections;
using System.Linq;
using WebAPI.DataAccess.MsSql.Persistence;
using WebAPI.Entities;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.DataAccess.MsSql.Repositories
{
    public class DepartmentRepository : ICrudRepository<Department>
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context) => _context = context;

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

        public IEnumerable ReadAll()
        {
            throw new NotImplementedException();
        }

        public string ReadPhotoName(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the department.
        /// </summary>
        /// <param name="model">Department model.</param>
        public void Update(Department model)
        {
            _context.Departments.Update(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes an department by ID.
        /// </summary>
        /// <param name="id">Id of the department (guid).</param>
        public void Delete(Guid id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == id);
            _context.Departments.Remove(department ?? throw new InvalidOperationException());
            _context.SaveChanges();
        }
    }
}