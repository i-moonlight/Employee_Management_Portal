using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.UserCases.Requests.Employees.Queries.GetDepartmentNameList
{
    /// <summary>
    /// Implements a handler for the department names list query.
    /// </summary>
    public class GetDepartmentNameListQueryHandler : IRequestHandler<GetDepartmentNameListQuery, IEnumerable>
    {
        private readonly ICrudRepository<Employee> _repository;

        public GetDepartmentNameListQueryHandler(ICrudRepository<Employee> repo) =>
            _repository = repo;

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns department name list.</returns>
        public async Task<IEnumerable> Handle(GetDepartmentNameListQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_repository.ReadAll());
        }
    }
}