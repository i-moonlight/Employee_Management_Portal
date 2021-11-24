using System;
using MediatR;

namespace WebAPI.UserCases.Cases.Employees.Commands.UploadEmployeePhoto
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class UploadPhotoCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}