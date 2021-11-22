using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.UserCases.Common.Exceptions;

namespace WebAPI.UserCases.Cases.Employees.Commands.UpdateEmployee
{
    /// <summary>
    /// Implements a handler for the employee update command.
    /// </summary>
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, string>
    {
        private readonly ICrudRepository<Employee> _repository;

        public UpdateEmployeeCommandHandler(ICrudRepository<Employee> repository) =>
            _repository = repository;

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                Id = request.Id,
                Name = request.Name,
                Department = request.Department,
                DateOfJoining = request.DateOfJoining,
                PhotoFileName = request.PhotoFileName
            };

            if (employee == null || employee.Id != request.Id)
            {
                throw new NotFoundException(nameof(employee), request.Id);
            }

            var success = true;
            try
            {
                _repository.Update(employee);
            }
            catch (Exception)
            {
                success = false;
            }

            return await Task.FromResult(success ? "Update successful" : "Update was not successful");
        }
    }
}