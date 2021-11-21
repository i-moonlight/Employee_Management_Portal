using System;
using MediatR;

namespace WebAPI.UserCases.Cases.Employees.Commands.CreateEmployee
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class CreateEmployeeCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}