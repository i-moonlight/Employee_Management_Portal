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

        public DepartmentRepository(AppDbContext context) =>
            _context = context;

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