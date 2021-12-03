using System;
using MediatR;

namespace WebAPI.UserCases.Requests.Employees.Commands.DeleteEmployee
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class DeleteEmployeeCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}