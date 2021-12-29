using System.Collections;
using MediatR;

namespace WebAPI.UserCases.Requests.Departments.Queries.GetDepartmentList
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class GetDepartmentListQuery : IRequest<IEnumerable>
    {
    }
}