using System.Collections.Generic;

namespace WebAPI.UseCases.Requests.Employees.Queries
{
    public class EmployeeListViewModel
    {
        public IList<EmployeeListDto> EmployeeList { get; set; }
    }
}
