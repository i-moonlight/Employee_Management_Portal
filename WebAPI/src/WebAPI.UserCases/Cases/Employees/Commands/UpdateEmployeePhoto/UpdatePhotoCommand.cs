using System;
using MediatR;

namespace WebAPI.UserCases.Cases.Employees.Commands.UpdateEmployeePhoto
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class UpdatePhotoCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}