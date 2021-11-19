using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.UserCases.Cases.Employees.Queries.EmployeeListQuery
{
    /// <summary>
    /// Sets the object selection property.
    /// </summary>
    public class EmployeeListQuery : IRequest<EmployeeListViewModel>
    {
        public Guid EmployeeId { get; set; }
    }

    /// <summary>
    /// Implements a handler for the request
    /// </summary>
    public class EmployeeListQueryHandler : IRequestHandler<EmployeeListQuery, EmployeeListViewModel>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeListQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>EmployeeListViewModel</returns>
        public async Task<EmployeeListViewModel> Handle(
            EmployeeListQuery request, CancellationToken cancellationToken) =>
            
            new EmployeeListViewModel()
            {
                EmployeeList = await _dbContext.Employees
                    .AsNoTracking()
                    .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
                    .OrderBy(dto => dto.EmployeeId)
                    .ToListAsync(cancellationToken)
            };
    }
}