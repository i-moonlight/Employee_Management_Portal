using MediatR;

namespace WebAPI.UserCases.Requests.Employees.Commands.UploadEmployeePhoto
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class UploadPhotoCommand : IRequest<string>
    {
    }
}