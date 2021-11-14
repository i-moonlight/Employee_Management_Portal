using System.Collections.Generic;

namespace WebAPI.UserCases.Cases.Employees.Queries.EmployeeList
{
    public class EmployeeListViewModel
    {
        public IList<EmployeeListDto> EmployeeList { get; set; }
    }
}