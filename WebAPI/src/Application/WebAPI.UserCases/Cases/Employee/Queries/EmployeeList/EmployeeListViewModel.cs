using System.Collections.Generic;

namespace WebAPI.UserCases.Cases.Employee.Queries.EmployeeList
{
    public class EmployeeListViewModel
    {
        public IList<EmployeeDto> Employees { get; set; }
    }
}