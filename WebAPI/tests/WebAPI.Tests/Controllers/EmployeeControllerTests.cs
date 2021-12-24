using NUnit.Framework;
using WebAPI.Controllers;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Tests.Common;
using WebAPI.Web;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.Entities.Models;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private EmployeeController _controller;
        private TestWebApplicationFactory<Startup> _factory;

        [SetUp]
        public void Setup()
        {
            _controller = new EmployeeController();
            _factory = new TestWebApplicationFactory<Startup>();
        }

        [Test]
        public void GetEmployeeList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = _controller.GetEmployeeList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetEmployeeList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var client = _factory.CreateClient();

            // Act.
            var response = await client.GetAsync("api/employee");

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void GetEmployeeById_Method_Should_Returns_ActionResult_EmployeeDto_Type()
        {
            // Arrange.
            var employeeId = TestContent.GetTestEmployeeDto().Id;

            // Act.
            var result = _controller.GetEmployeeById(employeeId);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<EmployeeDto>>), result.GetType());
        }

        [Test]
        public async Task GetEmployeeById_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var dbContext = _factory.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();
            await dbContext.Employees.AddRangeAsync(new Employee());
            await dbContext.SaveChangesAsync();
            var client = _factory.CreateClient();
            var employeeId = dbContext.Employees?.FirstOrDefault()?.Id;

            // Act.
            var response = await client.GetAsync($"api/employee/{employeeId}");

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void GetDepartmentNameList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = _controller.GetDepartmentNameList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentNameList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var client = _factory.CreateClient();

            // Act.
            var response = await client.GetAsync("api/employee/GetDepartmentNames");

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void CreateEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var employeeDto = TestContent.GetTestEmployeeDto();

            // Act.
            var result = _controller.CreateEmployee(employeeDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task CreateEmployee_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var client = _factory.CreateClient();
            var employeeDto = TestContent.GetTestEmployeeDto();
            var content = TestContent.GetRequestContent(employeeDto);

            // Act.
            var response = await client.PostAsync("api/employee", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Created successfully", stringResponse);
        }

        [Test]
        public async Task CreateEmployee_Method_Should_Returns_Validate_Response()
        {
            // Arrange.
            var client = _factory.CreateClient();
            var employeeDto = new EmployeeDto() {Name = null};
            var content = TestContent.GetRequestContent(employeeDto);

            // Act.
            var response = await client.PostAsync("api/employee", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            Assert.AreEqual("Name must be filled", stringResponse);
        }

        [Test]
        public void UploadEmployeePhoto_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act.
            var result = _controller.UploadEmployeePhoto();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task UploadEmployeePhoto_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var client = _factory.CreateClient();
            var employeeDto = TestContent.GetTestEmployeeDto();
            var content = TestContent.GetRequestContent(employeeDto);

            // Act.
            var response = await client.PostAsync("api/employee/UploadPhoto", content);

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void UpdateEmployeePhoto_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var employeeId = TestContent.GetTestEmployeeDto().Id;

            // Act.
            var result = _controller.UpdateEmployeePhoto(employeeId);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task UpdateEmployeePhoto_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var client = _factory.CreateClient();
            var employeeDto = TestContent.GetTestEmployeeDto();
            var content = TestContent.GetRequestContent(employeeDto);

            // Act.
            var response = await client.PostAsync("api/employee/UpdatePhoto", content);

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void UpdateEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var employeeDto = TestContent.GetTestEmployeeDto();

            // Act.
            var result = _controller.UpdateEmployee(employeeDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task UpdateEmployee_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var client = _factory.CreateClient();
            var employeeDto = TestContent.GetTestEmployeeDto();
            var content = TestContent.GetRequestContent(employeeDto);

            // Act.
            var response = await client.PutAsync("api/employee", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Updated successfully", stringResponse);
        }

        [Test]
        public void DeleteEmployee_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var employeeId = TestContent.GetTestEmployeeDto().Id;

            // Act.
            var result = _controller.DeleteEmployeeById(employeeId);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task DeleteEmployee_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var client = _factory.CreateClient();
            var dbContext = _factory.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();
            await dbContext.Employees.AddRangeAsync(new Employee());
            await dbContext.SaveChangesAsync();
            var employeeId = dbContext.Employees?.FirstOrDefault()?.Id;

            // Act.
            var response = await client.DeleteAsync($"api/employee/{employeeId}");
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Deleted successfully", stringResponse);
        }
    }
}