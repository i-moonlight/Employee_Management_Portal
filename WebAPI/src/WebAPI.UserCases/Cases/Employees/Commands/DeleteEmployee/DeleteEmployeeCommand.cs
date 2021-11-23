using System;
using MediatR;

namespace WebAPI.UserCases.Cases.Employees.Commands.DeleteEmployee
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class DeleteEmployeeCommand : IRequest<string>
    {
        public Guid EmployeeId { get; set; }
    }
}