using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.UserCases.Cases.Employees.Commands.CreateEmployee
{
    /// <summary>
    /// Implements a handler for the employee create command.
    /// </summary>
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly ICrudRepository<Employee> _repository;

        public CreateEmployeeCommandHandler(ICrudRepository<Employee> repository) =>
            _repository = repository;

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Department = request.Department,
                DateOfJoining = request.DateOfJoining,
                PhotoFileName = request.PhotoFileName
            };

            var success = true;
            try
            {
                _repository.Create(employee);
            }
            catch (Exception)
            {
                success = false;
            }

            return await Task.FromResult(
                success ? "Created successfully" : "Create was not successful");
        }
    }
}