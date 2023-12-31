﻿using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataAccess.Persistence;
using WebAPI.Domain.Entities;
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
        public IEnumerable GetDepartmentNameList()
        {
            return _context.Departments.OrderBy(d => d.Name).Select(d => d.Name).ToList();
        }

        /// <summary>
        /// Adds an employee to the database.
        /// </summary>
        /// <param name="model"></param>
        public void Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="model">Employee model</param>
        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">Id of the employee (guid).</param>
        public void Delete(Guid id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            _context.Employees.Remove(employee ?? throw new InvalidOperationException());
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets the photo name by ID.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Returns the file name of the employee's photo.</returns>
        public string GetPhotoName(Guid id)
        {
            return _context.Employees.Find(id).PhotoFileName;
        }
    }
}
