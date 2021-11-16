namespace WebAPI.UserCases.Cases.Employees.Queries.EmployeeList
{
    public partial class EmployeeListQueryHandler : IRequestHandler<EmployeeListQuery, EmployeeListViewModel>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeListQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EmployeeListViewModel> Handle(
            EmployeeListQuery request, CancellationToken cancellationToken) =>
        
            new EmployeeListViewModel()
            {
                EmployeeList = await _dbContext.Employees
                    .AsNoTracking()
                    .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.EmployeeId)
                    .ToListAsync(cancellationToken)
            };
    }
}