using System;
using AutoMapper;
using WebAPI.UserCases.Common.Mappings;
using WebAPI.Domain.Entities;

namespace WebAPI.UserCases.Cases.Employees.Queries.EmployeeList
{
    /// <summary>
    /// Represents a set of key/value application object properties.
    /// </summary>
    public class EmployeeListDto : IMapWith<Employee>
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        /// <summary>
        /// Compares employee properties in the form of a profile.
        /// Sets the logic of the object data.
        /// </summary>
        /// <param name="profile">profile</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeListDto>()
                .ForMember(
                    dto => dto.EmployeeId,
                    exp => exp.MapFrom(employee => employee.Id))
                .ForMember(
                    dto => dto.EmployeeName,
                    exp => exp.MapFrom(employee => employee.Name));
        }
    }
}