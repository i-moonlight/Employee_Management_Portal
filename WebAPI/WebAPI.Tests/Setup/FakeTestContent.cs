using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using WebAPI.Domain.Entities;
using WebAPI.UseCases.Dto;

namespace WebAPI.Tests.Setup
{
    /// <summary>
    /// Sets the fake content for testing.
    /// </summary>
    public static class FakeTestContent
    {
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

        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}
