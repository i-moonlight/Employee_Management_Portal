using System;
using AutoMapper;
using WebAPI.Domain.Entities;
using WebAPI.UseCases.Mappings;

namespace WebAPI.UseCases.Requests.Employees.Queries
{
    public class EmployeeListDto : IMapWith<Employee>
    {
        public Guid EmployeeId { get; set; }

        public void Mapping(Profile profile)
        {
            profile
                .CreateMap<Employee, EmployeeListDto>()
                .ForMember(dto => dto.EmployeeId, opt => opt.MapFrom(employee => employee.EmployeeId));
        }
    }
}
