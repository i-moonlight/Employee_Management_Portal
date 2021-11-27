using System.Collections;
using MediatR;

namespace WebAPI.UserCases.Cases.Employees.Queries.GetEmployeeList
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class GetEmployeeListQuery : IRequest<IEnumerable> {}
}