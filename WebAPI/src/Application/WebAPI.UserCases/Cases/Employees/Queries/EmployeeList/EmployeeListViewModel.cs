using System.Collections.Generic;

namespace WebAPI.UserCases.Cases.Employees.Queries.EmployeeList
{
    /// <summary>
    /// Sets the value returned by the object.
    /// </summary>
    public class EmployeeListViewModel
    {
        public IList<EmployeeListDto> EmployeeList = new List<EmployeeListDto>();
    }
}