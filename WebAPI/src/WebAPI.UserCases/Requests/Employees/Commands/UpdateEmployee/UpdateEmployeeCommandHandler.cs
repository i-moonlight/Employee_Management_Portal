using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.UserCases.Requests.Employees.Commands.UpdateEmployee
{
    /// <summary>
    /// Implements a handler for the employee update command.
    /// </summary>
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, string>
    {
        private readonly ICrudRepository<Employee> _repository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(ICrudRepository<Employee> repo, IMapper mapper) =>
            (_repository, _mapper) = (repo, mapper);

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var success = true;
            try
            {
                var employee = _mapper.Map<Employee>(request.EmployeeDto);
                _repository.Update(employee);
            }
            catch (Exception)
            {
                success = false;
            }

            return await Task.FromResult(success ? "Updated successfully" : "Update failed");
        }
    }
}