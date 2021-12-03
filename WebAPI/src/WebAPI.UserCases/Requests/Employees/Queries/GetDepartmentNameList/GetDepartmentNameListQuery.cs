using System.Collections;
using MediatR;

namespace WebAPI.UserCases.Requests.Employees.Queries.GetDepartmentNameList
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class GetDepartmentNameListQuery : IRequest<IEnumerable>
    {
    }
}