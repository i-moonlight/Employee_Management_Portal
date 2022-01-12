using MediatR;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.UserCases.Requests.Departments.Commands.UpdateDepartment
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class UpdateDepartmentCommand : IRequest<string>
    {
        public DepartmentDto DepartmentDto { get; set; }
    }
}