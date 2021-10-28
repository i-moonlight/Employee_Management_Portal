using System.Collections;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Domain.Entities;
using WebAPI.Tests.Setup;
using WebAPI.UseCases.Dto;
using WebAPI.Utils.Constants;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class DepartmentControllerTests : ControllerTestSetup
    {
        private DepartmentController _controller;

        [SetUp]
        public new void Setup()
        {
            _controller = new DepartmentController();

            FakeDbContext.Departments.AddRangeAsync(new Department());
            FakeDbContext.SaveChangesAsync();
        }

        [Test]
        public void GetDepartmentList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act
            var result = _controller.GetDepartmentList();

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Act
            var response = await HttpClient.GetAsync("api/department");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        }

        [Test]
        public void GetDepartmentById_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Arrange
            var departmentId = FakeDepartmentDto.Id;

            // Act
            var result = _controller.GetDepartmentById(departmentId);

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<DepartmentDto>>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentById_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange
            var departmentId = FakeDbContext.Departments?.FirstOrDefault()?.Id;

            // Act
            var response = await HttpClient.GetAsync($"api/department/{departmentId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        }

        [Test]
        public void CreateDepartment_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act
            var result = _controller.CreateDepartment(FakeDepartmentDto);

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task CreateDepartment_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange
            var content = FakeTestContent.GetRequestContent(FakeDepartmentDto);

            // Act
            var response = await HttpClient.PostAsync("api/department", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(ReportTypes.CreatedSuccessfully, stringResponse);
        }

        [Test]
        public async Task CreateDepartment_Method_Should_Returns_Validation_Response()
        {
            // Arrange
            var departmentDto = new DepartmentDto() {Name = null};
            var content = FakeTestContent.GetRequestContent(departmentDto);

            // Act
            var response = await HttpClient.PostAsync("api/department", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(ValidationTypes.NameMustFilled, stringResponse);
        }

        [Test]
        public void UpdateDepartment_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act
            var result = _controller.UpdateDepartment(FakeDepartmentDto);

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task UpdateDepartment_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange
            var content = FakeTestContent.GetRequestContent(FakeDepartmentDto);

            // Act
            var response = await HttpClient.PutAsync("api/department", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(ReportTypes.UpdatedSuccessfull, stringResponse);
        }

        [Test]
        public async Task UpdateDepartment_Method_Should_Returns_Validation_Response()
        {
            // Arrange
            var departmentDto = new DepartmentDto() {Name = null};
            var content = FakeTestContent.GetRequestContent(departmentDto);

            // Act
            var response = await HttpClient.PutAsync("api/department", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(ValidationTypes.NameMustFilled, stringResponse);
        }

        [Test]
        public void DeleteDepartment_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange
            var departmentId = FakeDepartmentDto.Id;

            // Act
            var result = _controller.DeleteDepartmentById(departmentId);

            // Assert
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task DeleteDepartment_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange
            var departmentId = FakeDbContext.Departments?.FirstOrDefault()?.Id;

            // Act
            var response = await HttpClient.DeleteAsync($"api/department/{departmentId}");
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(ReportTypes.DeletedSuccessfull, stringResponse);
        }
    }
}
