using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(ICrudRepository<Employee> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request.EmployeeDto);

            if (employee == null || employee.Id != request.EmployeeDto.Id)
                throw new NotFoundException(nameof(employee), request.EmployeeDto);

            var success = true;
            try
            {
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