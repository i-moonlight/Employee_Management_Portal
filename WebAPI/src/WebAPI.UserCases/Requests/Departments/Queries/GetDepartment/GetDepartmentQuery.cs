using System;
using MediatR;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.UserCases.Requests.Departments.Queries.GetDepartment
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class GetDepartmentQuery : IRequest<DepartmentDto>
    {
        public Guid Id { get; set; }
    }
}