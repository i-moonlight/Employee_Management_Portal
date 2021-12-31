using MediatR;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.UserCases.Requests.Departments.Commands.CreateDepartment
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class CreateDepartmentCommand : IRequest<string>
    {
        public DepartmentDto DepartmentDto { get; set; }
    }
}