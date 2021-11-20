using System;
using MediatR;
using WebAPI.Entities.Models;

namespace WebAPI.UserCases.Cases.Employees.Queries.GetEmployee
{
    /// <summary>
    /// Sets a property of the query object.
    /// </summary>
    public class GetEmployeeQuery : IRequest<Employee>
    {
        public Guid Id { get; set; }
    }
}