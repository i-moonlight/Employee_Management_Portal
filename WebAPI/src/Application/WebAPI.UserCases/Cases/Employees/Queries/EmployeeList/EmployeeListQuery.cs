using System;
using MediatR;

namespace WebAPI.UserCases.Cases.Employees.Queries.EmployeeList
{
    public class EmployeeListQuery : IRequest<EmployeeListViewModel>
    {
        public Guid EmployeeId { get; set; }
    }
}