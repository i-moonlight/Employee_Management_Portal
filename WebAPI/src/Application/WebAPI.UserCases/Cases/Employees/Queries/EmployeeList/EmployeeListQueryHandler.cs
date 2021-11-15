using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Interfaces.Interfaces;

namespace WebAPI.UserCases.Cases.Employees.Queries.EmployeeList
{
    public class EmployeeListQueryHandler : IRequestHandler<EmployeeListQuery, EmployeeListViewModel>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeListQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EmployeeListViewModel> Handle(
            EmployeeListQuery request, CancellationToken cancellationToken)
        {
            var employeesQuery = await _dbContext.Employees
                 .OrderBy(emp => emp.Id == request.EmployeeId)
                 .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);
            
            return new EmployeeListViewModel() { EmployeeList = employeesQuery };
        }
    }
}