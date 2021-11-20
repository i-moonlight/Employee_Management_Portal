using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.UserCases.Cases.Employees.Queries.GetEmployee
{
    /// <summary>
    /// Implements a handler for the employee request.
    /// </summary>
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Employee>
    {
        private readonly ICrudRepository<Employee> _repository;

        public GetEmployeeQueryHandler(ICrudRepository<Employee> repository) =>
            _repository = repository;

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns employee by id</returns>
        public Task<Employee> Handle(GetEmployeeQuery request, CancellationToken cancellationToken) =>
            Task.FromResult(_repository.Read(request.Id));
    }
}