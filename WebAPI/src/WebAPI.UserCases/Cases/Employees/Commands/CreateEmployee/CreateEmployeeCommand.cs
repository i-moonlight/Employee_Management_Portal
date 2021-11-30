using MediatR;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.UserCases.Cases.Employees.Commands.CreateEmployee
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class CreateEmployeeCommand : IRequest<string>
    {
        public EmployeeDto EmployeeDto { get; set; }
    }
}