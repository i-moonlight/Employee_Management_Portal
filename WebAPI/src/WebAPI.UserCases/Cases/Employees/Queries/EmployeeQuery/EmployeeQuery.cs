using System;
using MediatR;
using WebAPI.Entities.Models;

namespace WebAPI.UserCases.Cases.Employees.Queries.EmployeeQuery
{
    /// <summary>
    /// Sets a property of the query object.
    /// </summary>
    public class EmployeeQuery : IRequest<Employee>
    {
        public Guid Id { get; set; }
    }
}