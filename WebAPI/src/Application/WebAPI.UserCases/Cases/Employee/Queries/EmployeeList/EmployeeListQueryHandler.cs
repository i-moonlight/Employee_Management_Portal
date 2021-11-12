using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure.Interfaces.Interfaces;

namespace WebAPI.UserCases.Cases.Employee.Queries.EmployeeList
{
    public class EmployeeListQueryHandler : IRequestHandler<EmployeeListQuery, EmployeeListViewModel>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeListQueryHandler(IEmployeeDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<EmployeeListViewModel> Handle(
            EmployeeListQuery request, CancellationToken cancellationToken)
        {
            var employeesQuery = await _dbContext.Employees
                .Where(emp => emp.EmployeeId == request.EmployeeId)
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new EmployeeListViewModel() { Employees = employeesQuery };
        }
    }
}