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
        private readonly IEmployeeDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeListQueryHandler(IEmployeeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EmployeeListViewModel> Handle(
            EmployeeListQuery request, CancellationToken cancellationToken)
        {
            var employeesQuery = await _dbContext.Employees.FindAsync(request.EmployeeId);
                // .OrderBy(emp => emp.EmployeeId == request.EmployeeId)
                // .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
                // .ToListAsync();
            
           // var model = await _db.Foods.FindAsync(request.Id);
            return _mapper.Map<Employee, EmployeeListViewModel>(employeesQuery);

            //return new EmployeeListViewModel() { EmployeeList = employeesQuery };
        }
    }
}