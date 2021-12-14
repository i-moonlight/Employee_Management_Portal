using NUnit.Framework;
using WebAPI.Controllers;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Tests.Common;
using WebAPI.Web;
using System;
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
        private EmployeeDto _employeeDto;
        private TestWebApplicationFactory<Startup> _factory;

        [SetUp]
        public void Setup()
        {
            _controller = new EmployeeController();
            _employeeDto = new EmployeeDto();
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
            // Act.
            var result = _controller.GetEmployeeById(Guid.NewGuid());

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
            // Act.
            var result = _controller.CreateEmployee(_employeeDto);

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

        // [Test]
        // public void Put_Should_Returns_JsonResult_String_Value()
        // {
        //     // Arrange.
        //     _mockEmpRepo.Setup(x => x.Update(_model)).Returns(_fakeCategories.First());
        //
        //     // Act.
        //     var result = _controller.Put(_model);
        //
        //     // Assert.
        //     Assert.NotNull(result, "Result is null");
        //     Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
        //     Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        // }

        // [Test]
        // public void Delete_Should_Returns_JsonResult_String_Value()
        // {
        //     // Arrange.   
        //     _mockEmpRepo.Setup(x => x.Delete(_fakeCategories.First().EmployeeId));
        //
        //     // Act.
        //     var result = _controller.Delete(_fakeCategories.First().EmployeeId);
        //
        //     // Assert.
        //     Assert.NotNull(result, "Result is null");
        //     Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
        //     Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        // }

        // [Test]
        // public void UploadPhoto_Should_Returns_JsonResult_String_Value()
        // {
        //     // Act.
        //     var result = _controller.UploadPhoto();
        //
        //     // Assert.
        //     Assert.NotNull(result, "Result is null");
        //     Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
        //     Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        // }

        // [Test]
        // public void UpdatePhoto_Should_Returns_JsonResult_String_Value()
        // {
        //     // Arrange.   
        //     _mockEmpRepo.Setup(x => x.GetFileName(_fakeCategories.First().EmployeeId)).Returns("PhotoFileName");
        //
        //     // Act.
        //     var result = _controller.UpdatePhoto(_fakeCategories.First().EmployeeId);
        //
        //     // Assert.
        //     Assert.NotNull(result, "Result is null");
        //     Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
        //     Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        // }
    }
}