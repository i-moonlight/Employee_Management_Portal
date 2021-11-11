using System;
using AutoMapper;
using WebAPI.Application.Common.Mappings;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Cases.EmployeeOperations.Queries.EmployeeList
{
    public class EmployeeDto : IMapWith<Employee>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDto>()
                .ForMember(employeeDto => employeeDto.Id,
                    opt => opt.MapFrom(employee => employee.EmployeeId))
                .ForMember(employeeDto => employeeDto.Title,
                    opt => opt.MapFrom(note => note.EmployeeName));
        }
    }
}