using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Entities;
using WebAPI.UseCases.Services;
using WebAPI.Utils.Constants;

namespace WebAPI.UseCases.Requests.Employees.Commands
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class UpdatePhotoCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Implements a handler for the employee photo update command.
    /// </summary>
    public class UpdatePhotoCommandHandler : ActionContext, IRequestHandler<UpdatePhotoCommand, string>
    {
        private readonly ICrudRepository<Employee> _repository;
        private readonly IWebHostEnvironment _environment;

        public UpdatePhotoCommandHandler(ICrudRepository<Employee> repo, IWebHostEnvironment env)
        {
            (_repository, _environment) = (repo, env);
        }

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns update photo file name.</returns>
        public async Task<string> Handle(UpdatePhotoCommand request, CancellationToken token)
        {
            try
            {
                var photoName = _repository.GetPhotoName(request.Id);
                var httpRequest = HttpContext.Request.Form;
                var postedFile = httpRequest.Files[0];
                var filename = postedFile.FileName;
                var selectPath = _environment.ContentRootPath + "/Photos/" + filename;
                var storagePath = PathTypes.PhotoStoragePath + photoName;

                if (File.Exists(selectPath))
                {
                    File.Copy(storagePath, selectPath, true);
                }

                using (var stream = new FileStream(selectPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    if (File.Exists(selectPath)) File.Delete(storagePath);
                }

                return await Task.FromResult(filename);
            }
            catch (Exception)
            {
                return await Task.FromResult("anonymous.png");
            }
        }
    }
}
