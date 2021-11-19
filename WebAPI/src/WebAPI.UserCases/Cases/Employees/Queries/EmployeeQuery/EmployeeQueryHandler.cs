using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.UserCases.Cases.Employees.Queries.EmployeeQuery
{
    public class EmployeeQueryHandler : IRequestHandler<EmployeeQuery, Employee>
    {
        private readonly ICrudRepository<Employee> _repository;

        public EmployeeQueryHandler(ICrudRepository<Employee> repository) =>
            _repository = repository;

        public Task<Employee> Handle(EmployeeQuery request, CancellationToken cancellationToken) =>
            Task.FromResult(_repository.Read(request.Id));
    }
}