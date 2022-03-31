using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using WebAPI.Entities;
using WebAPI.Service.Authentication.Entities;
using WebAPI.UseCases.Common.Dto;

namespace WebAPI.Tests.Common
{
    /// <summary>
    /// Sets the fake content for testing.
    /// </summary>
    public static class FakeTestContent
    {
        public const string FakeToken =
            "CfDJ8FOZ5NbcDSBPu6UPW3rjaldtdO75Cnqmu4WiOait+wyFheNgvJKPDw8NnvW6xpvlMD3nhhJKQtfftG+UOvDLKFuRioX" +
            "JxjdhFdS5EQwCYU7bZySsJoEU4ccl4kEM5+OHm5XHFiLuOdLF78VUrCu3msH+VYffTS+tWosH/cqpj/xOS4RKLBHaVfgSvg" +
            "xdm6+UQh91aX+H7HENJDMIH2wsMpxnoB50ihWY9HmAga9ytjl+";

        public static User FakeUser => new()
        {
            FullName = "FullName",
            UserName = "UserName",
            Email = "User@test.ru",
            DateModified = DateTime.Today
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