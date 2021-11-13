using System;
using AutoMapper;
using WebAPI.UserCases.Common.Mappings;
using WebAPI.Domain.Entities;

namespace WebAPI.UserCases.Cases.Employees.Queries.EmployeeList
{
    public class EmployeeListDto : IMapWith<Employee>
    {
        public Guid EmployeeId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeListDto>()
                .ForMember(dto => dto.EmployeeId,
                    opt => opt.MapFrom(employee => employee.EmployeeId));
        }
    }
}