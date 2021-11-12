using MediatR;

namespace WebAPI.UserCases.Cases.Employee.Queries.EmployeeList
{
    public class EmployeeListQuery : IRequest<EmployeeListViewModel>
    {
        public int EmployeeId { get; set; }
    }
}