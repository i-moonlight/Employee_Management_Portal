using System;
using MediatR;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.UserCases.Requests.Employees.Queries.GetEmployee
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class GetEmployeeQuery : IRequest<EmployeeDto>
    {
        public Guid Id { get; set; }
    }
}