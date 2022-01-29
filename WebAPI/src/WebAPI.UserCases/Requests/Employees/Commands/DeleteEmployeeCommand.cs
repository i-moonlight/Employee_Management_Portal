using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.UserCases.Requests.Employees.Commands
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class DeleteEmployeeCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Implements a handler for the employee delete command.
    /// </summary>
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, string>
    {
        private readonly ICrudRepository<Employee> _repository;

        public DeleteEmployeeCommandHandler(ICrudRepository<Employee> repo) =>
            _repository = repo;

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(DeleteEmployeeCommand request, CancellationToken token)
        {
            var success = true;
            try
            {
                var employee = new Employee() {Id = request.Id};
                _repository.Delete(employee.Id);
            }
            catch (Exception)
            {
                success = false;
            }

            return await Task.FromResult(success ? "Deleted successfully" : "Delete failed");
        }
    }
}