using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Utils.Constants;

namespace WebAPI.UseCases.Requests.Employees.Commands
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class UploadPhotoCommand : IRequest<string>
    {
    }

    /// <summary>
    /// Implements a handler for the employee upload command.
    /// </summary>
    public class UploadPhotoCommandHandler : ActionContext, IRequestHandler<UploadPhotoCommand, string>
    {
        private readonly IWebHostEnvironment _environment;

        public UploadPhotoCommandHandler(IWebHostEnvironment env) =>
            _environment = env;

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(UploadPhotoCommand request, CancellationToken token)
        {
            try
            {
                var httpRequest = HttpContext.Request.Form;
                var postedFile = httpRequest.Files[0];
                var filename = postedFile.FileName;
                var selectPath = _environment.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(selectPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return await Task.FromResult(filename);
            }
            catch (Exception)
            {
                return await Task.FromResult(ReportTypes.NamePhotoDefault);
            }
        }
    }
}
