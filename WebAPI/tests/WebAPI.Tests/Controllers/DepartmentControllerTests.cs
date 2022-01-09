using System.Collections;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.DataAccess.MsSql.Persistence.Context;
using WebAPI.Entities.Models;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Dto;
using WebAPI.Web;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class DepartmentControllerTests
    {
        private DepartmentController _controller;
        private TestWebApplicationFactory<Startup> _factory;

        [SetUp]
        public void Setup()
        {
            _controller = new DepartmentController();
            _factory = new TestWebApplicationFactory<Startup>();
        }

        [Test]
        public void GetDepartmentList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = _controller.GetDepartmentList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var client = _factory.CreateClient();

            // Act.
            var response = await client.GetAsync("api/department");

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void GetDepartmentById_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Arrange.
            var departmentId = TestContent.GetTestDepartmentList().Cast<Department>().First().Id;

            // Act.
            var result = _controller.GetDepartmentById(departmentId);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<DepartmentDto>>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentById_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var dbContext = _factory.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();
            await dbContext.Departments.AddRangeAsync(new Department());
            await dbContext.SaveChangesAsync();

            var client = _factory.CreateClient();
            var departmentId = dbContext.Departments?.FirstOrDefault()?.Id;

            // Act.
            var response = await client.GetAsync($"api/department/{departmentId}");

            // Assert.
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void CreateDepartment_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var departmentDto = TestContent.GetTestDepartmentDto();

            // Act.
            var result = _controller.CreateDepartment(departmentDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task CreateDepartment_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var client = _factory.CreateClient();
            var departmentDto = TestContent.GetTestDepartmentDto();
            var content = TestContent.GetRequestContent(departmentDto);

            // Act.
            var response = await client.PostAsync("api/department", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Created successfully", stringResponse);
        }

        [Test]
        public async Task CreateDepartment_Method_Should_Returns_Validation_Response()
        {
            // Arrange.
            var client = _factory.CreateClient();
            var departmentDto = new DepartmentDto() {Name = null};
            var content = TestContent.GetRequestContent(departmentDto);

            // Act.
            var response = await client.PostAsync("api/department", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            Assert.AreEqual("Name must be filled", stringResponse);
        }

        [Test]
        public void UpdateDepartment_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var departmentDto = TestContent.GetTestDepartmentDto();

            // Act.
            var result = _controller.UpdateDepartment(departmentDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        //
        // [Test]
        // public void Delete_Should_Returns_JsonResult_String_Value()
        // {
        //     // Arrange.
        //     _mockDepRepo.Setup(x => x.Delete(_fakeCategories.First().Id));
        //
        //     // Act.
        //     //var result = _controller.Delete(_fakeCategories.First().Id);
        //
        //     // Assert.
        //     // Assert.NotNull(result, "Result is null");
        //     // Assert.AreEqual(new JsonResult(_model).GetType(), result.GetType(), "Return type mismatch");
        //     // Assert.AreEqual(typeof(string), result.Value.GetType(), "Return value type mismatch");
        // }
        //
    }
}