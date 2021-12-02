using MediatR;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.UserCases.Cases.Employees.Commands.UpdateEmployee
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class UpdateEmployeeCommand : IRequest<string>
    {
        public EmployeeDto EmployeeDto { get; set; }
    }
}