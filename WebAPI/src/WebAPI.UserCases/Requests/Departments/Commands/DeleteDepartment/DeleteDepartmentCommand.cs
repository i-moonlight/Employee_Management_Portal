using System;
using MediatR;

namespace WebAPI.UserCases.Requests.Departments.Commands.DeleteDepartment
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class DeleteDepartmentCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}