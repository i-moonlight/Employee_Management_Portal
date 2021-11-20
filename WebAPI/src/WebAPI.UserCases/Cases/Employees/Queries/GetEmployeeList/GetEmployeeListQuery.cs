using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure.Interfaces.DataAccess;

namespace WebAPI.UserCases.Cases.Employees.Queries.GetEmployeeList
{
    /// <summary>
    /// Sets the object selection property.
    /// </summary>
    public class GetEmployeeListQuery : IRequest<EmployeeListViewModel>
    {
        public Guid EmployeeId { get; set; }
    }

    /// <summary>
    /// Implements a handler for the employee list request.
    /// </summary>
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, EmployeeListViewModel>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEmployeeListQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns employee list.</returns>
        public async Task<EmployeeListViewModel> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
            => new EmployeeListViewModel()
            {
                EmployeeList = await _dbContext.Employees
                    .AsNoTracking()
                    .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
                    .OrderBy(dto => dto.EmployeeId)
                    .ToListAsync(cancellationToken)
            };
    }
}