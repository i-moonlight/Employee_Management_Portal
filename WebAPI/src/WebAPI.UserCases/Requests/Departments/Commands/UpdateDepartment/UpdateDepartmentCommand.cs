using MediatR;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.UserCases.Requests.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest<string>
    {
        public DepartmentDto DepartmentDto { get; set; }
    }
}