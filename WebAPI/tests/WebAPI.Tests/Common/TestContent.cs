using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using WebAPI.Entities.Models;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.Tests.Common
{
    /// <summary>
    /// Sets the content for testing.
    /// </summary>
    public static class TestContent
    {
        public static RegisterUserDto GetTestRegisterUserDto() => new()
        {
            FullName = "FullName",
            UserName = "UserName",
            Email = "User@test.ru",
            Password = "User123!"
        };

        public static EmployeeDto GetTestEmployeeDto() => new()
        {
            Name = "Name",
            Department = "Department",
            DateOfJoining = DateTime.UtcNow,
            PhotoFileName = "PhotoFileName"
        };

        public static IEnumerable GetTestEmployeeList() => new List<Employee>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Department = "Department",
                DateOfJoining = DateTime.UtcNow,
                PhotoFileName = "PhotoFileName"
            }
        };

        public static DepartmentDto GetTestDepartmentDto() => new()
        {
            Name = "Name"
        };

        public static IEnumerable GetTestDepartmentList() => new List<Department>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Name"
            }
        };

        public static StringContent GetRequestContent(object obj) =>
            new(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }
}