using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using WebAPI.Entities.Models;
using WebAPI.UseCases.Common.Dto;
using WebAPI.UseCases.Common.Dto.Auth;

namespace WebAPI.Tests.Common
{
    /// <summary>
    /// Sets the fake content for testing.
    /// </summary>
    public static class FakeTestContent
    {
        public static User FakeUser => new()
        {
            FullName = "FullName",
            UserName = "UserName",
            Email = "User@test.ru",
            DateModified = DateTime.Today
        };

        public static LoginDto FakeLoginDto => new()
        {
            Username = "UserName",
            Email = "User@test.ru",
            Password = "User123!"
        };

        public static RegisterUserDto FakeRegisterUserDto => new()
        {
            FullName = "FullName",
            UserName = "UserName",
            Email = "User@test.ru",
            Password = "User123!"
        };

        public static EmployeeDto FakeEmployeeDto => new()
        {
            Name = "Name",
            Department = "Department",
            DateOfJoining = DateTime.UtcNow,
            PhotoFileName = "PhotoFileName"
        };

        public static IEnumerable FakeEmployeeList => new List<Employee>
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

        public static DepartmentDto FakeDepartmentDto => new()
        {
            Name = "Name"
        };

        public static IEnumerable FakeDepartmentList => new List<Department>
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