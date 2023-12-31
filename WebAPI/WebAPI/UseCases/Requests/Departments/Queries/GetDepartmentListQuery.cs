using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Domain.Entities;
using WebAPI.UseCases.Services;

namespace WebAPI.UseCases.Requests.Departments.Queries
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class GetDepartmentListQuery : IRequest<IEnumerable>
    {}

    /// <summary>
    /// Implements a handler for the department list request.
    /// </summary>
    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery, IEnumerable>
    {
        private readonly ICrudRepository<Department> _repository;

        public GetDepartmentListQueryHandler(ICrudRepository<Department> repo) => _repository = repo;

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns department list.</returns>
        public async Task<IEnumerable> Handle(GetDepartmentListQuery request, CancellationToken token)
        {
            return await Task.FromResult(_repository.Read());
        }
    }
}
