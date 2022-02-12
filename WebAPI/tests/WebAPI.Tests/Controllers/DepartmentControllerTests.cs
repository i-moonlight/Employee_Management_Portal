using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Entities.Models;
using WebAPI.Tests.Common;
using WebAPI.UserCases.Common.Dto;
using static System.Net.HttpStatusCode;
using static WebAPI.Tests.Common.TestContent;
using static WebAPI.Utils.Constants.MessageTypes;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class DepartmentControllerTests : ControllerTestSetup
    {
        private DepartmentController _departmentController;

        [SetUp]
        public new void Setup()
        {
            _departmentController = new DepartmentController();

            TestDbContext.Departments.AddRangeAsync(new Department());
            TestDbContext.SaveChangesAsync();
        }

        [Test]
        public void GetDepartmentList_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Act.
            var result = _departmentController.GetDepartmentList();

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<IEnumerable>>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentList_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Act.
            var response = await HttpClient.GetAsync("api/department");

            // Assert.
            Assert.AreEqual(OK, response.StatusCode);
        }

        [Test]
        public void GetDepartmentById_Method_Should_Returns_ActionResult_IEnumerable_Type()
        {
            // Arrange.
            var departmentId = TestDepartmentDto.Id;

            // Act.
            var result = _departmentController.GetDepartmentById(departmentId);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<DepartmentDto>>), result.GetType());
        }

        [Test]
        public async Task GetDepartmentById_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var departmentId = TestDbContext.Departments?.FirstOrDefault()?.Id;

            // Act.
            var response = await HttpClient.GetAsync($"api/department/{departmentId}");

            // Assert.
            Assert.AreEqual(OK, response.StatusCode);
        }

        [Test]
        public void CreateDepartment_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act.
            var result = _departmentController.CreateDepartment(TestDepartmentDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task CreateDepartment_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var content = GetRequestContent(TestDepartmentDto);

            // Act.
            var response = await HttpClient.PostAsync("api/department", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(CreatedSuccessfull, stringResponse);
        }

        [Test]
        public async Task CreateDepartment_Method_Should_Returns_Validation_Response()
        {
            // Arrange.
            var departmentDto = new DepartmentDto() {Name = null};
            var content = GetRequestContent(departmentDto);

            // Act.
            var response = await HttpClient.PostAsync("api/department", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            Assert.AreEqual(NameMustFilled, stringResponse);
        }

        [Test]
        public void UpdateDepartment_Method_Should_Returns_ActionResult_String_Type()
        {
            // Act.
            var result = _departmentController.UpdateDepartment(TestDepartmentDto);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task UpdateDepartment_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var content = GetRequestContent(TestDepartmentDto);

            // Act.
            var response = await HttpClient.PutAsync("api/department", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(UpdatedSuccessfull, stringResponse);
        }

        [Test]
        public async Task UpdateDepartment_Method_Should_Returns_Validation_Response()
        {
            // Arrange.
            var departmentDto = new DepartmentDto() {Name = null};
            var content = GetRequestContent(departmentDto);

            // Act.
            var response = await HttpClient.PutAsync("api/department", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            Assert.AreEqual(NameMustFilled, stringResponse);
        }

        [Test]
        public void DeleteDepartment_Method_Should_Returns_ActionResult_String_Type()
        {
            // Arrange.
            var departmentId = TestDepartmentDto.Id;

            // Act.
            var result = _departmentController.DeleteDepartmentById(departmentId);

            // Assert.
            Assert.AreEqual(typeof(Task<ActionResult<string>>), result.GetType());
        }

        [Test]
        public async Task DeleteDepartment_Method_Should_Returns_Success_Http_Status_Code()
        {
            // Arrange.
            var departmentId = TestDbContext.Departments?.FirstOrDefault()?.Id;

            // Act.
            var response = await HttpClient.DeleteAsync($"api/department/{departmentId}");
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert.
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(DeletedSuccessfull, stringResponse);
        }
    }
}