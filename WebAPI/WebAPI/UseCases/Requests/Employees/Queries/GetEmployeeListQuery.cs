using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using WebAPI.Domain.Entities;
using WebAPI.UseCases.Services;

namespace WebAPI.UseCases.Requests.Employees.Queries
{
    public class GetEmployeeListQuery : IRequest<IEnumerable> {}

    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, IEnumerable>
    {
        private readonly ICrudRepository<Employee> _repository;

        public GetEmployeeListQueryHandler(ICrudRepository<Employee> repo) => _repository = repo;

        
        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns employee list.</returns>
        public async Task<IEnumerable> Handle(GetEmployeeListQuery request, CancellationToken token)
        {
            return await Task.FromResult(_repository.Read());
        }
    }
}
