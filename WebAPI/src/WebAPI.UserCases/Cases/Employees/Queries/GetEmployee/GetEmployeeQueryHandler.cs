using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Exceptions;

namespace WebAPI.UserCases.Cases.Employees.Queries.GetEmployee
{
    /// <summary>
    /// Implements a handler for the employee request.
    /// </summary>
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly ICrudRepository<Employee> _repository;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandler(ICrudRepository<Employee> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns employee by id.</returns>
        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = _repository.Read(request.Id);

            if (employee == null || employee.Id != request.Id)
                throw new NotFoundException(nameof(Employee), request.Id);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return await Task.FromResult(employeeDto);
        }
    }
}