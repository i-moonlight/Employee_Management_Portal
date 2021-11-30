using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.UserCases.Common.Exceptions;

namespace WebAPI.UserCases.Cases.Employees.Commands.CreateEmployee
{
    /// <summary>
    /// Implements a handler for the employee create command.
    /// </summary>
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly ICrudRepository<Employee> _repository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(ICrudRepository<Employee> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request.EmployeeDto);

            if (employee == null) throw new NotFoundException();

            var success = true;
            try
            {
                _repository.Create(employee);
            }
            catch (Exception)
            {
                success = false;
            }

            return await Task.FromResult(success ? "Created successfully" : "Create failed");
        }
    }
}