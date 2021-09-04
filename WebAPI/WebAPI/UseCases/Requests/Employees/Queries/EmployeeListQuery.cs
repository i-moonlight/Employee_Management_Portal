using System;
using MediatR;

namespace WebAPI.UseCases.Requests.Employees.Queries
{
    public class EmployeeListQuery : IRequest<EmployeeListViewModel>
    {
        public Guid EmployeeId { get; set; }
    }
}
