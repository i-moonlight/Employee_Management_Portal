using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using static WebAPI.Utils.Constants.MessageTypes;

namespace WebAPI.UseCases.Requests.Departments.Commands
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class DeleteDepartmentCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, string>
    {
        /// <summary>
        /// Implements a handler for the department delete command.
        /// </summary>
        private readonly ICrudRepository<Department> _repository;

        public DeleteDepartmentCommandHandler(ICrudRepository<Department> repo) =>
            _repository = repo;

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var success = true;
            try
            {
                var department = new Department() {Id = request.Id};
                _repository.Delete(department.Id);
            }
            catch (Exception)
            {
                success = false;
            }

            return await Task.FromResult(success ? DeletedSuccessfull : DeletedFailed);
        }
    }
}