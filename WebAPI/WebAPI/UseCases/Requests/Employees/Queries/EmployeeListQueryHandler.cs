using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess.Infrastructure;

namespace WebAPI.UseCases.Requests.Employees.Queries
{
    public class EmployeeListQueryHandler : IRequestHandler<EmployeeListQuery, EmployeeListViewModel>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeListQueryHandler(IAppDbContext ctx, IMapper mapper)
        {
            _dbContext = ctx;
            _mapper = mapper;
        }

        public async Task<EmployeeListViewModel> Handle(EmployeeListQuery request, CancellationToken token)
        {
            var employeesQuery = await Extensions.ProjectTo<EmployeeListDto>(
                    _dbContext.Employees.OrderBy(emp => emp.EmployeeId == request.EmployeeId),
                    _mapper.ConfigurationProvider)
                .ToListAsync(token);

            return new EmployeeListViewModel() {EmployeeList = employeesQuery};
        }
    }
}
