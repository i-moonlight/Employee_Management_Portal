using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using WebAPI.Entities.Models;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.Tests.Common
{
    public static class TestContent
    {
        public static EmployeeDto GetTestEmployeeDto() => new EmployeeDto()
        {
            Name = "Name",
            Department = "Department",
            DateOfJoining = DateTime.UtcNow,
            PhotoFileName = "PhotoFileName"
        };

        public static IEnumerable GetTestEmployeeList() => new List<Employee>
            {
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Department = "Department",
                    DateOfJoining = DateTime.UtcNow,
                    PhotoFileName = "PhotoFileName"
                }
            }
            .AsEnumerable();

        public static IEnumerable GetTestDepartmentList() => new List<Department>
            {
                new Department()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name"
                }
            }
            .AsEnumerable();

        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}