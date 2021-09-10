using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using WebAPI.Domain.Entities;
using WebAPI.UseCases.Dto;
using WebAPI.UseCases.Exceptions;
using WebAPI.UseCases.Services;

namespace WebAPI.UseCases.Requests.Employees.Queries
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class GetEmployeeQuery : IRequest<EmployeeDto>
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Implements a handler for the employee request.
    /// </summary>
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly ICrudRepository<Employee> _repository;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandler(ICrudRepository<Employee> repo, IMapper mapper)
        {
            (_repository, _mapper) = (repo, mapper);
        }

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns employee by id.</returns>
        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken token)
        {
            var employee = _repository.Read(request.Id);

            if (employee == null || employee.Id != request.Id)
            {
                throw new NotFoundException(nameof(Employee), request.Id);
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return await Task.FromResult(employeeDto);
        }
    }
}
