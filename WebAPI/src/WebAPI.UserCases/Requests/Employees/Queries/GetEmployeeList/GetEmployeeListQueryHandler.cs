using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.UserCases.Requests.Employees.Queries.GetEmployeeList
{
    /// <summary>
    /// Implements a handler for the employee list request.
    /// </summary>
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, IEnumerable>
    {
        private readonly ICrudRepository<Employee> _repository;

        public GetEmployeeListQueryHandler(ICrudRepository<Employee> repo) =>
            _repository = repo;

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns employee list.</returns>
        public async Task<IEnumerable> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_repository.Read());
        }
    }
}