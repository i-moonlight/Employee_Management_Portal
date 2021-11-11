using System.Collections.Generic;

namespace WebAPI.Application.Cases.EmployeeOperations.Queries.EmployeeList
{
    public class EmployeeListViewModel
    {
        public IList<EmployeeDto> Items { get; set; }
    }
}