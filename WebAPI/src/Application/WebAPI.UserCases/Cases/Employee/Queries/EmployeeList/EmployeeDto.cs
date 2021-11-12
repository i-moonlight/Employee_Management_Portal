using System;
using AutoMapper;
using WebAPI.UserCases.Common.Mappings;

namespace WebAPI.UserCases.Cases.Employee.Queries.EmployeeList
{
    public class EmployeeDto : IMapWith<Domain.Entities.Employee>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Employee, EmployeeDto>()
                .ForMember(employeeDto => employeeDto.Id,
                    opt => opt.MapFrom(employee => employee.EmployeeId))
                .ForMember(employeeDto => employeeDto.Title,
                    opt => opt.MapFrom(note => note.EmployeeName));
        }
    }
}